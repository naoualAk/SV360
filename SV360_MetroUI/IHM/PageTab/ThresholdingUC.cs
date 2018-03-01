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
using SV360.IHM.Elements.Seuils;
using SV360.Data;
using System.Globalization;
using System.Resources;
using SV360.Resources.lang;

namespace SV360.IHM.PageTab
{
    /// <summary>
    /// Onglet de seuillage :
    ///     Utilisé pour paramétrer les seuils (en largeur, longueur, épaisseur) que la machine separera pendant le tri
    /// </summary>
    public partial class ThresholdingUC : MetroUserControl
    {
        /// <summary>
        /// Appui sur bouton suivant
        /// </summary>
        public event EventHandler nextClicked;
        GlobalThreshold globalThreshold;

        /// <summary>
        /// Event que les classes sont validées.
        ///     Declenché lorsque le nombre de classe est de 4 ou le paramétrage fini
        /// </summary>
        public event EventHandler<List<List<Criteria>>> classValidated;

        /// <summary>
        /// Cstor : 
        ///     Instancie les éléments graphiques et l'element qui réalisera le paramétrage (globalThreshold)
        /// </summary>
        public ThresholdingUC()
        {
            InitializeComponent();
            buttonNext.Click += nextClicked;

            globalThreshold = new GlobalThreshold();
            tableLayoutPanel1.Controls.Add(globalThreshold, 0, 0);
            tableLayoutPanel1.SetColumnSpan(globalThreshold, 2);
            globalThreshold.Dock = DockStyle.Fill;

            Sorting sorting = Sorting.Instance();
        }

        /// <summary>
        /// Affiche une fenetre pour récapituler les critères choisis
        /// </summary>
        /// <param name="criterias"></param>
        private void DisplayResult(List<List<Criteria>> criterias)
        {
            DialogResult dialogResult;
            if (criterias.Count == 0 || criterias[0].Count==0)
            {
                dialogResult = MessageBox.Show(Lang.Text("q_select_no_class") +" \n\n", Lang.Text("a_validate_class"), MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Sorting sorting = Sorting.Instance();
                    sorting.Criterias = criterias;
                }
            }
            else
            {
                string mess = "";
                for (int i = 0; i < criterias.Count; i++)
                {
                    mess += Lang.Text("Class") + " " +(i + 1) + " : ";
                    if (criterias[i].Count > 0)
                    {
                        for (int j = 0; j < criterias[i].Count - 1; j++)
                            mess += criterias[i][j] + " " + Lang.Text("and") + " ";
                        mess += criterias[i][criterias[i].Count - 1] + "\n";
                    }
                }

                dialogResult = MessageBox.Show(Lang.Text("q_select_class") +" \n\n" + mess, Lang.Text("a_validate_class"), MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Sorting sorting = Sorting.Instance();
                    sorting.Criterias = criterias;
                    globalThreshold.DisplayClasses();
                    DialogResult result = MessageBox.Show(Lang.Text("m_class_validated"), Lang.Text("a_validate_class"), MessageBoxButtons.OK);
                }
            }

            // CheckClasses checkClasses = new CheckClasses(mess);
            //checkClasses.Show();
        }

        /// <summary>
        ///  Qd click sur sauvegarder classes
        ///     ouvre une fenetre pour afficher les résultats.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNext_Click(object sender, EventArgs e)
        {
            List<List<Criteria>> criterias = globalThreshold.GetClasses();
            DisplayResult(criterias);

        }

     
        internal void UpdateLanguage()
        {

            buttonNext.Text = Lang.Text("validate_text");
            globalThreshold.UpdateLanguage();
        }

    }
}
