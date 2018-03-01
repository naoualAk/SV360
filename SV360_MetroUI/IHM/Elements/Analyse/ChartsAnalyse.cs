using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SV360.Data;

namespace SV360.IHM.Elements.Analyse
{
    public partial class ChartsAnalyse : UserControl
    {

        ChartUsCo widthChart, thicknessChart, lengthChart;


        /// <summary>
        /// UserControl qui englobe et instancie les graphiques histogrammes 2D dédiées aux donneés morphologiques des grains (largeur, longueur, épaisseur) en fonction d'une liste de seed.
        /// </summary>
        /// <param name="seeds">liste de grains</param>
        public ChartsAnalyse(List<Seed> seeds)
        {

            InitializeComponent();

            Dock = DockStyle.Fill;
            
            if (seeds == null) return;

            thicknessChart = new ChartUsCo(ChartUsCo.AbscisseType.thickness, seeds);
            widthChart = new ChartUsCo(ChartUsCo.AbscisseType.width, seeds);
            lengthChart = new ChartUsCo(ChartUsCo.AbscisseType.length, seeds);

            tableLayoutPanel1.Controls.Add(thicknessChart, 0, 0);
            thicknessChart.Dock = DockStyle.Fill;
            tableLayoutPanel1.Controls.Add(widthChart, 1, 0);
            widthChart.Dock = DockStyle.Fill;
            tableLayoutPanel1.Controls.Add(lengthChart, 2, 0);
            lengthChart.Dock = DockStyle.Fill;
        }
    }
}
