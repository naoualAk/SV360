using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModbusTCP;
using uEye.Defines;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Diagnostics;
using SV360.Utils;
using System.Windows.Forms;
using System.IO;

namespace SV360.Automatism.Camera
{

    /// <summary>
    /// Classe qui permet de piloter une camera IDS
    /// </summary>
    public class CameraIDS : Camera
    {

        private uEye.Camera camera;
        private int counterImage = 0;
        private Status statusRet;
        private IntPtr displayHandle;
        bool bLive;

        /// <summary>
        /// Constructeur CameraIDS permet 
        ///     d'ouvrir la communication avec une caméra IDS, 
        ///     charger la configuration "Directory.GetCurrentDirectory() +  @"\cfg\IDS.ini""
        ///     Forcer le mode trigger
        ///     Start l'acquisition de la caméra
        ///     
        /// Les deux paramètres sont utilisés si il y a une intéraction entre automate et caméra (exemple :  un trigger)
        /// </summary>
        /// <param name="master">Connexion ModbusTCP avec l'automate</param>
        /// <param name="idSlave">ID de la caméra sur l'automate</param>
        public CameraIDS(Master master, ushort idSlave) : base(master, idSlave)
        {
            try
            {
                Init();
            }
            catch
            {
                Exception.LogMessage("Camera init is failed.");
                Environment.Exit(-1);
            }
        }

        private void Init()
        {
            camera = new uEye.Camera();

            uEye.Defines.Status statusRet = 0;

             // Open Camera
            statusRet = camera.Init();
            if (statusRet != uEye.Defines.Status.Success)
            {
                Debug.WriteLine("IDS not found");
                MessageBox.Show("Conveyor camera is not found.");
                throw new Exception("IDS init failed or not found");
                Environment.Exit(-1);
            }

            camera.Trigger.Set(uEye.Defines.TriggerMode.Lo_Hi);

            // Allocate Memory
            statusRet = camera.Memory.Allocate();
            if (statusRet != uEye.Defines.Status.Success)
            {
                throw new Exception("IDS Allocation failed");
                Environment.Exit(-1);
            }

            //LoadParam(@"C:\Users\damien.delhay\Documents\Travail\Programmation\Systeme convoyeur\param3.ini");
            // LoadParam(@"C:\Users\damien.delhay\Documents\Travail\Programmation\Systeme convoyeur\param-haut-28-11.ini");
            // LoadParam(@"C:\SV360\IDS cfg SV360.ini");
            LoadParam(Directory.GetCurrentDirectory() +  @"\cfg\IDS.ini");

            Start();

            // Connect Event
            camera.EventFrame += onFrameEvent;
            camera.EventDeviceRemove += Disconnected;
            camera.EventDeviceUnPlugged += UnPlugged;

        }

        private void LoadParam(string path)
        {
            uEye.Defines.Status statusRet = 0;

            camera.Acquisition.Stop();

            Int32[] memList;
            statusRet = camera.Memory.GetList(out memList);
            if (statusRet != uEye.Defines.Status.Success)
            {
                throw new Exception("Get memory list failed: " + statusRet);
                Environment.Exit(-1);
            }

            statusRet = camera.Memory.Free(memList);
            if (statusRet != uEye.Defines.Status.Success)
            {
                throw new Exception("Free memory list failed: " + statusRet);
                Environment.Exit(-1);
            }

            statusRet = camera.Parameter.Load(path);
            if (statusRet != uEye.Defines.Status.Success)
            {
                throw new Exception("Loading parameter failed: " + statusRet);
            }

            statusRet = camera.Memory.Allocate();
            if (statusRet != uEye.Defines.Status.SUCCESS)
            {
                throw new Exception("Allocate Memory failed");
                Environment.Exit(-1);
            }

            if (bLive == true)
            {
                camera.Acquisition.Capture();
            }
        }

        private void UnPlugged(object sender, EventArgs e)
        {
           // throw new NotImplementedException();
        }

        private void Disconnected(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Event déclenché quand une acquisition est prise par la caméra
        /// </summary>
        public override event EventHandler AcquisitionNotify;

        /// <summary>
        /// Event déclenché quand la camera est perdu (perte de signal)
        /// </summary>
        public override event EventHandler CameraNotFounded;

        private void onFrameEvent(object sender, EventArgs e)
        {
            //Debug.WriteLine("onFrameEvent");
            uEye.Camera camera = sender as uEye.Camera;

            Int32 s32MemID;
            camera.Memory.GetActive(out s32MemID);
            //camera.Discamera.Imageplay.Render(s32MemID, displayHandle, uEye.Defines.DisplayRenderMode.FitToWindow);

            Bitmap bmp;
            camera.Memory.ToBitmap(s32MemID, out bmp);
            image = new Image<Rgb, byte>(bmp);

            Debug.WriteLine("img :" + image[16, 16]);
            if (image[16, 16].Red == 0 || image[16, 16].Green == 0 || image[16, 16].Blue == 0)
            { 
                Debug.WriteLine("Wrong Image acquisition: Image is black.\n Retry.");
                TakeAcquisition();

                return;
            }

            //image.Convert<Bgr, Byte>().Resize(0.25, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR).Save(@"C:\Users\damien.delhay\Documents\Travail\Programmation\Systeme convoyeur\validation micro-calibrage\image" + counterImage + ".bmp");
            //image.Convert<Bgr, Byte>().Resize(0.2, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR).Save(@"C:\Users\damien.delhay\Documents\Travail\Programmation\Systeme convoyeur\validation machine\calibration rapide\image" + counterImage + ".bmp");
            //image.Convert<Bgr, Byte>().Save(@"C:\Users\damien.delhay\Documents\Travail\Programmation\Systeme convoyeur\validation micro-calibrage\new\image" + counterImage + ".bmp");
            image.Convert<Bgr, Byte>().Save(@"E:\Pois\image" + counterImage + ".bmp");

            //  ImageSaver imgsaver = new ImageSaver(@"C:\Users\damien.delhay\Documents\Travail\Programmation\Systeme convoyeur\validation micro-calibrage\image" + counterImage + ".bmp", bmp);

            AcquisitionNotify(this, EventArgs.Empty);

            counterImage++;

           // camera.Image.Save(@"C:\Users\damien.delhay\Documents\Travail\Programmation\Systeme convoyeur\test_IDS\image" + counterImage + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp);
        }


        /// <summary>
        /// non implementée
        /// </summary>
        public override void Close()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// non implémentée
        /// </summary>
        /// <param name="sensibility"></param>
        public override void SetSensibility(int sensibility)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Met la caméra en mode acquisition.
        /// Par défaut, la caméra attendra un trigger
        /// </summary>
        public override void Start()
        {
            if (!bLive)
            {
                // Start Live Video
                statusRet = camera.Acquisition.Capture();
                if (statusRet != uEye.Defines.Status.Success)
                {
                    throw new Exception("Starting IDS camera failed");
                }
                bLive = true;
            }
        }

        /// <summary>
        /// Stop le mode acquistion de la caméra
        /// </summary>
        public override void Stop()
        {
            if (bLive)
            {
                statusRet = camera.Acquisition.Stop();
                if (statusRet != uEye.Defines.Status.Success)
                {
                    throw new Exception("Stopping IDS camera failed");
                }
                bLive = false;
            }
        }


        /// <summary>
        /// Demande à l'automate d'envoyer un trigger à la caméra
        /// </summary>
        public override void TakeAcquisition()
        {
            //Start();  
            Send(true);
            //Stop();
        }
    }
}
