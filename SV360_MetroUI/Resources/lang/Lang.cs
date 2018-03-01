using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace SV360.Resources.lang
{
    /// <summary>
    /// Classe permettant de traduire de l'anglais ou français à l'aide de fichier .resx
    /// Chaque texte doit avoir deux références: une anglaise une francaise dans les fichiers .resx pour pouvoir effectuer la traduction pendnat l'execution du programme
    /// </summary>
    public static class Lang
    {
        private static ResourceManager resMan = null;    // declare Resource manager to access to specific cultureinfo
        private static CultureInfo cI = null;

        /// <summary>
        /// Getter Setter de la langue
        /// </summary>
        public static CultureInfo CI
        {
            get
            {
                return cI;
            }

            set
            {
                cI = value;

            }
        }

        /// <summary>
        /// Getter et setter du manager de ressources
        /// </summary>
        public static ResourceManager ResMan
        {
            get
            {
                return resMan;
            }

            set
            {
                resMan = value;

            }
        }

        /// <summary>
        /// Ecriture d'un texte en anglais ou francais dépendant des fichiers .resx. 
        /// La méthode recherche si le texte es présent dans les fichiers .resx. 
        /// Si oui, il renvoie en fonction de la cultureInfo un texte français ou anglais
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        static public string Text(string txt)
        {
            if (CI == null)
            {
                CultureInfo.CreateSpecificCulture("fr");
            }
            if (ResMan == null)
            {
                new ResourceManager("SV360.Resources.lang.Res", typeof(IHM.MainForm).Assembly);
            }
            string res;
            try
            {
                res = resMan.GetString(txt, cI);
            }
            catch(Exception e)
            {
                res = txt;
                Debug.WriteLine("Error Lang: \n" + e);
            }
            
            return res;
        }
    }
}
