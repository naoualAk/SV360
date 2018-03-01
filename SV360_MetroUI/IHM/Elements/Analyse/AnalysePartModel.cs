using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MetroFramework.Controls;
using SV360.Resources;

namespace SV360.IHM.Elements
{

    /// <summary>
    /// Classe qui permet de présenter un userControl avec :
    ///     un titre
    ///     un encadrement orange
    ///     
    /// Cette classe permet de standardiser l'affichage
    /// </summary>
    public partial class AnalysePartModel : MetroUserControl
    {

        /// <summary>
        /// Cstor : 
        ///     Initialise les donnéees graphiques et ajoute le titre en fonction du paramètre entré.
        /// </summary>
        /// <param name="title"> titre du controle</param>
        private AnalysePartModel(string title)
        {
            InitializeComponent();
            init(title, null);
        }

        private void myControl_Resize(object sender, EventArgs e)
        {
            ((Control)sender).Update();
        }

        /// <summary>
        ///   Cstor : 
        ///   <list type="bulet">
        ///     <item>  Initialise les donnéees graphiques</item>
        ///     <item>  ajoute le titre en fonction du paramètre entré.</item>
        ///     <item>  ajoute le userControl à l'intérieur</item>
        ///  </list>
        /// </summary>
        /// <param name="title"></param>
        /// <param name="uc"></param>
        public AnalysePartModel(string title, UserControl uc)
        {
            InitializeComponent();
            init(title, uc);
        }

        /// <summary>
        ///  Initialise le titre et le usercontrol 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="uc"></param>
        private void init(string title, UserControl uc)
        {
            titleLabel.BackColor = SVColor.orangeUp;
            titleLabel.Text = title;
            tableLayoutPanel.BackColor = SVColor.orangeUp;
            panel.BackColor = SVColor.snow;
            BackColor = SVColor.snow;
            if (uc != null)
                panel.Controls.Add(uc);
            Dock = DockStyle.Fill;
            Resize += UpdateSize;
        }
         
        /// <summary>
        /// Adapte la taille du titre en fonction de la taille de la fenetre.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateSize(object sender, EventArgs e)
        {

           tableLayoutPanel.RowStyles[0].SizeType = SizeType.Absolute;
           tableLayoutPanel.RowStyles[0].Height = 30;

           int size = titleLabel.Height / 2 < 4 ? 4 : titleLabel.Height / 2;
           titleLabel.Font = new Font(titleLabel.Font.FontFamily, size, titleLabel.Font.Style);
        }
    }

}
