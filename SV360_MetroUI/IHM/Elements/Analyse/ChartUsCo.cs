using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;
using SV360.Resources;
using SV360.Utils;
using SV360.Data;
using System.IO;

namespace SV360.IHM.Elements.Analyse
{

    /// <summary>
    /// UserControl qui permet d'afficher un graphique histogramme 2D en fonction d'une liste de grains et la caractéristique voulue (largeur, longueur ou épaisseur)
    /// En abscisse: les dimensions en mm par pas de 0.25mm.
    /// En ordonnée: le nombre de grains.
    /// </summary>
    public partial class ChartUsCo : UserControl
    {

        /// <summary>
        /// Type de caractéristiques
        /// </summary>
        public enum AbscisseType {
            /// <summary>
            /// Longueur 
            /// </summary>
            length,
            /// <summary>
            /// largeur 
            /// </summary>
            width,
            /// <summary>
            /// épaisseur
            /// </summary>
            thickness };

        AbscisseType absType;

        private ChartUsCo(AbscisseType absType)
        {
            InitializeComponent();
            this.absType = absType;
            Resize += updateSize;
        }

        /// <summary>
        /// Adapte la taille des écritures en fonction de la taille du userControl.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateSize(object sender, EventArgs e)
        {

            int maxSize = 18;
            if (chart1 != null)
            {
                if(Size.Width / 15 > 1 && Size.Width / 15 < maxSize)
                    chart1.Titles[0].Font = new Font(chart1.Titles[0].Font.FontFamily, Size.Width / 15, chart1.Titles[0].Font.Style);
                if (Size.Width / 15 > 1 && Size.Width / 15 >= maxSize)
                    chart1.Titles[0].Font = new Font(chart1.Titles[0].Font.FontFamily, maxSize, chart1.Titles[0].Font.Style);

                if(chart1.ChartAreas.Count > 0 && chart1.Height>0)
                    chart1.ChartAreas[0].AxisY.TitleFont = new Font(chart1.ChartAreas[0].AxisY.TitleFont.FontFamily.Name, chart1.Height/22, chart1.ChartAreas[0].AxisY.TitleFont.Style);
            }    
        }

        /// <summary>
        /// Cstor  par défaut:
        /// <list type="bulet">
        ///     <item>  Instancie les graphismes du userControl</item>
        ///     <item>   Charge la liste des grains en fonction de la caractéristique voulue. </item>
        /// </list>
        /// </summary>
        /// <param name="absType">caractéristique</param>
        /// <param name="seeds">liste de grains à afficher</param>
        public ChartUsCo(AbscisseType absType, List<Seed> seeds)
        {
            InitializeComponent();
            this.absType = absType;
            LoadSeeds(seeds);
            Resize += updateSize;
        }

        private ChartUsCo()
        {
            InitializeComponent();
        }

        VerticalLineAnnotation VA;
        Series series1;
        int ptX1;

        /// <summary>
        ///  Affiche dans le UserControl, l'histogramme d'une liste de grains. 
        ///     Les grains inférieurs à 1 ou supérieurs à 15mm ne sont pas pris en compte.
        /// </summary>
        /// <param name="seeds">liste de grains</param>
        public void LoadSeeds(List<Seed> seeds)
        {
            if (seeds == null || seeds.Count == 0)
                return;

            chart1.Series.Clear();
            series1 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Series1",
                IsVisibleInLegend = false,
                IsXValueIndexed = true
            };
  
            this.chart1.Series.Add(series1);

            if (seeds.Count == 0) return;

            List<double> list = new List<double>(seeds.Count);

            // Selection des grains en fonction de leur taille
            seeds = (from s in seeds
                     where s.Length > 1 && s.Length < 15
                            && s.Width > 1 && s.Width < 15
                            && s.Thickness > 1 && s.Thickness < 15
                     select s).ToList();

            // ajout dans la liste de données des caract. des grains en fonction de la caractéristique voulue
            switch (absType)
            {
                case AbscisseType.length:
                    for (int i = 0; i < seeds.Count; i++)
                        if(seeds[i].Length != 0) list.Add(seeds[i].Length);
                    break;
                        case AbscisseType.width:
                    for (int i = 0; i < seeds.Count; i++)
                        if (seeds[i].Width != 0) list.Add(seeds[i].Width);
                    break;
                case AbscisseType.thickness:
                    for (int i = 0; i < seeds.Count; i++)
                        if (seeds[i].Thickness != 0) list.Add(seeds[i].Thickness);
                    break;
            }

            // tri dans l'ordre croissant
            list.Sort((x, y) => x.CompareTo(y));


            // division par pas de 0.25 du plus petit
            float length = (float)Math.Round(list[0] * 4, MidpointRounding.ToEven) / 4;
            int numb = 0;


            // ajout des grains en fonction du pas
            for (int i = 0; i < list.Count; i++)
            {
                if (Math.Abs((float)list[i] - length) < 0.25)
                {
                    numb++;
                    continue;
                }
                else
                {
                    series1.Points.AddXY((double)length , (double)numb * 100 / (list.Count ));
                    length += 0.25F;
                    numb = 0;
                    i--;
                }
               
            }

            // ajout des données dans la série
            series1.Points.AddXY((double)length , (double)numb * 100 / (list.Count));


            // MAJ (mise à jour) des titres et légendes
            chart1.ChartAreas[0].AxisY.Title = @"% grains"; // ??!

            switch (absType)
            {
                case AbscisseType.length:
                    chart1.Titles[0].Text = "Répartition de la longueur des grains"; //"Number of seeds based on length";
                    series1.Color = SVColor.peterRiver;
                    chart1.ChartAreas[0].AxisX.Title = "Longueur";
                    break;

                case AbscisseType.thickness:
                    chart1.Titles[0].Text = "Répartition de l'épaisseur des grains";
                    series1.Color = SVColor.sunFlower;
                    chart1.ChartAreas[0].AxisX.Title = "Epaisseur";
                    break;

                case AbscisseType.width:
                    chart1.Titles[0].Text = "Répartition de la largeur des grains";
                    series1.Color = SVColor.emerald;
                    chart1.ChartAreas[0].AxisX.Title = "Largeur";
                    break;
            }

            chart1.Invalidate();

            chart1.Annotations.Clear();

            VA = new VerticalLineAnnotation();
            VA.LineWidth = 3;
            VA.IsInfinitive = false;
            VA.Height = 65;
            chart1.ChartAreas[0].RecalculateAxesScale();

            VA.Y = (int)chart1.ChartAreas[0].AxisY.Maximum + 0.5;

            VA.AnchorDataPoint = series1.Points[0];
            VA.X = 0;
            VA.AllowMoving = true;
            VA.LineColor = SVColor.pumpkin;
            chart1.Annotations.Add(VA);
        }

        /// <summary>
        /// Création titre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
          
            switch (absType)
            {
                case AbscisseType.length:
                    chart1.Titles[0].Text = "Répartition de la longueur des grains"; 
                    series1.Color = SVColor.peterRiver;
                    chart1.ChartAreas[0].AxisX.Title = "Longueur";
                    break;

                case AbscisseType.thickness:
                    chart1.Titles[0].Text = "Répartition de l'épaisseur des grains";
                    series1.Color = SVColor.sunFlower;
                    chart1.ChartAreas[0].AxisX.Title = "Epaisseur";
                    break;

                case AbscisseType.width:
                    chart1.Titles[0].Text = "Répartition de la largeur des grains";
                    series1.Color = SVColor.emerald;
                    chart1.ChartAreas[0].AxisX.Title = "Largeur";
                    break;
            }

          
        }

        /// <summary>
        ///  Savegarde du graphique histogramme en png.
        /// </summary>
        public void SaveImage(string path)
        {
            VA.Visible = false;

            FileInfo fi = new FileInfo(path);
            if (fi.Extension != ".png" || fi.Extension != "png")
                return;
            try
            {
                chart1.SaveImage(path, ChartImageFormat.Png);
            }
            catch { MessageBox.Show("Sauvegarde impossible"); }
            VA.Visible = true;
        }

        /// <summary>
        ///     Gere le deplacement de la barre verticale.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chart1_AnnotationPositionChanging(object sender, AnnotationPositionChangingEventArgs e)
        {
            ptX1 = (int)e.NewLocationX;

            //chart1.Update();
        }

        /// <summary>
        /// Gere le deplacement de la barre verticale
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chart1_AnnotationPositionChanged(object sender, EventArgs e)
        {
            //VA.AnchorDataPoint= series1.Points[4];
            if (ptX1 <= 0) VA.X = 0;
            else
            {
                if (ptX1 > series1.Points.Count) ptX1 = series1.Points.Count;
                VA.X = ptX1 + 0.4;
            }

            
        }

        /// <summary>
        ///  si click sur le graphique, la barre verticale se déplace automatiquement.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chart1_Click(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            double xVal = 0;
            var results = chart1.HitTest(pos.X, pos.Y, false,
                                         ChartElementType.PlottingArea);
            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.PlottingArea)
                {
                    xVal = result.ChartArea.AxisX.PixelPositionToValue(pos.X);
                    var yVal = result.ChartArea.AxisY.PixelPositionToValue(pos.Y);
                }
            }

            ptX1 = (int)xVal;
            //Debug.WriteLine(ptX);
            if (ptX1 <= 0) VA.X = 0;
            else
            {
                if (ptX1 > series1.Points.Count) ptX1 = series1.Points.Count;
                VA.X = ptX1 + 0.4;
            }

            //SaveImage();
        }
    }
}
