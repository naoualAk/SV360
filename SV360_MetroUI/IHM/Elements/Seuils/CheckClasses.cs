using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SV360.IHM.Elements.Seuils
{

    /// <summary>
    /// Classes qui permet d'afficher les criteres choisis par l'utilisateur.
    /// </summary>
    public partial class CheckClasses : Form
    {


        private CheckClasses()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Cstor : 
        ///     Affiche le message donnée
        /// </summary>
        /// <param name="mess">message</param>
        public CheckClasses(string mess)
        {
            InitializeComponent();
            Print(mess);
        }


        /// <summary>
        ///  Affiche un message dans la fenetre
        /// </summary>
        /// <param name="mess"></param>
        public void Print(string mess)
        {
            richTextBox1.Text = mess;
        }
    }
}
