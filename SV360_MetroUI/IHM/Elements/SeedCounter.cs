using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Controls;

namespace SV360.IHM.Elements
{
    public partial class SeedCounter : UserControl
    {

        /// <summary>
        /// Affiche le nombre de grains 
        /// </summary>
        public SeedCounter()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;

            label.Text = "Analysée: 1000\r\nSCO : 1000 \r\nSCA:1000";
        }


        /// <summary>
        /// Affiche le nombre de grains
        /// </summary>
        /// <param name="value"></param>
        public void Set(int value)
        {
            label.Text = "         " + value.ToString();
        }

        private void label_Click(object sender, EventArgs e)
        {

        }
    }
}
