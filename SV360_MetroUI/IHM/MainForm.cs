using Emgu.CV.Structure;
using MetroFramework.Forms;
using SV360.Automatism;
using SV360.Data;
using SV360.IHM.PageTab;
using SV360.Resources.lang;
using SV360.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SV360.IHM
{

    /// <summary>
    /// Fenetre principale du logiciel contenant les onglets : 
    /// <list type="bulet"> <item>
    ///                     - seuillage 
    ///  </item><item>      - trier 
    ///   </item><item>     - analyser
    ///   </item>
    ///   </list>
    /// La classe gere les modifications a apporter à la fenetre lorsqu'il y a des events (exemple: Stopsorting, Resize, etc.)
    /// 
    /// </summary>
    public partial class MainForm : MetroForm
    {

        SV360Controller SV360;
        SortingUC sortingUC;
        AnalysisUC analysisUC;
        ThresholdingUC thresholdingUC;

        Stopwatch executionTime;


        bool FirstProcessCompleted = false;

        ScannedVariable varSCA = new ScannedVariable() { addr = 31, valueType = DataType.Integer };
        ScannedVariable varSCO = new ScannedVariable() { addr = 30, valueType = DataType.Integer };

        /// <summary>
        /// Cstor de la fenetre principale :
        ///     - Charge les ressources de langue
        ///     - Charge les onglets et les events
        ///     - Construit l'objet de SV360Controller 
        ///     - Gere le chronomètre 
        /// </summary>
        public MainForm()
        {


            InitializeComponent();

            Lang.ResMan = new ResourceManager("SV360.Resources.lang.Res", typeof(MainForm).Assembly);

            FormClosed += MainFormClosed;
            this.Resize += FormResize;
            metroTabControl1.SelectedIndex = 1;

            #region seuils
            thresholdingUC = new ThresholdingUC();
            tabPageSeuils.Controls.Add(thresholdingUC);
            thresholdingUC.Dock = DockStyle.Fill;
            #endregion

            #region analyser
            analysisUC = new AnalysisUC();
            tabPageAnalyser.Controls.Add(analysisUC);
            analysisUC.Dock = DockStyle.Fill;
            #endregion

            #region trier
            sortingUC = new SortingUC();
            tabPageTrier.Controls.Add(sortingUC);
            sortingUC.Dock = DockStyle.Fill;
            sortingUC.PlayClick += StartSorting;
            sortingUC.StopClick += StopSorting;
            sortingUC.pauseClick += PauseSorting;



            #endregion

            UpdateLanguage();

            SV360 = new SV360Controller();

            SV360.UpdateUI += new EventHandler<float>(UpdateUI);

            //userControlAnalyser.SaveExcel += new EventHandler<string>(SaveExcel);
        }

        private bool isPaused = false;


        /// <summary>
        /// Action of pauseclick event
        ///     Lance le processus d'analyse visible dans l'onglet analyse
        ///     Stop le temps d'execution
        ///     Affiche en pause..
        ///     Update l'onglet trier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PauseSorting(object sender, EventArgs e)
        {
            try
            {
                if (FirstProcessCompleted)
                    analysisUC.Set(SV360.seeds, true);
                //userControlAnalyser.ActiveAnalyse(true, SV360.seeds);

                isPaused = true;
                SV360.sc.StopActuators();
                executionTime.Stop();
                sortingUC.SetLabel("En pause..");
                sortingUC.Update();
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (SV360 != null)
                {
                    SV360.sc.StopActuators();
                }
                Close();
            }
        }

        private void SaveExcel(object sender, string path)
        {
            if (ExcelController.Write(SV360.seeds, path))
                MessageBox.Show(Lang.Text("save_excel_done"));
        }



        /// <summary>
        /// Action of startclick event :
        /// 
        ///     si le logiciel n'est pas en pause.
        ///         Clear les acquisitions de l'onglet trier
        ///         Creer une nouvelle liste de seed
        ///         Lance la prise d'images de fonds
        ///    
        ///     Lance le chronomètre
        ///     Reset les compteurs de l'automate
        ///     Lance le run des actionneurs 
        ///     Update de l'interface
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartSorting(object sender, EventArgs e)
        {
            try
            {

                if (isPaused)
                {
                    isPaused = false;
                }
                else
                {


                    //userControlAnalyser.ActiveAnalyse(false);
                    analysisUC.Enable(false);
                    SV360.seeds = new List<Seed>();
                    sortingUC.ClearAcquisition();
                    SV360.sc.ResetCounters();
                    sortingUC.SetLabel(Lang.Text("background_acquisition"));

                    sortingUC.Update();


                    SV360.TakeAcquisitionBackground();

                }

                SV360.ActivateConstantEjection();
                executionTime = Stopwatch.StartNew();

                //First Acquisitions for Background


                if (SV360.sc.hasTakeIDSBackGround == false || SV360.hasTakeIDSBackground == false)
                {
                    sortingUC.SetLabel(Lang.Text("background_acquisition_failed"));
                    //userControlAnalyser.ActiveAnalyse(true, SV360.seeds);
                    FirstProcessCompleted = false;
                    SV360.Enabled = true;
                    //Stop acquisition
                    SV360.sc.StopActuators();
                    sortingUC.Update();
                    sortingUC.ResetValues();

                    return;
                }
                sortingUC.SetSpinner(true);
                SV360.sc.Run();
                SV360.Enabled = true;

            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (SV360 != null)
                {
                    SV360.sc.StopActuators();
                }
                Close();
            }
        }


        /// <summary>
        /// Action of stop sorting click:
        ///     Lance le processus d'analyse visible dans l'onglet analyse
        ///     Stop les actionneurs
        ///     Stop le chrono
        ///     Update l'interface
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopSorting(object sender, EventArgs e)
        {
            try
            {
                if (FirstProcessCompleted && SV360.seeds != null && SV360.seeds.Count > 0)
                    analysisUC.Set(SV360.seeds, true);
                //userControlAnalyser.ActiveAnalyse(true, SV360.seeds);

                isPaused = false;
                FirstProcessCompleted = false;
                //Stop acquisition
                SV360.sc.StopActuators();
                executionTime.Stop();
                executionTime.Reset();
                sortingUC.SetLabel("");
                sortingUC.Update();
                SV360.Enabled = false;
                //sortingUC.ResetValues();
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (SV360 != null)
                {
                    SV360.sc.StopActuators();
                }
                Close();
            }
        }

        private void UpdateUI(object sender, float fps)
        {
            try
            {
                FirstProcessCompleted = true;

                int idx = SV360.seeds.Count;

                if (SV360.seeds[idx - 1] == null || SV360.seeds[idx - 1].ImageCollection == null || SV360.seeds[idx - 1].ImageCollection.Count < 1)
                    return;

                //morpho Show
                sortingUC.ShowFeatures(SV360.seeds[idx - 1].Width, SV360.seeds[idx - 1].Thickness, SV360.seeds[idx - 1].Length);
                sortingUC.ShowTime(executionTime.Elapsed, fps);

                //display acquisition
                sortingUC.DisplayAcquisition(SV360.seeds[idx - 1].ImageCollection[0].Image);

                //string mess = (SV360.seeds.Count).ToString();// + "\n" + (SV360.FinishedCounter).ToString();
                //setCounter
                //sortingUC.SetCounter(idx);

                AutomateScanner.ReadVariable(varSCO, SV360.sc.Master());
                AutomateScanner.ReadVariable(varSCA, SV360.sc.Master());
                sortingUC.SetCounter("    " + idx.ToString() + "\r\n\r\nSCO:" + varSCO.currentValue + "\r\nSCA:" + varSCA.currentValue);



                Sorting sorting;
                sorting = Sorting.Instance();
                int nbClass;

                if (sorting != null && sorting.Criterias != null && sorting.Criterias.Count != 0)
                    nbClass = sorting.Criterias.Count + 1;
                else
                    nbClass = 2;




                List<int> countByClass = new List<int>(nbClass);
                for (int i = 0; i < nbClass; i++)
                {
                    countByClass.Add(0);
                }

                for (int i = 0; i < idx; i++)
                {
                    countByClass[(int)SV360.seeds[i].NumClass]++;
                }

                for (int i = 0; i < nbClass; i++)
                {
                    countByClass[i] = countByClass[i] * 100 / (idx);
                }

                //setPieChart
                sortingUC.SetChartPie(countByClass);
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (SV360 != null)
                {
                    SV360.sc.StopActuators();
                }
                Close();
            }
        }

        private void FormResize(Object sender, EventArgs e)
        {
            metroTabControl1.SizeMode = TabSizeMode.Fixed;
            if (metroTabControl1.TabCount > 1 && metroTabControl1.Width > 0)
                metroTabControl1.ItemSize = new Size(metroTabControl1.Width / metroTabControl1.TabCount - 1, 0);
        }

        private void MainFormClosed(Object sender, FormClosedEventArgs e)
        {
            //Stop system
            if (SV360 != null && SV360.sc != null)
            {
                SV360.sc.Close();
            }
        }


        bool isEnglish = false;
        private void button1_Click(object sender, EventArgs e)
        {
            if (!isEnglish)
            {
                lang_button.BackgroundImage = Properties.Resources.french_flag;
                isEnglish = true;
                UpdateLanguage();
            }
            else
            {
                lang_button.BackgroundImage = Properties.Resources.english_flag;
                isEnglish = false;
                UpdateLanguage();
            }
        }

        private void UpdateLanguage()
        {
            if (isEnglish)
                Lang.CI = CultureInfo.CreateSpecificCulture("en");
            else
                Lang.CI = CultureInfo.CreateSpecificCulture("fr");

            metroTabControl1.TabPages[0].Text = Lang.Text("Thresholding_PageTab_Text");
            metroTabControl1.TabPages[1].Text = Lang.Text("Sorting_PageTab_Text");
            metroTabControl1.TabPages[2].Text = Lang.Text("Analysis_PageTab_Text");
            Text = Lang.Text("Title_program");

            if (SV360 != null && !SV360.sc.IsRunning() && SV360.Enabled)
            {
                sortingUC.SetLabel(Lang.Text("background_acquisition"));
                if (SV360.sc.hasTakeIDSBackGround == false || SV360.hasTakeIDSBackground == false)
                    sortingUC.SetLabel(Lang.Text("background_acquisition_failed"));
            }
            else
                sortingUC.SetLabel("");

            sortingUC.UpdateLanguage();
            thresholdingUC.UpdateLanguage();
            analysisUC.UpdateLanguage();
        }

        // bool settingsIsOpen = false;
        SettingsForm settingsForm;
        private void settings_button_Click(object sender, EventArgs e)
        {


            // Show the settings form

            // Create a new instance of the Form2 class

            if (settingsForm == null || settingsForm.IsDisposed)
            {
                settingsForm = new SettingsForm(SV360.sc);
            }

            settingsForm.Show();
            settingsForm.BringToFront();

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}
