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
using SV360.IHM.Elements;
using System.Diagnostics;
using SV360.Data;
using SV360.IHM.Elements.Analyse;
using System.Resources;
using SV360.Utils;
using SV360.Resources.lang;

namespace SV360.IHM.PageTab
{

    /// <summary>
    /// Classe qui controle l'onglet analyser.
    /// L'interface présente les données des graines qui sont analysées pendant la phase de passage des lots.
    /// La classe contient : 
    ///     <list type="bullet">
    ///     <item>la partie General de chaque classe (Classe Overview)</item>
    ///     <item>la partie graphiques</item>
    ///     </list> 
    ///    
    /// </summary>
    public partial class AnalysisUC : MetroUserControl
    {

        Overview overview;

        /// <summary>
        /// AnalysePartModel est une surcouche graphique pour présenter les analyses :
        ///     avec un titre 
        ///     avec un encadrement
        /// </summary>
        AnalysePartModel overviewPart;

        ChartsAnalyse charts;
        AnalysePartModel chartsPart;

        List<Seed> seeds;

        MetroLabel lb;
        bool isActivated = false;
        int nbClass;

        /// <summary>
        /// Cstr de l'interface.
        /// </summary>
        public AnalysisUC()
        {
            InitializeComponent();
            Enable(false);

            toolStripButton1.Visible = false;
            toolStripButton2.Visible = false;
        }


        /// <summary>
        /// Affiche les données sur l'interface par rapport à une liste de grains et au seuillage entré
        /// </summary>
        /// <param name="seeds">liste des grains</param>
        /// <param name="OnOff">activer l'affichage ou non de l'interface</param>
        public void Set(List<Seed> seeds, bool OnOff)
        {
            Sorting sorting;
            sorting = Sorting.Instance();

            if (sorting != null && sorting.Criterias != null && sorting.Criterias.Count != 0)
                nbClass = sorting.Criterias.Count + 1;
            else
                nbClass = 2;

            Set(seeds);
            Enable(OnOff);
            isActivated = OnOff;
        }

        /// <summary>
        /// Instancie les parties de description générale et graphiques 
        /// </summary>
        /// <param name="seeds">liste de grains</param>
        private void Set(List<Seed> seeds)
        {
            statsTableLayout.Controls.Clear();

            this.seeds = seeds;

            overview = new Overview(seeds);
            overviewPart = new AnalysePartModel(Lang.Text("general"), overview);
            statsTableLayout.Controls.Add(overviewPart, 0, 0);

            charts = new ChartsAnalyse(seeds);
            chartsPart = new AnalysePartModel(Lang.Text("morpho_analysis"), charts);
            statsTableLayout.Controls.Add(chartsPart, 0, 1);

            Sorting sort = Sorting.Instance();

            if (sort.Criterias.Count > 0)
                menuButtonC1.Visible = true;
            else menuButtonC1.Visible = false;

            if (sort.Criterias.Count > 1)
                menuButtonC2.Visible = true;
            else menuButtonC2.Visible = false;

            if (sort.Criterias.Count > 2)
                menuButtonC3.Visible = true;
            else menuButtonC3.Visible = false;

            if (sort.Criterias.Count > 3)
                menuButtonC4.Visible = true;
            else menuButtonC4.Visible = false;
        }

        /// <summary>
        /// Update de l'interface 
        /// Affiche l'interface par rapport à un numéro de classe donnée.
        /// 
        /// </summary>
        /// <param name="numClass">numéro de la classe (doit être compris entre -1 et 4) -1 correspondant à l'ensemble des graines, le reste a des classes de <see cref="Seed.ENumClass"/></param>
        public void Update(int numClass)
        {
            if (!CheckSeeds(numClass)) return;

            if (seeds == null || seeds.Count == 0) return;

            if (numClass < -1 || numClass > 4) return;

            List<Seed> s;
            if (numClass == -1)
                s = seeds;
            else
                s = SortSeedsbyClass(seeds, numClass);

            statsTableLayout.Controls.Clear();

            overview = new Overview(seeds, numClass);
            overviewPart = new AnalysePartModel(Lang.Text("general"), overview);
            statsTableLayout.Controls.Add(overviewPart, 0, 0);

            charts = new ChartsAnalyse(s);
            chartsPart = new AnalysePartModel(Lang.Text("morpho_analysis"), charts);
            statsTableLayout.Controls.Add(chartsPart, 0, 1);
        }

        private List<Seed> SortSeedsbyClass(List<Seed> seeds, int numClass)
        {
            if (numClass < 0 || numClass > 4) return null;
            var seedsClass = from s in seeds
                             where s.NumClass == (Seed.ENumClass)numClass
                             select s;
            return seedsClass.ToList();
        }
        /// <summary>
        /// active ou non l'interface
        ///     Si l'interface est non activée. Affichage d'un texte pour informer que l'analyse est impossible.
        /// </summary>
        /// <param name="OnOff"></param>
        public void Enable(bool OnOff)
        {
            isActivated = OnOff;
            if (!OnOff)
            {
                lb = new MetroLabel();

                lb.Text = Lang.Text("impossible_analysis");
                Controls.Add(lb);
                lb.Dock = DockStyle.Fill;
                lb.ForeColor = Color.Black;
                lb.TextAlign = ContentAlignment.MiddleCenter;

                menuButtonC1.Visible = false;
                menuButtonC2.Visible = false;
                menuButtonC3.Visible = false;
                menuButtonC4.Visible = false;

            }
            mainPanel.Visible = OnOff;
        }

        /// <summary>
        /// Click bouton excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripBtExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = Lang.Text("excel_filter");
            saveFileDialog1.Title = Lang.Text("excel_title");
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                SaveExcel(this, saveFileDialog1.FileName);
            }
        }

        /// <summary>
        /// Save dans fichier excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="path"></param>
        private void SaveExcel(object sender, string path)
        {
            if (ExcelController.Write(seeds, path))
                MessageBox.Show(Lang.Text("excel_success"));
            else
                MessageBox.Show(Lang.Text("excel_failed"));
        }

        private void toolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        /// <summary>
        /// click icone de statistiques
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            TreeViewUserControl treeView;
            Form statsForm;

            treeView = new TreeViewUserControl(seeds);
            treeView.Dock = DockStyle.Fill;

            statsForm = new Form();
            statsForm.Text = Lang.Text("stats");
            statsForm.Size = new Size(300, 500);

            statsForm.Icon = Properties.Resources.list_icon;
            statsForm.Controls.Add(treeView);
            statsForm.Show();
        }

        /// <summary>
        /// click sur le bouton classe 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuButtonC1_Click(object sender, EventArgs e)
        {
            Update(1);
        }

        /// <summary>
        /// click sur bouton general
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuButtonGeneral_Click(object sender, EventArgs e)
        {
            Update(-1);
        }

        /// <summary>
        /// click sur bouton Classe 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuButtonC2_Click(object sender, EventArgs e)
        {
            Update(2);
        }
        /// <summary>
        /// click sur bouton C3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuButtonC3_Click(object sender, EventArgs e)
        {
            Update(3);
        }
        /// <summary>
        /// Verifie si la liste de grains en fonction d'un numéro de classe donnée est valide (c-a-d non vide, listes des critères valides)
        /// </summary>
        /// <param name="numClass">numéro de classe (doit être compris entre 1 et 4 ou -1) </param>
        /// <returns></returns>
        private bool CheckSeeds(int numClass)
        {
            Sorting sorting;
            sorting = Sorting.Instance();
            int nbClass;

            if (numClass < 1 && numClass!=-1)
                return false;

            if (sorting != null && sorting.Criterias != null && sorting.Criterias.Count != 0)
                nbClass = sorting.Criterias.Count + 1;
            else
                nbClass = 2;

            if (numClass >= 0 && numClass <= nbClass)
            {
                if (sorting == null || sorting.Criterias == null)
                    return false;
                if (sorting.Criterias.Count+1 < numClass)
                    return false;
                if (sorting.Criterias[numClass-1].Count == 0)
                    return false;
                if (seeds == null || seeds.Count == 0)
                    return false;
                List<Seed> s = SortSeedsbyClass(seeds, numClass);
                if (s.Count == 0) return false;

                return true;
            }
            else
            {
                if (seeds == null || seeds.Count == 0)
                    return false;
                return true;
            }
        }

        internal void UpdateLanguage()
        {
            if (!isActivated)
            {
                lb.Text = Lang.Text("impossible_analysis");
            }
            else
            {
                //overviewPart
            }

        }
    }
}

