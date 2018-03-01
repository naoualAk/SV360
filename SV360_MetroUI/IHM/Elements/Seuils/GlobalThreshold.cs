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
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using SV360.Resources.lang;

namespace SV360.IHM.Elements.Seuils
{
    /// <summary>
    /// UserControl qui permet d'avoir une IHM pour paramétrer les criteres en fonction d'un arbre de seuil. Chaque seuil va avoir deux enfants et un parent.
    /// </summary>
    public partial class GlobalThreshold : MetroUserControl
    {

        /// <summary>
        /// Seuil origine, le seul sans parent
        /// </summary>
        Threshold seuilOrigin;

        /// <summary>
        /// Cstor  :
        ///     initialise le seuil origine, instancie le tablelayoutpanel qui permettra de gerer la disposition des seuils.
        /// </summary>
        public GlobalThreshold()
        {
            InitializeComponent();
            seuilOrigin = new Threshold();
            seuilOrigin.parent = null;
            tableLayoutPanel1.Controls.Add(seuilOrigin, 0, 0);
            tableLayoutPanel1.SetColumnSpan(seuilOrigin, 8);
            seuilOrigin.Dock = DockStyle.Fill;
            seuilOrigin.NextClicked += CreateNewSon;

           // seuilOrigin.ClassesCreated += DisplayResult;
        }

        private void DisplayResult(object sender, List<List<Criteria>> e)
        {

            string mess="";
            for (int i = 0; i < e.Count; i++)
            {
                mess += Lang.Text("Class") + " " + (i + 1) + ": ";
                for (int j = 0; j < e[i].Count-1; j++)
                    mess += e[i][j] + Lang.Text("and");
                mess += e[i][e[i].Count - 1] + "\n";
            }
            CheckClasses checkClasses = new CheckClasses(mess);
            checkClasses.Show();
        }

        /// <summary>
        /// Retourne la liste des listes de criteres. 
        /// Chaque seuil à une liste de criteres. 
        /// La fonction demande aux enfants sans enfant de donner leurs listes, en commencant à droite.
        /// </summary>
        /// <returns></returns>
        internal List<List<Criteria>> GetClasses()
        {
           // DisplayResult(null, seuilOrigin.RootGetClasses());
            return seuilOrigin.RootGetClasses();
            
            //return null;
        }

        /// <summary> 
        /// Click sur un bouton d'un seuil
        /// Création de deux nouveaux fils en fonction de ce seuil.
        /// Création des données graphiques de ces fils
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateNewSon(object sender, EventArgs e)
        {
            Threshold seuil = (Threshold)sender;
            if (seuil.sonLeft != null)
                return;

            // récupération position du seuil
            TableLayoutPanelCellPosition pos = tableLayoutPanel1.GetCellPosition(seuil);
            // Debug.WriteLine("P " + pos);

            if (pos.Row > 2)
                return;

            // création fils1
            Threshold seuilSon1 = new Threshold(Comp.inf, seuil);
            pos.Row += 1;
            tableLayoutPanel1.Controls.Add(seuilSon1, pos.Column, pos.Row);
            tableLayoutPanel1.SetColumnSpan(seuilSon1, 8 / (pos.Row * 2));
            seuilSon1.Dock = DockStyle.Top;
            seuilSon1.Visible = false;
            seuilSon1.NextClicked += CreateNewSon;

            //création fils2
            Threshold seuilSon2 = new Threshold(Comp.sup, seuil);
            tableLayoutPanel1.Controls.Add(seuilSon2, 8 / (pos.Row * 2) + pos.Column, pos.Row);
            tableLayoutPanel1.SetColumnSpan(seuilSon2, 8 / (pos.Row * 2));
            seuilSon2.Dock = DockStyle.Top;
            seuilSon2.Visible = false;
            seuilSon2.NextClicked += CreateNewSon;

            seuil.sonLeft = seuilSon1;
            seuil.sonRight = seuilSon2;
        }

        /// <summary>
        ///  Affiche le résumé de la liste des listes des critères
        /// </summary>
        internal void DisplayClasses()
        {
            if (seuilOrigin != null)
            {
                seuilOrigin.TreeIsFill = true;
                seuilOrigin.DisplayClass();
            }
        }

        internal void UpdateLanguage()
        {
            seuilOrigin.UpdateLanguage();
        }

    }
}
