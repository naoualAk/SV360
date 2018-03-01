using MetroFramework.Controls;
using MetroFramework.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SV360.IHM.Elements
{

    /// <summary>
    /// Bouton graphique qui peut etre checked ou non
    /// </summary>
    public partial class SVButton : MetroButton
    {
        /// <summary>
        /// checked ou non
        /// </summary>
        public bool isChecked = false;
        /// <summary>
        /// Evenement de check
        /// </summary>
        public event EventHandler Checked;

        /// <summary>
        /// Cstr : Instancie les graphismes et events
        /// </summary>
        public SVButton() : base()
        {
            this.Click += ChangeColor;

            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
        }


        private void ChangeColor(object sender, EventArgs e)
        {
            if (!isChecked)
            {
                this.ForeColor = Color.White;
                this.BackColor = Color.Gray;
                isChecked = true;
                Checked(this, EventArgs.Empty);
            }
            else
            {
                this.ForeColor = Color.Black;
                this.BackColor = Color.White;
                isChecked = false;
                Checked(this, EventArgs.Empty);
            }
        }
    }
}

