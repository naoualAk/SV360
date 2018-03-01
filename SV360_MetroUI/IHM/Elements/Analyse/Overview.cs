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
using SV360.Resources;
using SV360.Data;
using SV360.IHM.Elements.Seuils;

namespace SV360.IHM.Elements.Analyse
{

    /// <summary>
    /// UserControl qui affiche les données générales d'une liste de grains : 
    ///     <list type="bulet">
    /// <item>Repartition de la classe</item>    
    /// <item>Critères de la classes</item>
    /// <item>Nombre de grains dans la classe</item>
    /// </list>
    /// </summary>
    public partial class Overview : MetroUserControl
    {
        /// Graphique camembert pour la répartition des classes
        ChartPie chartPie;

        /// UserControl pour afficher le nombre de grains
        SeedCounter seedCounter;
        int numClass;     

        /// <summary>
        ///  Cstor : 
        ///     Affiche les données générales de toute les classes pour un liste de grains. 
        ///     
        /// </summary>
        /// <param name="seeds"> liste de grains</param>
        public Overview(List<Seed> seeds) : this(seeds, -1)
        {
        }

        /// <summary>
        ///  Cstor : 
        ///     Affiche les données générales pour une classe donnée pour un liste de grains. 
        /// </summary>
        /// <param name="seeds">liste de grains</param>
        /// <param name="numClass">numéro de la classes (doit être compris entre -1 et 4) -1 = total, 0 = indéfini, autres = numéro de la classe</param>
        public Overview(List<Seed> seeds, int numClass)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            BackColor = SVColor.snow;
            tableLayoutPanel1.BackColor = SVColor.snow;
            overviewLabel.ForeColor = SVColor.asbestos;


            seeds = (from s in seeds
                     where s.Length > 1 && s.Length < 15
                            && s.Width > 1 && s.Width < 15
                            && s.Thickness > 1 && s.Thickness < 15
                     select s).ToList();

            this.numClass = numClass;

            updateOverviewLabel(numClass);
            Resize += new EventHandler(UpdateSize);

            chartPie = new ChartPie();
            chartPie.Set(seeds, numClass);
            tableLayoutPanel1.Controls.Add(chartPie, 2, 0);

            seedCounter = new SeedCounter();
            seedCounter.Set(CountbyClass(seeds, numClass));
            tableLayoutPanel1.Controls.Add(seedCounter, 3, 0);
        }


        /// <summary>
        /// renvoi le nombre de grain en fonction d'une classe donnée et d'une liste de grains
        /// </summary>
        /// <param name="seeds"> liste de grains</param>
        /// <param name="numClass">classe donnée (doit être compris entre -1 et 4) -1 = total, 0 = indéfini, autres = numéro de la classe</param>
        /// <returns></returns>
        private int CountbyClass(List<Seed> seeds, int numClass)
        {
            if (numClass == -1) return seeds.Count;
            if (numClass < 0 || numClass > 4) return 0;
            var seedsClass = from s in seeds
                             where s.NumClass == (Seed.ENumClass)numClass
                             select s;
            return seedsClass.Count();
        }


        /// <summary>
        /// Affiche les criteres de la classe voulue
        /// </summary>
        /// <param name="numClass">classe voulue</param>
        private void updateOverviewLabel(int numClass)
        {
            Sorting sorting;
            sorting = Sorting.Instance();

            if (numClass == -1)
                overviewLabel.Text = "General";
            else
            {
                if (sorting.Criterias == null)
                    return;

                string mess = "";
               
                 mess += "Classe " + (numClass) + " : \n  ";
                   for (int j = 0; j < sorting.Criterias[numClass-1].Count-1; j++)
                       mess += sorting.Criterias[numClass - 1][j] + "\n  ";
                   mess += sorting.Criterias[numClass - 1][sorting.Criterias[numClass - 1].Count - 1] + "\n";
                
                //overviewLabel.Text = "Classe 1 : \n  Epaisseur>4 &\n  Largeur>5 &\n  Longueur>6";
                overviewLabel.Text = mess;
            }
        }

        /// <summary>
        /// Adapte la taille des écriture en fonction de la taille du userControl.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateSize(object sender, EventArgs e)
        {
            if (overviewLabel != null)
            {
                int min = overviewLabel.Size.Height < overviewLabel.Size.Width ? overviewLabel.Size.Height : overviewLabel.Size.Width;
                int size = min / 12 < 5 ? 5 : min / 12;
                overviewLabel.Font = new Font(Font.FontFamily, size, Font.Style);
            }
        }


    }
}
