using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModbusTCP;
using Emgu.CV;
using Emgu.CV.Structure;

namespace SV360.Automatism.Camera
{
    /// <summary>
    /// Abstract class for 
    /// the minimal atttribute and method for camera
    /// </summary>
    public abstract class Camera : Slave
    {
        /// <summary>
        /// Acquisition Notify
        /// </summary>
        public abstract event EventHandler AcquisitionNotify;

        /// <summary>
        ///  Notify when the camera is not founded during the starting
        /// </summary>
        public abstract event EventHandler CameraNotFounded;

        /// <summary>
        /// Image of acquisition 
        /// </summary>
        protected Image<Rgb, Byte> image;

        /// <summary>
        /// Getter of image
        /// </summary>
        public Image<Rgb, byte> Image
        {
            get
            {
                return image;
            }
        }

        /// <summary>
        /// Constructeur d'un objet camera
        /// 
        /// Les deux paramètres sont utilisés si il y a une intéraction entre automate et caméra (exemple :  un trigger)
        /// </summary>
        /// <param name="master">Connexion ModbusTCP avec l'automate</param>
        /// <param name="idSlave">ID de la caméra sur l'automate</param>
        public Camera(Master master, ushort idSlave) : base(master, idSlave)
        {
        }

        /// <summary>
        /// trigger an acquisition 
        /// </summary>
        public abstract void TakeAcquisition();

        /// <summary>
        /// stop the camera
        /// </summary>
        public abstract void Stop();

        /// <summary>
        /// start the camera
        /// </summary>
        public abstract void Start();

        /// <summary>
        /// close camera and dispose memory
        /// </summary>
        public abstract void Close();


        /// <summary>
        /// setter of sensibility camera
        /// </summary>
        /// <param name="sensibility"></param>
        public abstract void SetSensibility(int sensibility);
    }
}
