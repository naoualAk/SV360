using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SV360.IHM
{
    /// <summary>
    /// UserControl permettant d'afficher les données relatif à une graine lors de la phase tri.
    /// </summary>
    public partial class MorphoShow : UserControl
    {
        /// <summary>
        /// Cstr : Initialise les graphisme.
        /// </summary>
        public MorphoShow()
        {
            InitializeComponent();
            Resize += new EventHandler(UpdateSize);
        }

        delegate void ShowFeaturesCallback(float width, float thickness, float length);
        delegate void ShowTimeCallback(TimeSpan elapsed, float fps);

        /// <summary>
        /// Permet d'afficher les données liées à la largeur,l'épaisseur, et la longueur
        /// </summary>
        /// <param name="width"></param>
        /// <param name="thickness"></param>
        /// <param name="length"></param>
        public void ShowFeatures(float width, float thickness, float length)
        {
            if (this.InvokeRequired)
            {
                ShowFeaturesCallback v = new ShowFeaturesCallback(ShowFeatures);
                this.Invoke(v, new object[] { width, thickness, length });
            }
            else
            {
                labelW.Text = "      " + width.ToString();
                labelT.Text = "      " + thickness.ToString();
                labelL.Text = "      " + length.ToString();
            }
        }


        /// <summary>
        /// affiche le nombre de grains passés
        /// </summary>
        /// <param name="value"></param>
        public void SetCounter(int value)
        {
            SetText(counterLabel, "            " + value.ToString());
        }

        /// <summary>
        /// affiche le nombre de grains passés
        /// </summary>
        public void SetCounter(string mess)
        {
            SetText(counterLabel, mess);
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

        /// <summary>
        /// Efface toutes les données dans le usercontrol
        /// </summary>
        delegate void ResetCallback();
        public void Reset()
        {
            if (this.InvokeRequired)
            {
                ResetCallback v = new ResetCallback(Reset);
                this.Invoke(v, new object[] { });
            }
            else
            {
                labelW.Text = "";
                labelT.Text = "";
                labelL.Text = "";
                timeLabel.Text = "";
                fpsLabel.Text = "";
                SetCounter("");
            }
        }

        private void labelW_TextChanged(object sender, EventArgs e)
        {

        }

        private void fpsLabel_Click(object sender, EventArgs e)
        {

        }

        float timeLabelCoefSize = 1F;

        /// <summary>
        /// Affiche le temps écoulé depuis le dernier grain passé, et le nombre de grains par seconde (mésurée sur les 3 dernieres grains)
        /// </summary>
        /// <param name="elapsed">temps écoulé</param>
        /// <param name="fps">nombre de grains par seconde</param>
        internal void ShowTime(TimeSpan elapsed, float fps)
        { 

            if (this.InvokeRequired)
            {
                ShowTimeCallback v = new ShowTimeCallback(ShowTime);
                this.Invoke(v, new object[] { elapsed, fps });
            }
            else
            {
                fpsLabel.Text = "         " + fps.ToString("0.00");

                if (elapsed.Hours != 0)
                {
                    timeLabel.Text = "           " + elapsed.Hours + "h" + elapsed.Minutes + "mn" + elapsed.Seconds + "s";
                    timeLabelCoefSize = 0.8F;
                }
                else if (elapsed.Minutes != 0)
                {
                    timeLabelCoefSize = 0.9F;
                    timeLabel.Text = "           " + elapsed.Minutes + "mn" + elapsed.Seconds + "s" ;
                }
                else
                {
                    timeLabelCoefSize = 1F;
                    timeLabel.Text = "        " + elapsed.Seconds + "s" ;
                }
                    
            }

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labelW_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Update des tailles des textes en fonction de la taille du usercontrol
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateSize(object sender, EventArgs e)
        {
            int min = panel1.Size.Height < panel1.Size.Width ? panel1.Size.Height : panel1.Size.Width;
            int size = min / 12 < 5 ? 5 : min / 12;
            labelL.Font = new Font(Font.FontFamily, size, Font.Style);
            labelT.Font = new Font(Font.FontFamily, size, Font.Style);
            labelW.Font = new Font(Font.FontFamily, size, Font.Style);
            timeLabel.Font = new Font(Font.FontFamily, (int)(size* timeLabelCoefSize), Font.Style);
            fpsLabel.Font = new Font(Font.FontFamily, size, Font.Style);
            counterLabel.Font = new Font(Font.FontFamily, size*0.75F, Font.Style);
            counterLabel.Size = new Size(counterPanel.Size.Width / 2, counterPanel.Size.Height);
            counterLabel.Location = new Point(counterPanel.Width / 2, 0);

        }

        private void timeLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
