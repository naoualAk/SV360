using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SV360.IHM.Elements
{
    public partial class IPConfig : Form
    {
        /// <summary>
        /// Fenetre permettant de changer IP avec laquelle le programme cherchera l'automate
        /// </summary>
        public IPConfig()
        {
            InitializeComponent();
            textBox1.Text = Properties.Settings.Default.ip_addr ;

            this.FormClosed += Aborted();
        }

        private FormClosedEventHandler Aborted()
        {
            this.DialogResult = DialogResult.Abort;
            return null;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
               
        }

        /// <summary>
        /// Si appuie bouton OK : Sauvegarde de l'ip, ferme la fenetre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Properties.Settings.Default.ip_addr = textBox1.Text;
            Properties.Settings.Default.Save();
            Close();
        }
    }
}
