using MetroFramework.Forms;
using SV360.Automatism;
using SV360.IHM.Elements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SV360.IHM
{
    /// <summary>
    /// Interface qui permet de controler les actionneurs séparemment de l'execution du process logiciel.
    /// (Utilisé pour du debug)
    /// </summary>
    public partial class SettingsForm : MetroForm
    {

        SystemController sc;
        Identificator idUserControl;


        /// <summary>
        /// Cstor:
        ///     Set le systemController pour permettre de dialoguer avec l'automate
        /// </summary>
        /// <param name="sc">systemcontroller pour controler l'automate</param>
        public SettingsForm(SystemController sc)
        {
            InitializeComponent();

            idUserControl = new Identificator();
            Controls.Add(idUserControl);
            idUserControl.isIdentificated += ShowControl;
            idUserControl.Dock = DockStyle.Fill;
            idUserControl.Margin = new Padding(10, 10, 10, 10);

            tableLayoutPanel1.Visible = false;
            this.sc = sc;
            Update();
        }

        private void ShowControl(object sender, EventArgs e)
        {
            idUserControl.Visible = false;
            tableLayoutPanel1.Visible = true;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            Update();
        }

        /// <summary>
        /// active le vibrateur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Vibrator_Click(object sender, EventArgs e)
        {
            if (!sc.VibratorIsStarted())
            {
                sc.StartVibrator();
                metroButton1.Text = "Arreter vibrateur";
            }
            else
            {
                sc.StopVibrator();
                metroButton1.Text = "Démarrer vibrateur";
            }
        }

        /// <summary>
        /// Envoi la classe 2 dans la file des prochaines sorties de l'automate 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAutoClass_Click(object sender, EventArgs e)
        {
            sc.SetNextEject(2);
        }

        /// <summary>
        /// Update de l'interface
        /// </summary>
        void Update()
        {
            if (sc.IsRunning())
            {
                metroButton1.Text="Arreter vibrateur";
            }
            else
            {
                metroButton1.Text = "Démarrer vibrateur";
            }
        }


        /// <summary>
        /// Arrete ou demarre le convoyeur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Conveyor_Click(object sender, EventArgs e)
        {
            if (!sc.ConveyorIsStarted())
            {
                sc.StartConveyor();
                metroButton3.Text = "Arreter convoyeur";
            }
            else {
                sc.StopConveyor();
                metroButton3.Text = "Démarrer convoyeur";
            }
        }

        private void metroLabel1_Click(object sender, EventArgs e)
        {

        }


        bool ejected = false;
        private void metroButton4_Click(object sender, EventArgs e)
        {
            if (!ejected)
            {
                sc.Eject(true);
                ejected = true;
            }
            else
            {
                sc.Eject(false);
                ejected = false;
            }
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            ScannedVariable var = new ScannedVariable() { addr = 4, valueType = DataType.Integer };
            AutomateScanner.ReadVariable(var, sc.Master());
            Debug.WriteLine("Variable = " + var.currentValue);
        }
    }
}
