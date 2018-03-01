using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using System.Windows.Forms.DataVisualization.Charting;
using SV360.Resources;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using SV360.Resources.lang;
using SV360.IHM.Elements;
using SV360.Data;

namespace SV360.IHM.PageTab
{

    /// <summary>
    /// Interface de l'onglet trier
    /// L'interface sert à :
    /// <list type="bullet">
    ///<item>lancer le passage du lot</item>
    ///<item>effectuer les anlayses en temps réel</item>
    ///<item>afficher les données des grains qui viennent d'être analysée</item>
    /// </list>
    /// </summary>
    public partial class SortingUC : UserControl
    {
        /// <summary>
        /// Appui sur bouton Play
        /// </summary>
        public event EventHandler PlayClick;
        /// <summary>
        /// Appui sur bouton Stop
        /// </summary>
        public event EventHandler StopClick;
        /// <summary>
        /// Appui  sur le bouton Pause
        /// </summary>
        public event EventHandler pauseClick;
        Series pieChartSeries;
        Label labelMess;
        int nbClass;
        SVButton bt;
        Dictionary<Point, Color> cellcolors;
        bool isStarted = false;
        bool isPaused = false;

        /// <summary>
        ///  Affiche les données sur la longueur, épaisseur, largeur
        /// </summary>
        /// <param name="width">largeur</param>
        /// <param name="thickness">épaisseur</param>
        /// <param name="length">longueur</param>
        public void ShowFeatures(float width, float thickness, float length)
        {
            morphoShow.ShowFeatures(width, thickness, length);
        }

        /// <summary>
        /// Cstr:
        ///     Instancie tout les elements graphiques permettant d'afficher les données sur les grains venant d'être analysés
        /// </summary>
        public SortingUC()
        {
            InitializeComponent();

            Sorting sorting;
            sorting = Sorting.Instance();

            if (sorting != null && sorting.Criterias != null && sorting.Criterias.Count != 0)
                nbClass = sorting.Criterias.Count + 1;
            else
                nbClass = 2;


            // tableLayoutPanelLeft.SetColumnSpan(metroProgressSpinner1, 2);
            // tableLayoutPanelLeft.SetColumnSpan(sortingImageBox, 2);

            //startBt.Checked += StartBtChecked;
            startBt.MouseClick += StartBtChecked;
       

            SizeChanged += new EventHandler(UpdateSize);

            //startBt.MouseHover += mouseHover;
            //startBt.MouseEnter += mouseEnter;
          //  startBt.MouseLeave += mouseLeave;
          //  startBt.BackColorChanged += BackColorChanged;

            sortingImageBox.SizeMode = PictureBoxSizeMode.Zoom;

            //bt start/stop
            //tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Absolute;
         //   tableLayoutPanel1.RowStyles[1].Height = 30;

            labelMess = new Label();
            tableLayoutPanelLeft.Controls.Add(labelMess, 0, 3);
            tableLayoutPanelLeft.SetColumnSpan(labelMess, 3);
            labelMess.Dock = DockStyle.Fill;
            labelMess.TextAlign = ContentAlignment.MiddleCenter;
            labelMess.AutoSize = false;

            pauseLabel.BackColor = Color.Transparent;
            playLabel.BackColor = Color.Transparent;
            stopLabel.BackColor = Color.Transparent;

            pauseBt.Visible = false;
            stopLabel.Visible = false;
            pauseLabel.Visible = false;
            stopBt.Visible = false;
            pauseBt.Visible = false;

            cellcolors = new Dictionary<Point, Color>();
            Color color = SVColor.peterRiver;
            cellcolors.Add(new Point(0, 1), color);
            cellcolors.Add(new Point(1, 1), color);
            cellcolors.Add(new Point(2, 1), color);
            cellcolors.Add(new Point(0, 2), color);
            cellcolors.Add(new Point(1, 2), color);
            cellcolors.Add(new Point(2, 2), color);

            

            tableLayoutPanelLeft.CellPaint += tableLayoutPanel1_CellPaint;

            InitLayout();
        }


        /// <summary>
        /// Dessine le cadre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (cellcolors.Keys.Contains(new Point(e.Column, e.Row)))
                using (SolidBrush brush = new SolidBrush(cellcolors[new Point(e.Column, e.Row)]))
                    e.Graphics.FillRectangle(brush, e.CellBounds);   
        }


        /// <summary>
        /// Affiche  un message dans un label visible par l'utilisateur situé en dessous de la barre de mise en route (play, stop, pause)
        /// </summary>
        /// <param name="mess"></param>
        public void SetLabel(string mess)
        {
            SetText(labelMess, mess);
            metroProgressSpinner1.Visible = false;
            labelMess.Visible = true;

        }

        /// <summary>
        /// Active le spinner, le cercle tournant qui indique que le processus est lancé
        /// </summary>
        /// <param name="OnOff"></param>
        public void SetSpinner(bool OnOff)
        {
            metroProgressSpinner1.Visible = OnOff;
            labelMess.Visible = !OnOff;
        }

        private void UpdateSize(object sender, EventArgs e)
        {

            int size = chartPie.Size.Width / 35;
            if (size > 15)
                size = 15;
            else if (size < 6)
                size = 6;

            //counterLabel.Font = new Font(counterLabel.Font.FontFamily, size, counterLabel.Font.Style);
            if (pieChartSeries != null)
                pieChartSeries.Font = new Font(pieChartSeries.Font.FontFamily, chartPie.Size.Width / 55, chartPie.Font.Style);
        }

        /// <summary>
        /// Initilise l'interface
        /// </summary>
        private void InitLayout()
        {
            //   tableLayoutPanel1.RowStyles[0].Height = 30;
            // tableLayoutPanel1.RowStyles[tableLayoutPanel1.RowStyles.Count - 1].Height = 70;
            chartPie.BackColor = Color.Transparent;
           
            metroProgressSpinner1.Visible = false;
            sortingImageBox.Visible = false;
            //counterPanel.Visible = false;
            chartPie.Visible = false;
            labelMess.Visible = false;
            morphoShow.Visible = false;
            pauseBt.Visible = false;
        }


        /// <summary>
        /// Mets les valeurs des graphes à zéro.
        /// Counter = #Seed
        /// ChartPie=0
        /// 
        /// </summary>
        public void ResetValues()
        {

            //SetCounter("# Seed");

            if (pieChartSeries != null)
                for (int i = 0; i < nbClass; i++)
                {

                    pieChartSeries.Points[i].AxisLabel = "";
                    pieChartSeries.Points[i].SetValueY(0);
                }
            morphoShow.Reset();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ResetShowAllLayout()
        {
         //   tableLayoutPanel1.RowStyles[0].Height = 25;
          //  tableLayoutPanel1.RowStyles[2].Height = 25;
          //  tableLayoutPanel1.RowStyles[3].Height = 40;

            //counterPanel.Visible = true;
            morphoShow.Visible = true;
            morphoShow.Reset();
            sortingImageBox.Visible = true;

            UpdateSize(this, EventArgs.Empty);
        }




        private void StartBtChecked(object sender, EventArgs e)
        {
            if (!isStarted)
            {
                StartState();
            }
          
        }


        /// <summary>
        /// Update de l'interface quand le processus est démarré
        /// </summary>
        private void StartState()
        {
            //startBt.Text = Lang.Text("sorting_start_bt_stop");
            pauseBt.Enabled = true;
            startBt.BackgroundImage = Properties.Resources.play_button_disabled;
            pauseBt.BackgroundImage = Properties.Resources.pause_circular_button;
            startBt.BackColor = Color.Transparent;
            pauseBt.Visible = true;
            stopLabel.Visible = true;
            pauseLabel.Visible = true;
            stopBt.Visible = true;
            pauseBt.Visible = true;
            startBt.Enabled = false;
            ResetShowAllLayout();
            isStarted = true;
            PlayClick(this, EventArgs.Empty);
        }

        /// <summary>
        /// Update de l'interface quand le processus est en pause
        /// </summary>
        private void PauseState()
        {
            //startBt.Text = Lang.Text("sorting_start_bt_stop");
            pauseBt.BackgroundImage = Properties.Resources.pause_circular_button_disabled;
            startBt.BackgroundImage = Properties.Resources.play_button;
            pauseBt.BackColor = Color.Transparent;
            pauseBt.Visible = true;
            pauseBt.Enabled = false;
            stopLabel.Visible = true;
            pauseLabel.Visible = true;
            stopBt.Visible = true;
            pauseBt.Visible = true;
            startBt.Enabled = true;
           
            pauseClick(this, EventArgs.Empty);
            isStarted = false;
        }


        /// <summary>
        /// Update de l'interface quand le processus est arrété
        /// </summary>
        private void StopState()
        {

            pauseBt.Enabled = true;
            startBt.Enabled = true;
            startBt.BackgroundImage = Properties.Resources.play_button;
            startBt.BackColor = Color.Transparent;
            pauseBt.Visible = false;
            stopLabel.Visible = false;
            pauseLabel.Visible = false;
            stopBt.Visible = false;
            pauseBt.Visible = false;
            isStarted = false;       
        }


        /// <summary>
        /// Affiche une image dans imageBox de l'interface
        /// </summary>
        /// <param name="image"></param>
        public void DisplayAcquisition(Image<Rgb, byte> image)
        {
            //Image<Bgr, byte> im = new Image<Bgr, byte>(image.Width, image.Height);
            //image.CopyTo(im);

            sortingImageBox.Image = image;
        }

        /// <summary>
        /// Efface l'image de l'interface
        /// </summary>
        public void ClearAcquisition()
        {
            //Image<Bgr, byte> im = new Image<Bgr, byte>(image.Width, image.Height);
            //image.CopyTo(im);
            if (sortingImageBox != null && sortingImageBox.Image != null)
                sortingImageBox.Image = null;
        }

        /// <summary>
        /// Affiche un nombre dans la partie correspondant au nombre de grains analysé
        /// </summary>
        /// <param name="count">nmobre</param>
        public void SetCounter(int count)
        {
            // SetText(counterLabel, count.ToString());
            morphoShow.SetCounter(count);
        }


        /// <summary>
        /// Affiche un message dans la partie correspondant au nombre de grains analysé
        /// </summary>
        /// <param name="str">message</param>
        public void SetCounter(string str)
        {
            morphoShow.SetCounter(str);
            //  SetText(counterLabel, str);
        }

        //Asynchrone Pie Chart update
        delegate void SetPieChartCallback(List<int> count);
        /// <summary>
        /// Affiche le temps de passage du dernier grain dans l'interface
        /// </summary>
        /// <param name="elapsed"></param>
        /// <param name="fps"></param>
        internal void ShowTime(TimeSpan elapsed, float fps)
        {
            morphoShow.ShowTime(elapsed, fps);
        }

        /// <summary>
        /// Affiche les données de répartition des classes dans un chartpie
        /// </summary>
        /// <param name="countByClass">liste du nombre de grains par classe</param>
        public void SetChartPie(List<int> countByClass)
        {
            if (chartPie.InvokeRequired)
            {
                SetPieChartCallback v = new SetPieChartCallback(SetChartPie);
                if (countByClass != null)
                    this.Invoke(v, new object[] { countByClass });
                else return;
            }
            else
            {

                if (countByClass == null || countByClass.Count == 0)
                    return;

                //create pie chart
                if (pieChartSeries == null)
                {
                    chartPie.Series.Clear();
                    pieChartSeries = new Series
                    {
                        Name = "series1",
                        IsVisibleInLegend = true,
                        ChartType = SeriesChartType.Doughnut,
                        Font = new Font(chartPie.Font.FontFamily, chartPie.Size.Width / 55, chartPie.Font.Style),
                        LabelForeColor = System.Drawing.Color.White
                    };
                    chartPie.Series.Add(pieChartSeries);

                    pieChartSeries.Color = Color.Transparent;
                    chartPie.ChartAreas[0].BackColor = Color.Transparent;
                    chartPie.Legends[0].BackColor = Color.Transparent;

                    for (int i = 0; i < countByClass.Count; i++)
                    {
                        pieChartSeries.Points.Add(countByClass[i]);
                        var p = pieChartSeries.Points[i];
                        if (countByClass[i] > 0)
                            p.AxisLabel = countByClass[i].ToString() + "%";
                    }

                    if (countByClass.Count > 0) pieChartSeries.Points[0].Color = SVColor.lightGray;
                    if (countByClass.Count > 1) pieChartSeries.Points[1].Color = SVColor.nephritis;
                    if (countByClass.Count > 2) pieChartSeries.Points[2].Color = SVColor.alizarin;
                    if (countByClass.Count > 3) pieChartSeries.Points[3].Color = SVColor.belizeHole;
                    if (countByClass.Count > 4) pieChartSeries.Points[4].Color = SVColor.carrot;

                    if (countByClass.Count > 0) pieChartSeries.Points[0].LegendText = "Undefined";
                    if (countByClass.Count > 1) pieChartSeries.Points[1].LegendText = Lang.Text("Class") + " 1";
                    if (countByClass.Count > 2) pieChartSeries.Points[2].LegendText = Lang.Text("Class") + " 2";
                    if (countByClass.Count > 3) pieChartSeries.Points[3].LegendText = Lang.Text("Class") + " 3";
                    if (countByClass.Count > 4) pieChartSeries.Points[4].LegendText = Lang.Text("Class") + " 4";

                    chartPie.Visible = true;
                    chartPie.Invalidate();
                    return;
                }
                else
                {
                    chartPie.Visible = true;
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


        delegate void SetTextCallback(Label lb, string text);
        private void SetText(Label lb, string text)
        {
            if (lb.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { lb, text });
            }
            else
            {
                lb.Text = text;
            }
        }

        internal void UpdateLanguage()
        {
          /*  if (!startBt.isChecked)
                startBt.Text = Lang.Text("sorting_start_bt_start");
            else
                startBt.Text = Lang.Text("sorting_start_bt_stop");*/
        }

        private void stopBt_Click(object sender, EventArgs e)
        {
            PauseState();
            //StopBtChanged(this, true);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void sortingImageBox_Click(object sender, EventArgs e)
        {

        }

        private void metroProgressSpinner1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanelLeft_Paint(object sender, PaintEventArgs e)
        {

        }

        private void startBt_Click(object sender, EventArgs e)
        {

        }

        private void stopBt_Click_1(object sender, EventArgs e)
        {
            StopState();
            StopClick(this, null);
        }
    }
}
