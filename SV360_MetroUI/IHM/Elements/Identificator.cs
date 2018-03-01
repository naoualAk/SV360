using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SV360.IHM.Elements
{
    public partial class Identificator : UserControl
    {
        /// <summary>
        /// est autorisé
        /// </summary>
        public EventHandler isIdentificated;

        public Identificator()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Appuie sur bouton valider : Permet de rentrer dans les settings (id : admin mdp : Vilmorin2016)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_valider_Click(object sender, EventArgs e)
        {
            if(textBox_id.Text.Equals("admin") && textBox_mdp.Text.Equals("Vilmorin2016"))
            {
                try
                {
                    isIdentificated(this, null);
                }
                catch { }
            }
        }
    }
}
