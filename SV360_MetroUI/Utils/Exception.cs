using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SV360
{
    /// <summary>
    /// Calsse qui permet de gerer les exceptions afin de les enregistrer dans un fichier log.
    /// </summary>
    class Exception : System.Exception
    {

        public Exception(string message, Exception innerException) : base(message, innerException) { }


        /// <summary>
        /// Crée une exception. Cette exception va écrire les détails de l'exception dans le fichier Directory.GetCurrentDirectory() + @"\SV360_log.txt".
        /// Cette exception peut aussi afficher un message à l'utilisateur pour le prévenir de l'erreur
        /// </summary>
        /// <param name="message">message écrit dans le fichier log ou afficher à l'utilisateur si messBox==null ET messageBox=true</param>
        /// <param name="messageBox">affichage du message à l'utilisateur</param>
        /// <param name="messBox">message destiné à l'utilisateur</param>
        public Exception(string message, bool messageBox = false, string messBox = null) : base(message)
        {

            if (messageBox)
                if (messBox == null)
                    MessageBox.Show(message);
                else
                    MessageBox.Show(messBox);

            DateTime dateOnly = DateTime.Now;
            using (System.IO.StreamWriter file =
        new System.IO.StreamWriter(Directory.GetCurrentDirectory() + @"\SV360_log.txt", true))
            {
                file.Write(dateOnly.ToString("MM/dd/yyyy HH:mm") + " : ");
                file.WriteLine(message);
            }
        }

        /// <summary>
        /// Permet d'écrire dans le ficher log sans lancer une exception dans le fichier Directory.GetCurrentDirectory() + @"\SV360_log.txt".
        /// Peut aussi afficher un message à l'utilisateur pour le prévenir de l'erreur
        /// </summary>
        /// <param name="message">message écrit dans le fichier log ou afficher à l'utilisateur si messBox==null ET messageBox=true</param>
        /// <param name="messageBox">affichage du message à l'utilisateur</param>
        /// <param name="messBox">message destiné à l'utilisateur</param>
        public static void LogMessage(string message, bool messageBox = false, string messBox = null)
        {
            if (messageBox)
                if (messBox == null)
                    MessageBox.Show(message);
                else
                    MessageBox.Show(messBox);

            DateTime dateOnly = DateTime.Now;
            using (System.IO.StreamWriter file =
        new System.IO.StreamWriter(Directory.GetCurrentDirectory() + @"\SV360_log.txt", true))
            {
                file.Write(dateOnly.ToString("MM/dd/yyyy HH:mm") + " : ");
                file.WriteLine(message);
            }
        }
    }
}
