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
using SV360.Resources;
using SV360.Data;

namespace SV360.IHM.Elements
{
    
    /// <summary>
    /// Chartpie est un usercontrol qui permet d'afficher via un graphique en camembert la répartition des classes 
    /// </summary>
    public partial class ChartPie : UserControl
    {

        Series pieChartSeries;
        Color[] colors;

        /// <summary>
        /// Cstor : Instancie les graphismes
        /// </summary>
        public ChartPie()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            chart.Dock = DockStyle.Fill;
            Resize += new EventHandler(UpdateSize);
            chart.BackColor = SVColor.snow;
            chart.BackSecondaryColor = SVColor.snow;


            colors = new Color[]{
                SVColor.lightGray,
                SVColor.nephritis,
                SVColor.alizarin,
                SVColor.belizeHole,
                SVColor.carrot
            };
        }

        private void UpdateSize(object sender, EventArgs e)
        {
            if (pieChartSeries != null)
            {
                int min = Size.Height < Size.Width ? Size.Height : Size.Width;
                int size = min / 25 < 4 ? 4 : min / 25;
                pieChartSeries.Font = new Font(Font.FontFamily, size, Font.Style);
            }
        }

        //Asynchrone Pie Chart update
        delegate void SetPieChartCallback(List<Seed> seeds);


        /// <summary>
        /// Afiche la répartition des classes d'une liste de grains
        /// </summary>
        /// <param name="seeds">liste de grains</param>
        public void Set(List<Seed> seeds)
        {
            if (seeds.Count < 1) return;

            List<int> countByClass = new List<int>(4);
            for (int i = 0; i < Seed.nbClass; i++)
            {
                countByClass.Add(0);
            }

            for (int i = 0; i < seeds.Count; i++)
            {
                countByClass[(int)seeds[i].NumClass]++;
            }

            for (int i = 0; i < Seed.nbClass; i++)
            {
                countByClass[i] = countByClass[i] * 100 / seeds.Count;
            }

            if (chart.InvokeRequired)
            {
                SetPieChartCallback v = new SetPieChartCallback(Set);
                this.Invoke(v, new object[] { countByClass });
            }
            else
            {
                //create pie chart
                if (pieChartSeries == null)
                {
                    chart.Series.Clear();
                    pieChartSeries = new Series
                    {
                        Name = "series1",
                        IsVisibleInLegend = true,
                        ChartType = SeriesChartType.Doughnut,
                        Font = new Font("Calibri", Size.Height / 35, Font.Style),
                        LabelForeColor = System.Drawing.Color.White,
                    };

                    //pieChartSeries.SmartLabelStyle.Enabled = true;
                    pieChartSeries.BackSecondaryColor = SVColor.snow;
                    pieChartSeries.Color = SVColor.snow;

                    chart.BackColor = Color.Transparent;


                    chart.Series.Add(pieChartSeries);

                    for (int i = 0; i < countByClass.Count; i++)
                    {
                        pieChartSeries.Points.Add(countByClass[i]);
                        if (countByClass[i] > 0)
                        {
                            var p = pieChartSeries.Points[i];
                            p.AxisLabel = countByClass[i].ToString() + "%";
                        }
                    }

                    for (int i = 0; i < chart.Legends.Count; i++)
                    {
                        chart.Legends[i].BackColor = Color.Transparent;
                        chart.ChartAreas[i].BackColor = Color.Transparent;
                    }


                    for (int i = 0; i < pieChartSeries.Points.Count; i++)
                    {
                        pieChartSeries.Points[i].Color = colors[i];
                    }
                  

                    for (int i = 0; i < pieChartSeries.Points.Count; i++)
                    {
                        if (i == 0)
                        {
                            pieChartSeries.Points[i].LegendText = "Indéfini";
                            pieChartSeries.Points[i].IsVisibleInLegend = (countByClass[i] > 0);
                        }
                        else
                        {
                            pieChartSeries.Points[i].LegendText = "Classe " + (i);
                            pieChartSeries.Points[i].IsVisibleInLegend = (countByClass[i] > 0);
                        }
                    }

                    chart.Visible = true;
                    chart.Invalidate();
                    return;
                }
                else
                {
                    chart.Visible = true;
                    for (int i = 0; i < countByClass.Count; i++)
                    {
                        if (countByClass[i] > 0)
                        {
                            pieChartSeries.Points[i].AxisLabel = countByClass[i].ToString() + "%";
                            pieChartSeries.Points[i].SetValueY(countByClass[i]);
                        }
                    }
                }
            }
        }




        /// <summary>
        ///  renvoi la liste de nombre d'une classe
        /// if numclass = -1, fill all the class
        /// if numclass != -1, fill in one class and fill others in class
        /// </summary>
        /// <param name="seeds"></param>
        /// <param name="numClass"></param>
        /// <returns></returns>
        private List<int> SetCountByClass(List<Seed> seeds, int numClass = -1)
        {

            List<int> countByClass = new List<int>(Seed.nbClass);
            for (int i = 0; i < Seed.nbClass; i++)
            {
                countByClass.Add(0);
            }
            for (int i = 0; i < seeds.Count; i++)
            {
                if (numClass != -1)
                {
                    if (seeds[i].NumClass == (Seed.ENumClass)numClass)
                        countByClass[numClass]++;
                    else
                    {
                        countByClass[0]++;
                    }
                }
                else
                    countByClass[(int)seeds[i].NumClass]++;
            }

            for (int i = 0; i < Seed.nbClass; i++)
            {
                countByClass[i] = countByClass[i] * 100 / seeds.Count;
            }
            return countByClass;
        }


        /// <summary>
        /// Affiche la répartition d'une classe par rapport aux autres d'une liste de grains 
        /// </summary>
        /// <param name="seeds">liste de grains </param>
        /// <param name="numClass">numéro de la classe</param>
        public void Set(List<Seed> seeds, int numClass)
        {

            if (seeds.Count < 1) return;

            List<int> countByClass = SetCountByClass(seeds, numClass);

            if (chart.InvokeRequired)
            {
                SetPieChartCallback v = new SetPieChartCallback(Set);
                this.Invoke(v, new object[] { countByClass });
            }
            else
            {
                //create pie chart
                if (pieChartSeries == null)
                {
                    chart.Series.Clear();
                    pieChartSeries = new Series
                    {
                        Name = "series1",
                        IsVisibleInLegend = true,
                        ChartType = SeriesChartType.Doughnut,
                        Font = new Font("Calibri", Size.Height / 35, Font.Style),
                        LabelForeColor = System.Drawing.Color.White,

                    };


                    //pieChartSeries.SmartLabelStyle.Enabled = true;
                    pieChartSeries.BackSecondaryColor = SVColor.snow;
                    pieChartSeries.Color = SVColor.snow;

                    chart.BackColor = Color.Transparent;


                    chart.Series.Add(pieChartSeries);

                    for (int i = 0; i < countByClass.Count; i++)
                    {
                        pieChartSeries.Points.Add(countByClass[i]);
                        if (countByClass[i] > 0)
                        {
                            var p = pieChartSeries.Points[i];
                            p.AxisLabel = countByClass[i].ToString() + "%";
                        }
                    }

                    for (int i = 0; i < chart.Legends.Count; i++)
                    {
                        chart.Legends[i].BackColor = Color.Transparent;
                        chart.ChartAreas[i].BackColor = Color.Transparent;
                    }

                    if (numClass >= 0)
                    {
                        if (pieChartSeries.Points.Count > 0)
                            pieChartSeries.Points[numClass].Color = colors[numClass];
                        if (pieChartSeries.Points.Count > 1)
                            pieChartSeries.Points[0].Color = SVColor.asbestos;

                        for (int i = 0; i < pieChartSeries.Points.Count; i++)
                            pieChartSeries.Points[i].IsVisibleInLegend = (countByClass[i] > 0);

                        pieChartSeries.Points[numClass].LegendText = "Classe " + (numClass);
                        pieChartSeries.Points[0].LegendText = "Autres";
                    }
                    else
                    {
                        for (int i = 0; i < pieChartSeries.Points.Count; i++)
                            pieChartSeries.Points[i].Color = colors[i];
                        

                        for (int i = 0; i < pieChartSeries.Points.Count; i++)
                        {
                            if (i == 0)
                            {
                                pieChartSeries.Points[i].LegendText = "Indéfini";
                                pieChartSeries.Points[i].IsVisibleInLegend = (countByClass[i] > 0);
                            }
                            else
                            {
                                pieChartSeries.Points[i].LegendText = "Classe " + (i);
                                pieChartSeries.Points[i].IsVisibleInLegend = (countByClass[i] > 0);
                            }
                        }
                    }


                    chart.Visible = true;
                    chart.Invalidate();
                    return;
                }
                else
                {
                    chart.Visible = true;
                    for (int i = 0; i < countByClass.Count; i++)
                    {
                        if (countByClass[i] > 0)
                        {
                            pieChartSeries.Points[i].AxisLabel = countByClass[i].ToString() + "%";
                            pieChartSeries.Points[i].SetValueY(countByClass[i]);
                        }
                    }
                }
            }

        }
    }
}
