using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModbusTCP;
using SV360.Automatism.Camera;
using static SV360.Automatism.Actuator;
using SV360.IHM.Elements;
using System.Windows.Forms;

namespace SV360.Automatism
{

    /// <summary>
    /// Classe qui permet d'englober tout les périphériques liés à la machine :
    /// <list type="bullet">
    ///     <item>vibrator</item>
    ///     <item>ejection</item>
    ///     <item>convoyeur</item>
    ///     <item>caméra</item>
    /// </list> 
    /// </summary>
    public class SystemController
    {
        //Run/Stop system information
        private bool isRunning;
        //PLC Modbus network master object
        private Master MBmaster;

        public Master Master()
        {
            return MBmaster;
        }

        Actuator vibrator;
        Actuator ejector;
        Actuator conveyor;
        Actuator blowingAir;
        Actuator counterReset;
        EjectionMem ejectorMem;

        // public CameraJAI cameraJAI;

        /// <summary>
        /// Objet qui permet de faire le lien avec une caméra IDS
        /// </summary>
        public CameraIDS cameraIDS;

        /// <summary>
        /// bool qui permet de savoir si le fond a bien été pris
        /// </summary>
        public bool hasTakeIDSBackGround = false;

       
        /// <summary>
        /// Cstor : 
        ///     Réalise la connexion à l'automate.
        ///     Si la connexion n'est pas réussi, il ya un lancement de la fenetre IPConfig pour modifier l'ip.
        ///     Si la connexion réussi, iniatialise tout les périphériques.
        ///     Stop le vibrateur et le convoyeur.
        /// </summary>
        public SystemController()
        {
            isRunning = false;
            bool isConnected = false;
           
            while (!isConnected)
            {
                //AutoClosingMessageBox.Show("Connexion à l'automate", "Connexion", 1500);
                CloseMessageBox closeMessageBox = new CloseMessageBox();
                try
                {
                    // Create new modbus master associated with the PLC IP address
                    MBmaster = new Master(Properties.Settings.Default.ip_addr, 502);
                    isConnected = true;
                }
                catch (SystemException error)
                {
                    closeMessageBox.Close();

                    // Ouverture de la fenetre pour demander un nouvel ip
                    IPConfig ip = new IPConfig();
                    DialogResult res = ip.ShowDialog();
                    if (res != DialogResult.OK)
                        if (System.Windows.Forms.Application.MessageLoop)
                        {
                            // WinForms app
                            System.Windows.Forms.Application.Exit();
                        }
                        else
                        {
                            // Console app
                            System.Environment.Exit(1);
                        }

                    isConnected = false;
                    //System.Windows.Forms.MessageBox.Show("Impossible de se connecter au système. Veuillez vérifier les branchements. ");
                    //throw new Exception("Cannot connect to the system's PLC.\nCheck your connections and try again.\n\nError details :\n" + error.ToString());
                }
                finally
                {
                    closeMessageBox.Close();
                }
            }

            vibrator = new Actuator(MBmaster, (ushort)IdSlave.Vibrator);
            ejector = new Actuator(MBmaster, (ushort)IdSlave.Ejector);
            conveyor = new Actuator(MBmaster, (ushort)IdSlave.Conveyor);
            blowingAir = new Actuator(MBmaster, (ushort)IdSlave.BlowingAir);
            ejectorMem = new EjectionMem(MBmaster);

            counterReset = new Actuator(MBmaster, (ushort)IdSlave.ResetCounter);
            //Reset of all counters
            counterReset.Start();

            //cameraJAI = new CameraJAI(MBmaster, (ushort)IdSlave.CameraJAI);
            cameraIDS = new CameraIDS(MBmaster, (ushort)IdSlave.CameraIDS);
            vibrator.Stop();
            conveyor.Stop();
        }


        public void ResetCounters()
        {
            counterReset.Start();
        }

        /// <summary>
        /// Dstor : 
        ///     Ferme tout les périphériques
        /// </summary>
        ~SystemController()
        {
            Close();
        }


        /// <summary>
        /// Permet d'activer :
        /// <list type="bullet">
        ///     <item>la caméraIDS</item>
        ///     <item>le convoyeur</item>
        ///     <item>le vibrateur</item>
        ///     <item>le souffle d'air (nettoyage du tapis)</item>
        ///     </list>
        /// </summary>
        public void Run()
        {
            //Start preprocessing
            isRunning = true;

            //   if(cameraJAI.isConnected()) cameraJAI.Start();
            cameraIDS.Start();
            if (!conveyor.Started) conveyor.Start();
            if (!vibrator.Started) vibrator.Start();
            if (!blowingAir.Started) blowingAir.Start();
        }


        /// <summary>
        /// Demande d'activation du convoyeur à l'automate
        /// Attente de 1s pour sa mise en route
        /// </summary>
        public void StartConveyor()
        {
            if (!conveyor.Started) conveyor.Start();
            System.Threading.Thread.Sleep(1000);

        }

        /// <summary>
        /// Stop le convoyeur
        /// </summary>
        public void StopConveyor()
        {
            conveyor.Stop();
        }

        /// <summary>
        /// Getter de l'activation du convoyeur
        /// </summary>
        /// <returns>true : convoyeur est activé
        /// false : convoyeur ne tourne pas</returns>
        public bool ConveyorIsStarted()
        {
            return conveyor.Started;
        }

        /// <summary>
        /// Actionne le vibrateur
        /// </summary>
        public void StartVibrator()
        {
            if (!vibrator.Started) vibrator.Start();
        }

        /// <summary>
        /// Stop le vibrateur
        /// </summary>
        public void StopVibrator()
        {
            vibrator.Stop();
        }

        /// <summary>
        /// Getter de l'activation du vibrateur
        /// </summary>
        /// <returns>true : vibrateur est activé
        /// false : vibrateur ne fonctionne pas</returns>
        public bool VibratorIsStarted()
        {
            return vibrator.Started;
        }

        /// <summary>
        /// Déclenche la prise d'acquisitions du background
        ///     5 imgs sont prises par intervalle de 0.5s 
        /// </summary>
        public void TakeAcquisitionBackGround()
        {
           

            //   if (cameraJAI.isConnected()) cameraJAI.TakeAcquisition();
            System.Threading.Thread.Sleep(1000);
            // 5 acquisition for background moy
            for (int i = 0; i < 5; i++)
            {
                System.Threading.Thread.Sleep(500);
                cameraIDS.TakeAcquisition();
            }
            hasTakeIDSBackGround = true;
        }

        /// <summary>
        /// Setter pour enregistrer une valeur dans la file des prochaines sorties dans l'automate
        /// </summary>
        /// <param name="value"></param>
        public void SetNextEject(int value)
        {
            ejectorMem.SetClass(value);
        }

        /// <summary>
        /// Permet de lancer l'ejection (activer les vérins)
        /// L'automate ejectera par rapport à la première valeur dans la file des sorties.
        /// </summary>
        /// <param name="OnOff">Commencer ejection = true, stop ejection = false</param>
        public void Eject(bool OnOff)
        {
            //Active the ejection
            if (OnOff) ejector.Start();
            else ejector.Stop();
        }


        /// <summary>
        /// getter que le système est en fonctionnement
        /// </summary>
        /// <returns>true: en fonctionnement
        /// false: non fonctionnement</returns>
        public bool IsRunning()
        {
            return isRunning;
        }

        /// <summary>
        /// Stop les actionneurs mécaniques: 
        /// <list type="bullet">
        ///     <item>convoyeur</item>
        ///     <item>vibrateur</item>
        ///     <item>souffle d'air (nettoyage)</item>
        ///     </list>
        /// </summary>
        public void StopActuators()
        {
            isRunning = false;
            //stop vibrator
            conveyor.Stop();
            vibrator.Stop();
            blowingAir.Stop();
            ejector.Stop();
            // if (cameraJAI.isConnected()) cameraJAI.Stop();
            //  cameraIDS.Stop();
        }

   
        /// <summary>
        /// Ferme toutes les communications avec les périphériques
        /// </summary>
        public void Close()
        {
            //run vibrator
            isRunning = false;
            if(conveyor != null)conveyor.Stop();
            if (vibrator != null) vibrator.Stop();
            if (blowingAir != null) blowingAir.Stop();
            // if (cameraJAI.isConnected()) cameraJAI.Close();
            //  not implemented 
            //cameraIDS.Close();
        }


    }
}
