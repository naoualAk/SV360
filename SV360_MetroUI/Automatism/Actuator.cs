using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModbusTCP;

namespace SV360.Automatism
{

    /// <summary>
    /// Classe qui permet de controler les actionneurs via l'automate
    /// </summary>
    public class Actuator : Slave
    {

        /// <summary>
        /// ID des actionneurs correspondant au mot mémoire dans l'automate
        /// 
        /// ID of Bit %M'X' in the logic controller M221 
        /// </summary>
        public enum IdSlave
        {
            /// <summary>
            ///  trigger cameraJAI
            /// </summary>
            CameraJAI = 0,
            /// <summary>
            /// Ejection (commande ejection 3, vérin le plus haut).
            /// </summary>
            Ejector = 24,
            /// <summary>
            /// commande vibrateur
            /// </summary>
            Vibrator = 2,
            /// <summary>
            /// trigger camera IDS
            /// </summary>
            CameraIDS = 23,
            /// <summary>
            /// commande convoyeur
            /// </summary>
            Conveyor = 73,
            /// <summary>
            /// commande souffle d'air (nettoyage tapis)
            /// </summary>
            BlowingAir =27,
            /// <summary>
            /// commande pour reset l'ensemble des compteurs 
            /// </summary>
            ResetCounter = 1
        }
        private bool started;

        /// <summary>
        /// setter getter started
        /// </summary>
        public bool Started
        {
            get
            {
                return started;
            }

            set
            {
                started = value;
            }
        }

        /// <summary>
        /// Cstructor: 
        ///     met Started à 0
        /// </summary>
        /// <param name="master">Connexion ModbusTCP avec l'automate</param>
        /// <param name="idSlave">ID de la caméra sur l'automate</param>
        public Actuator(Master master, ushort idSlave) : base(master, idSlave)
        {
            started = false;
        }

        /// <summary>
        /// Méthode qui envoie 1 dans le mot mémoire de l'automate à l'adresse de l'objet (IDSlave)
        /// </summary>
        public void Start()
        {
            Send(true);
            started = true;
        }


        /// <summary>
        /// Méthode qui envoie 0 dans le mot mémoire de l'automate à l'adresse de l'objet (IDSlave)
        /// </summary>
        public void Stop()
        {
            Send(false);
            started = false;
        }


        /// <summary>
        /// Dstructor: Stop l'actionneur
        /// </summary>
        ~Actuator()
        {
            Stop();
        }
    }
}
