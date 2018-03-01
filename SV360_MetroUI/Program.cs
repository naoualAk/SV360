using SV360.IHM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SV360
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        /// 

        //Windows Form user interface
        //static UI ui;
        static MainForm mainForm;
       
        //Seed object
        // static Seed_View seed;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ui = new UI();

            //try
          //  {
                mainForm = new MainForm();
                Application.Run(mainForm);
          /*  }
            catch(System.Exception e)
            {
                Exception.LogMessage("The mainForm throwed an exception: " + e.ToString());
            }
            finally
            {
                if (mainForm != null)
                    mainForm.Close();
            }*/

            //Get seed object
            // seed = Seed_View.Instance();
            //Attach user interface as an observer
            // seed.Attach(ui);

            //Run the application
            //Application.Run(ui);
        }

       
    }
}
