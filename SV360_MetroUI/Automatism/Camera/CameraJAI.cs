using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jai_FactoryDotNET;
using System.Drawing;
using System.Drawing.Imaging;
using ModbusTCP;
using Emgu.CV;
using Emgu.CV.Structure;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Diagnostics;

namespace SV360.Automatism.Camera
{

    /// <summary>
    /// Classe qui permet de controler une caméra JAI
    /// </summary>
    public class CameraJAI : Camera
    {

        // Main factory object
        CFactory myFactory = new CFactory();

        // Opened camera object
        CCamera myCamera;

        // GenICam nodes
        CNode myWidthNode;
        CNode myHeightNode;
        CNode myGainNode;
        CNode myPxNode;
        uint myRGain;
        uint myGGain;
        uint myBGain;

        Jai_FactoryWrapper.EPixelFormatType pixelFormatType = Jai_FactoryWrapper.EPixelFormatType.GVSP_PIX_BAYRG8;
        Jai_FactoryWrapper.ImageInfo myDIBImageInfo = new Jai_FactoryWrapper.ImageInfo();
        private bool connected;

        /// <summary>
        /// Event déclenché quand une acquisition est prise par la caméra
        /// </summary>
        public override event EventHandler AcquisitionNotify;

        /// <summary>
        /// Event déclenché quand la camera est perdu (perte de signal)
        /// </summary>
        public override event EventHandler CameraNotFounded;

        /// <summary>
        /// Constructeur CameraIDS permet 
        ///     d'ouvrir la communication avec une caméra JAI, 
        ///     
        /// Les deux paramètres sont utilisés si il y a une intéraction entre automate et caméra (exemple :  un trigger)
        /// </summary>
        /// <param name="master">Connexion ModbusTCP avec l'automate</param>
        /// <param name="idSlave">ID de la caméra sur l'automate</param>
        public CameraJAI(Master master, ushort idSlave) : base(master, idSlave)
        {
            Jai_FactoryWrapper.EFactoryError error = Jai_FactoryWrapper.EFactoryError.Success;

            // Open the factory with the default Registry database
            error = myFactory.Open("");

            // Search for cameras and update all controls
            UpdateCam();

            if (connected)
            {
                myCamera.NewImageDelegate += new Jai_FactoryWrapper.ImageCallBack(HandleImage);
                myCamera.SkipImageDisplayWhenBusy = false;
                myCamera.StartImageAcquisition(false, 5);
            }
        }


        /// <summary>
        /// Permet de mettre à jour la caméra
        /// </summary>
        private void UpdateCam()
        {
            if (null != myCamera)
            {
                if (myCamera.IsOpen)
                {
                    myCamera.Close();
                }

                myCamera = null;
            }

            // Discover GigE and/or generic GenTL devices using myFactory.UpdateCameraList(in this case specifying Filter Driver for GigE cameras).
            myFactory.UpdateCameraList(Jai_FactoryDotNET.CFactory.EDriverType.FilterDriver);

            // Open the camera - first check for GigE devices
            for (int i = 0; i < myFactory.CameraList.Count; i++)
            {
                myCamera = myFactory.CameraList[i];
                if (Jai_FactoryWrapper.EFactoryError.Success == myCamera.Open())
                {
                    break;
                }
            }

            if (null != myCamera && myCamera.IsOpen)
            {

                // int currentValue = 0;

                // Get the Width GenICam Node
                myWidthNode = myCamera.GetNode("Width");


                /*SetFramegrabberValue("Width", (Int64)myWidthNode.Value);*/

                // Get the Height GenICam Node
                myHeightNode = myCamera.GetNode("Height");

                /*    SetFramegrabberValue("Height", (Int64)myHeightNode.Value);

                    SetFramegrabberPixelFormat();*/

                // Get the GainRaw GenICam Node
                myGainNode = myCamera.GetNode("GainRaw");

                myPxNode = myCamera.GetNode("PixelFormat");
                if (myPxNode != null)
                    pixelFormatType = (Jai_FactoryWrapper.EPixelFormatType)((Jai_FactoryDotNET.CNode.IEnumValue)myPxNode.Value).Value;


                myCamera.GetGain(ref myRGain, ref myGGain, ref myBGain);
                connected = true;
            }
            else
            {
                MessageBox.Show("JAI Camera not found.");
                if(CameraNotFounded != null)
                    CameraNotFounded(this, EventArgs.Empty);
                connected = false;
            }
        }

        /// <summary>
        /// Executer lors d'une reception d'image
        /// </summary>
        /// <param name="ImageInfo"></param>
        void HandleImage(ref Jai_FactoryWrapper.ImageInfo ImageInfo)
        {


            Jai_FactoryWrapper.EFactoryError error = Jai_FactoryWrapper.EFactoryError.Success;


            // We have to allocate the conversion buffer - but we only want to do it once!
            if (myDIBImageInfo.ImageBuffer == IntPtr.Zero)
            {
                error = Jai_FactoryWrapper.J_Image_MallocDIB(ref ImageInfo, ref myDIBImageInfo);
            }
            else Debug.WriteLine("Error JAI: Allocate memory failed");

            // OK - lets check if we have the DIB ImageInfo allocated
            if (myDIBImageInfo.ImageBuffer != IntPtr.Zero)
            {
                // Now we convert from RAW to DIB in order to generate AGB color image
                //error = Jai_FactoryWrapper.J_Image_FromRawToDIB(ref ImageInfo, ref myDIBImageInfo, myRGain,  myGGain,myBGain);
               error = Jai_FactoryWrapper.J_Image_FromRawToDIB(ref ImageInfo, ref myDIBImageInfo, Jai_FactoryWrapper.EColorInterpolationAlgorithm.BayerStandardMultiprocessor, myRGain, myGGain,myBGain );
                if (error == Jai_FactoryWrapper.EFactoryError.Success)
                {
                    image = new Image<Rgba, byte>((int)myDIBImageInfo.SizeX, (int)myDIBImageInfo.SizeY, (int)myDIBImageInfo.SizeX * 4, myDIBImageInfo.ImageBuffer).Convert<Rgb,Byte>();

                    // If the image got created OK then event
                    if (image != null)
                    {
                        AcquisitionNotify(this, EventArgs.Empty);
                    }
                    else Debug.WriteLine("Error JAI: Image is null");
                }
                else Debug.WriteLine("Error JAI: Image Conversion failed");
            }
            else Debug.WriteLine("Error JAI: Image Buffer is null");
            return;
        }

        /// <summary>
        /// Permet de demander à l'automate de d'envoyer un trigger à la caméra
        /// </summary>
        public override void TakeAcquisition()
        {
            if (!connected) { Debug.WriteLine("Error JAI: Impossible to take Acquisition. JAI is disconnected."); return; }

            Send(true);
            System.Threading.Thread.Sleep(50);
            Send(false);
        }


        /// <summary>
        /// Stop le mode acquisition de la caméra
        /// </summary>
        public override void Stop()
        {
            myCamera.StopImageAcquisition(); // throw new NotImplementedException();
        }


        /// <summary>
        /// non implémentée
        /// </summary>
        /// <param name="sensibility"></param>
        public override void SetSensibility(int sensibility)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Start le mode acuistion de la caméra
        /// </summary>
        public override void Start()
        {
            if (!connected) { Debug.WriteLine("Error JAI: Impossible to start. JAI is disconnected."); return; }
            myCamera.StartImageAcquisition(false, 5); 
        }


        /// <summary>
        /// return le bool connected
        /// </summary>
        /// <returns></returns>
        public bool isConnected()
        {
            return connected;
        }


        /// <summary>
        /// Ferme les connexions avec la caméra
        /// </summary>
        public override void Close()
        {
            if (myCamera != null)
            {
                // Stop the acquisition
                myCamera.StopImageAcquisition();
            }

            // Free the BID image info buffer (if it is allocated)
            if (myDIBImageInfo.ImageBuffer != IntPtr.Zero)
            {
                Jai_FactoryWrapper.J_Image_Free(ref myDIBImageInfo);
                myDIBImageInfo.ImageBuffer = IntPtr.Zero;
            }

            if (myCamera != null)
            {
                myCamera.Close();
                return;
            }
        }

        /// <summary>
        /// Destructeur : Ferme les connexions avec la cam
        /// </summary>
        ~CameraJAI()
        {
            Close();
        }
    }
}
