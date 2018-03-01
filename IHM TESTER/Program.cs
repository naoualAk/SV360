using SV360.IHM;
using SV360.Resources.lang;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IHM_TESTER
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Lang.ResMan = new ResourceManager("SV360.Resources.lang.Res", typeof(MainForm).Assembly);
            Lang.CI = CultureInfo.CreateSpecificCulture("fr");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Sorting
            Application.Run(new Form1());

            //analyse
             //Application.Run(new Form2());

            //Settings
            //Application.Run(new SettingsForm(null));
        }
    }
}
