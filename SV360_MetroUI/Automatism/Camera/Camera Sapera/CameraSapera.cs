using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DALSA.SaperaLT.SapClassBasic;
using System.Diagnostics;
using System.Windows.Forms;
//using DALSA.SaperaProcessing.CPro;
using Emgu.CV;
using Emgu.CV.Structure;
using ModbusTCP;
using Microsoft.Win32;
using System.IO;
using SV360.Automatism.Camera;
using SV360.Utils;

namespace SV360.Automatism.Camera
{

    /// <summary>
    /// Le code était utilisé pour piloter la caméra JAI à travers le SDK Sapera
    /// Older code for Driver Sapera 
    /// OBSOLETE CODE!!
    /// </summary>
    

   [Obsolete("Utilisé anciennement sur le proto (Version Emeric)")]
    public class CameraSapera : Camera
    {
        //Camera acquisition device object
        private SapAcqDevice acqDevice;
        //Bitmaped image objectF
        private SapBuffer buffers;
        //Acquisition to buffer converter
        private SapAcqDeviceToBuf xfer;
        //Bayer processing object
        private SapBayer bayer;
        //Processing object
        private SapProcessing sapProcessing;
        //Camera server object
        private SapLocation serverLocation;
        //Configuration file name
        private String configFileName;
        //Calibration mode
        private static bool calibration;
        //Acquisition configuration user interface
        //Bayer gain
        private SapDataFRGB gain;

        private string m_ServerName = "";
        private string m_ConfigFile = "";
        private int m_ResourceIndex = 0;
        private string m_ProductDir = "";
        private bool m_ConfigFileEnable=false;

        public string ConfigFile
        {
             
            get
            {
                if (m_ConfigFileEnable)
                    return m_ConfigFile;
                else
                    return "";
            }
        }

        public SapLocation ServerLocation
        {
            get { return new SapLocation(m_ServerName, m_ResourceIndex); }
        }

        public enum ServerCategory
        {
            ServerAll,
            ServerAcq,
            ServerAcqDevice
        };
        private ServerCategory m_ServerCategory;

        private Image<Rgb,Byte> image;

        /// Notification after acquisition
        public override event EventHandler CameraNotFounded;
        public override event EventHandler AcquisitionNotify;

        int nbImg = 0;


        public int NbImg
        {
            get
            {
                return nbImg;
            }
        }

        public Image<Rgb, byte> Image
        {
            get
            {
                return image;
            }
        }

        public override void TakeAcquisition()
        {
            Send(true);
            System.Threading.Thread.Sleep(75);
            Send(false);
        }

        public CameraSapera(Master master, ushort idSlave) : base(master, idSlave)
        {
            
            float coef = 1.7F;
            this.gain = new SapDataFRGB(1.02F * coef, 0.84F * coef, 1.14F * coef);
            Start();
        }

        public override void Start()
        {
            LoadSettings();

            if (m_ServerCategory == ServerCategory.ServerAll)
            {
                if (SapManager.GetResourceCount(new SapLocation(m_ServerName, m_ResourceIndex), SapManager.ResourceType.Acq) > 0)
                    m_ServerCategory = ServerCategory.ServerAcq;
                else if (SapManager.GetResourceCount(new SapLocation(m_ServerName, m_ResourceIndex), SapManager.ResourceType.AcqDevice) > 0)
                    m_ServerCategory = ServerCategory.ServerAcqDevice;
            }
            SaveSettings();

            //Create acquisition objects
            if (!CreateNewObjects())
            {
                throw new Exception("Pre-Processing initialisation failed\nCheck power supply or/and connections");
            }
            calibration = true;
            //Enable grabbing
            if (!xfer.Grab())
            {
                xfer.Abort();
                throw new Exception("A problem has occured during the acquisition");
            }
        }

        private void LoadSettings()
        {
            String KeyPath = "Software\\Teledyne DALSA\\" + Application.ProductName + "\\SapAcquisition";
            RegistryKey RegKey = Registry.CurrentUser.OpenSubKey(KeyPath);
            if (RegKey != null)
            {
                // Read location (server and resource) and file name
                m_ServerName = RegKey.GetValue("Server", "").ToString();
                m_ResourceIndex = (int)RegKey.GetValue("Resource", 0);
                if (File.Exists(RegKey.GetValue("ConfigFile", "").ToString()))
                    m_ConfigFile = RegKey.GetValue("ConfigFile", "").ToString();
            }
        }

        private void SaveSettings()
        {
            String KeyPath = "Software\\Teledyne DALSA\\" + Application.ProductName + "\\SapAcquisition";
            RegistryKey RegKey = Registry.CurrentUser.CreateSubKey(KeyPath);
            // Write config file name and location (server and resource)
            RegKey.SetValue("Server", m_ServerName);
            RegKey.SetValue("ConfigFile", m_ConfigFile);
            RegKey.SetValue("Resource", m_ResourceIndex);
        }



        private bool CreateNewObjects()
        {
            //Get camera server parameters
            serverLocation = ServerLocation;
            configFileName = ConfigFile;

            // define on-line object
            acqDevice = new SapAcqDevice(serverLocation, configFileName);
            if (SapBuffer.IsBufferTypeSupported(serverLocation, SapBuffer.MemoryType.ScatterGather))
            {
                buffers = new SapBufferWithTrash(2, acqDevice, SapBuffer.MemoryType.ScatterGather);
            }
            else
            {
                buffers = new SapBufferWithTrash(2, acqDevice, SapBuffer.MemoryType.ScatterGatherPhysical);
            }
            //initialize acquisition objects
            xfer = new SapAcqDeviceToBuf(acqDevice, buffers);
            //initialize Bayer filter
            bayer = new SapBayer(buffers);
            bayer.Method = SapBayer.CalculationMethod.Method3;
            bayer.Align = SapBayer.AlignMode.RGGB;
            bayer.WBGain = gain;

            //event for transfer
            xfer.Pairs[0].EventType = SapXferPair.XferEventType.EndOfFrame;
            xfer.XferNotify += new SapXferNotifyHandler(SapAcquisitionNotify);
            xfer.XferNotifyContext = this;
            sapProcessing = new BayerProcessing(buffers, bayer, new SapProcessingDoneHandler(BayerProcessingCallback), this);
            if (!CreateObjects())
            {
                DisposeObjects();
                return false;
            }
            bayer.LutEnable = true;
            return true;
        }

        //Call when an acquisition occured
        void SapAcquisitionNotify(object sender, SapXferNotifyEventArgs argsNotify)
        {
            CameraSapera cameraSapera = argsNotify.Context as CameraSapera;
            // If grabbing in trash buffer, do not execute bayer processing           
            if (!argsNotify.Trash)
            {
                cameraSapera.sapProcessing.ExecuteNext();
            }
        }

        //Call when the bayer processing is finished
        void BayerProcessingCallback(object sender, SapProcessingDoneEventArgs pInfo)
        {
            CameraSapera cameraSapera = pInfo.Context as CameraSapera;

           // image = new Image<Bgr, byte>(ImageConverter.GetBitmapFromCProImage(cameraSapera.GetImage())).Convert<Rgb,Byte>();
            nbImg++;

            AcquisitionNotify(cameraSapera, EventArgs.Empty);
        }

        // Call Create Object 
        private bool CreateObjects()
        {
            // Create acquisition object
            if (acqDevice != null && !acqDevice.Initialized)
            {
                if (acqDevice.Create() == false)
                {
                    DestroyObjects();
                    return false;
                }
            }
            // Enable/Disable bayer conversion
            // This call may require to modify the acquisition output format.
            // For this reason, it has to be done after creating the acquisition object but before
            // creating the output buffer object.
            bayer.Enable(true, false);

            // Create buffer object
            if (buffers != null && !buffers.Initialized)
            {
                if (buffers.Create() == false)
                {
                    DestroyObjects();
                    return false;
                }
                buffers.Clear();
            }

            // Create bayer object
            if (bayer != null && !bayer.Initialized)
            {
                if (bayer.Create() == false)
                {
                    DestroyObjects();
                    return false;
                }
            }

            // Create Xfer object
            if (xfer != null && !xfer.Initialized)
            {
                if (xfer.Create() == false)
                {
                    DestroyObjects();
                    return false;
                }
                xfer.AutoEmpty = false;
            }

            // Create processing object
            if (sapProcessing != null && !sapProcessing.Initialized)
            {
                if (!sapProcessing.Create())
                {
                    DestroyObjects();
                    return false;
                }

                sapProcessing.AutoEmpty = true;
            }
            return true;
        }

        public override void Stop()
        {
            DestroyObjects();
        }

        void DestroyObjects()
        {
            if (xfer != null && xfer.Initialized)
            {
                xfer.Destroy();
            }
            if (sapProcessing != null && sapProcessing.Initialized)
            {
                sapProcessing.Destroy();
            }
            if (bayer != null && bayer.Initialized)
            {
                bayer.Destroy();
            }
            if (buffers != null && buffers.Initialized)
            {
                buffers.Destroy();
            }
            if (acqDevice != null && acqDevice.Initialized)
            {
                acqDevice.Destroy();
            }
        }

        private void DisposeObjects()
        {
            if (xfer != null)
            {
                xfer.Dispose();
                xfer = null;
            }
            if (sapProcessing != null)
            {
                sapProcessing.Dispose();
                sapProcessing = null;
            }
            if (bayer != null)
            {
                bayer.Dispose();
                bayer = null;
            }
            if (buffers != null)
            {
                buffers.Dispose();
                buffers = null;
            }
            if (acqDevice != null)
            {
                acqDevice.Dispose();
                acqDevice = null;
            }
        }

      /*  public CProImage GetImage()
        {
            // Extract information from Sapera LT’s SapBuffer object
            SapBuffer m_Buf = bayer.BayerBuffer;
            int width = m_Buf.Width;
            int height = m_Buf.Height;
            CProData.FormatEnum format;
            switch (m_Buf.Format)
            {
                case SapFormat.Mono8:
                    format = CProData.FormatEnum.FormatUByte; break;
                case SapFormat.Mono16:
                    format = CProData.FormatEnum.FormatUShort; break;
                default:
                    format = CProData.FormatEnum.FormatRgb; break;
            };
            IntPtr data;
            m_Buf.GetAddress(out data);

            CProImage image = new CProImage(width, height, format, data, false);
            // Construct CProImage wrapper object
            return image;
        }*/

        public override void SetSensibility(int sensibility)
        {
            //Set Bayer gain for low color sensibility
            if (sensibility == 0)
            {
                //this.gain = new SapDataFRGB(1.99F, 1.4F, 2.1F);
                float coef = 1.7F;
                this.gain = new SapDataFRGB(1.02F * coef, 0.84F * coef, 1.14F * coef);
            }
            //Set Bayer gain for high color sensibility
            else if (sensibility == 1)
            {
                this.gain = new SapDataFRGB(2.7F, 1.9F, 2.85F);
            }
        }

  

        public override void Close()
        {
            throw new NotImplementedException();
        }

    }
}
