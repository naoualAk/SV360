using System;
using System.Collections.Generic;
using System.Diagnostics;

using SV360.Data;
using SV360.Preprocessing;
using SV360.Image_Processing;
using SV360.Automatism;
using SV360.Utils;
using Emgu.CV.Structure;

namespace SV360
{

    /// <summary>
    /// Classe qui permet de manager et controler les processus liés aux logociels et à la machine (automate, caméra)
    /// </summary>
    public class SV360Controller
    {
        /// <summary>
        /// Permet de manager l'automate
        /// </summary>
        public SystemController sc;
        //public MachineLearning ml;

        /// <summary>
        /// liste de grains 
        /// </summary>
        public List<Seed> seeds;
        private bool enabled = false;


        ConvoyeurPP convoyeurPP;
        GlobalVisionPP globalVisionPP;
        List<ImageProcessing> ImageProcessings;


        bool doEject = false;

        bool isFirstAcquisition = true;

        private bool isIDSFirstAcquisition = true;
        private int IDSCounter = 0;
        public int FinishedCounter = 0;
        public bool hasTakeIDSBackground = false;

        public bool Enabled
        {
            get
            {
                return enabled;
            }

            set
            {
                enabled = value;
            }
        }

        // public event EventHandler processesCompleted;
        public event EventHandler<float> UpdateUI;

        /// <summary>
        /// Cstor : Initialise le systemcontroller permettant de piloter l'automate, les preprocessings, les processings, la caméra, les events
        /// </summary>
        public SV360Controller()
        {
            sc = new SystemController();

            //ml = new MachineLearning();
            seeds = new List<Seed>();

            globalVisionPP = new GlobalVisionPP();
            convoyeurPP = new ConvoyeurPP();

            ImageProcessings = new List<ImageProcessing>();
            ImageProcessings.Add(new MorphoIP());

            //  sc.cameraJAI.Start();
            // sc.cameraIDS.Start();

            // processesCompleted += Eject;

            //sc.cameraJAI.AcquisitionNotify += JAIAcquisitionNotified;
            //sc.cameraIDS.AcquisitionNotify += IDSAcquisitionNotified;
        }


        /// <summary>
        /// Instancie les preprocessing, remise à zéro
        /// </summary>
        public void InitAcq()
        {
            globalVisionPP = new GlobalVisionPP();
            convoyeurPP = new ConvoyeurPP();

            isFirstAcquisition = true;
            isIDSFirstAcquisition = true;
            FinishedCounter = 0;
            IDSCounter = 0;
            sc.hasTakeIDSBackGround = false;
            hasTakeIDSBackground = false;
            sc.StartConveyor();

        }

        int nbClass = 0;

        /// <summary>
        /// Demande au systemController de commencer l'acquisition des images de fonds
        /// </summary>
        public void TakeAcquisitionBackground()
        {
            InitAcq();

            sc.TakeAcquisitionBackGround();







        }


        /// <summary>
        /// Regarde si le seuillage a plus d'une classe sinon renvoyer tout les grains dans le bac 1 (à droite)
        /// </summary>
        /// <returns>Ejection activée? oui ou non</returns>
        public bool ActivateConstantEjection()
        {
            Sorting sorting;
            sorting = Sorting.Instance();


            if (sorting != null && sorting.Criterias != null && sorting.Criterias.Count != 0)
                nbClass = sorting.Criterias.Count;
            else
                nbClass = 1;

            if (nbClass <= 1)
            {
                sc.Eject(true);
                return true;
            }
            else return false;

        }


        private int nbSeedFps = 0, nbSeedPrev;
        private bool counterFpsEnabled = false;
        Stopwatch timeFps;
        float[] timeSeed;
        float fps;

        /// <summary>
        /// Event de l'acquisition d'une image par la caméra IDS : 
        ///     Lors de cet events seront faits : 
        ///     <list type="bullet">
        ///     <item>les calculs preprocessings,</item>
        ///     <item>les calculs processings</item>
        ///     <item>la classification en classes en fonction du choix utilisateur</item>
        ///     <item>l'affichage des données</item>
        ///     <item>le calcul de temps entre les grains</item>
        /// </list>" 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IDSAcquisitionNotified(object sender, EventArgs e)
        {

            if (IDSCounter < 5)
            {
                convoyeurPP.AddBg(sc.cameraIDS.Image);
                Debug.WriteLine("IDS imBg saved : " + IDSCounter);
                IDSCounter++;
                if (IDSCounter == 4 && convoyeurPP.ImBg != null)
                {
                    hasTakeIDSBackground = true;
                    // convoyeurPP.ImBg.Convert<Bgr, Byte>().Save(@"E:\Pois\bg.bmp");
                    //convoyeurPP.ImBg.Convert<Bgr, Byte>().Save(@"C:\Users\damien.delhay\Documents\Travail\Gestion de projet\Projet VCO222\BDD\Aubergine\Convoyeur\bg.bmp");
                }
                nbSeedFps = 0;
                counterFpsEnabled = false;
                timeSeed = new float[5];
                return;
            }

            if (!enabled) return;

            seeds.Add(new Seed(new Acquisition(sc.cameraIDS.Image)));
            // Debug.WriteLine("IDS ACQ : {0} , first Counter : {1}", seeds[seeds.Count - 1].ImageCollection.Count, seeds.Count - 1);

            int idx = seeds.Count - 1;

            //pre process
            if (!convoyeurPP.preprocessing(seeds[idx].ImageCollection[0]))
            {
                Debug.WriteLine("convoyeurPP.preprocessing failed!");

                seeds[idx].DisposeAcquisitions();
                seeds.RemoveAt(idx);
                return;
            }

            Debug.WriteLine("Seeds Counter: {0} ", idx);
            Debug.WriteLine("DIFF ACQ : {0} ", idx - FinishedCounter);

            for (int i = 0; i < ImageProcessings.Count; i++)
            {
                if (!ImageProcessings[i].process(seeds[idx]))
                {
                    Debug.WriteLine("ImageProcessing failed! for seed " + idx);
                }
            }


            if (nbClass > 1)
            {
                int classEject = 0;
                if ((int)seeds[idx].NumClass > 0)
                    classEject = 5 - (int)seeds[idx].NumClass;


                Debug.WriteLine("Ejection in class " + (int)seeds[idx].NumClass);
                sc.SetNextEject(classEject);
            }

            #region calculate FPS

            nbSeedFps++;

            if (timeFps == null)
            {
                timeFps = Stopwatch.StartNew();
            }
            else
            {
                timeSeed[(nbSeedFps - 1) % 5] = timeFps.ElapsedMilliseconds;
                //Debug.WriteLine(timeFps.ElapsedMilliseconds + "in" + (nbSeedFps - 1) % 5);
                timeFps.Restart();
            }


            if (nbSeedFps > 3)
            {
                float sum = 0;
                int end = nbSeedFps <= 5 ? nbSeedFps - 1 : 5;
                for (int i = 0; i < end; i++)
                {
                    sum += timeSeed[i];
                    //Debug.WriteLine(timeSeed[i]);
                }

                fps = (end + 1) * 1000 / sum;

            }
            else fps = 0;
            #endregion


            // seeds[idx].ImageCollection[0].Image.Convert<Bgr, Byte>().Resize(0.1, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR).Save(@"C:\Users\damien.delhay\Documents\Travail\Programmation\Systeme convoyeur\validation machine\calibration rapide\g" + seeds[idx].Width + "-" + idx + ".bmp");


            /*   if (seeds[idx].Length > 15)
               {
                   seeds[idx].ImageCollection[0].Image.Save(@"C:\Users\damien.delhay\Documents\Travail\Programmation\Systeme convoyeur\test_IDS\pb15-"+ idx + ".bmp");
               }
               else if (seeds[idx].Length == 0)
               {
                   seeds[idx].ImageCollection[0].Image.Save(@"C:\Users\damien.delhay\Documents\Travail\Programmation\Systeme convoyeur\test_IDS\pb0-" + idx + ".bmp");
               }*/


            UpdateUI(this, fps);

            if (seeds[idx] != null)
                seeds[idx].DisposeAcquisitions();

            //seeds[seeds.Count - 1].DisposeAcquisitions();
        }

        /// <summary>
        /// Event pour permettre une ejection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /*   private void Eject(object sender, EventArgs e)
           {
               if (seeds[FinishedCounter - 1].NumClass > 0)
               {
                   sc.Eject();
                   //doEject = false;
               }
               //else doEject = true;
           }*/


        /* void JAIAcquisitionNotified(object sender, EventArgs e)
         {
             if (sc.cameraJAI.Image != null)
             {
                 if (isFirstAcquisition)
                 {
                    // globalVisionPP.imBg = sc.cameraJAI.Image;
                     //  globalVisionPP.imBg.Save(@"D:\Users\damien.delhay\Documents\Travail\Images\test_SDKJAI\Preproc\bg.bmp");
                     Debug.WriteLine("imBg saved");
                     isFirstAcquisition = false;
                     return;
                 }

                 if (!enabled) return;


                 // Debug.WriteLine("JAI ACQ : {0} ; FinishedCounter : {1}", seeds[FinishedCounter].ImageCollection.Count, FinishedCounter);
                 Debug.WriteLine("DIFF ACQ : {0} ", seeds.Count - 1 - FinishedCounter);
                 if (seeds.Count - 1 - FinishedCounter < 0)
                 {
                     // sc.cameraJAI.Image.Save(@"C:\Users\damien.delhay\Documents\Travail\Programmation\Systeme convoyeur\test_IDS\pb.bmp");
                     FinishedCounter++;
                     return;
                     //sc.Close();
                     //throw new Exception("DIFF ACQ < 0");
                 }

                 seeds[FinishedCounter].AddAcquisition(new Acquisition(sc.cameraJAI.Image));
                 //Debug.WriteLine("JAI ACQ : {0} ; FinishedCounter : {1}", seeds[FinishedCounter].ImageCollection.Count, FinishedCounter);

             }
             ///pre process
           /*  if (!globalVisionPP.preprocessing(seeds[FinishedCounter].ImageCollection[seeds[FinishedCounter].ImageCollection.Count - 1]))
                 Debug.WriteLine("globalVisionPP.preprocessing failed!");
                 */
        /* FinishedCounter++;

         processesCompleted(this, EventArgs.Empty);
         seeds[FinishedCounter - 1].DisposeAcquisitions();


     }*/
        /// <summary>
        /// Dstor : 
        ///    Fermeture du systemcontroller
        /// </summary>
        ~SV360Controller()
        {
            if (sc != null) sc.Close();
        }
    }
}
