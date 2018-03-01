
using System;

namespace SV360.IHM.PageTab
{
    partial class SortingUC
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.metroProgressSpinner1 = new MetroFramework.Controls.MetroProgressSpinner();
            this.chartPie = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanelLeft = new System.Windows.Forms.TableLayoutPanel();
            this.sortingImageBox = new Emgu.CV.UI.ImageBox();
            this.pauseBt = new System.Windows.Forms.Button();
            this.stopBt = new System.Windows.Forms.Button();
            this.stopLabel = new System.Windows.Forms.Label();
            this.playLabel = new System.Windows.Forms.Label();
            this.pauseLabel = new System.Windows.Forms.Label();
            this.startBt = new System.Windows.Forms.Button();
            this.tableLayoutPanelRight = new System.Windows.Forms.TableLayoutPanel();
            this.morphoShow = new SV360.IHM.MorphoShow();
            ((System.ComponentModel.ISupportInitialize)(this.chartPie)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sortingImageBox)).BeginInit();
            this.tableLayoutPanelRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroProgressSpinner1
            // 
            this.metroProgressSpinner1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.metroProgressSpinner1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanelLeft.SetColumnSpan(this.metroProgressSpinner1, 3);
            this.metroProgressSpinner1.Cursor = System.Windows.Forms.Cursors.Default;
            this.metroProgressSpinner1.Enabled = false;
            this.metroProgressSpinner1.Location = new System.Drawing.Point(91, 128);
            this.metroProgressSpinner1.Maximum = 100;
            this.metroProgressSpinner1.MaximumSize = new System.Drawing.Size(150, 150);
            this.metroProgressSpinner1.MinimumSize = new System.Drawing.Size(70, 70);
            this.metroProgressSpinner1.Name = "metroProgressSpinner1";
            this.metroProgressSpinner1.Size = new System.Drawing.Size(70, 70);
            this.metroProgressSpinner1.Speed = 2F;
            this.metroProgressSpinner1.Style = MetroFramework.MetroColorStyle.Red;
            this.metroProgressSpinner1.TabIndex = 4;
            this.metroProgressSpinner1.UseCustomBackColor = true;
            this.metroProgressSpinner1.UseSelectable = true;
            this.metroProgressSpinner1.UseStyleColors = true;
            this.metroProgressSpinner1.Value = 30;
            this.metroProgressSpinner1.Visible = false;
            this.metroProgressSpinner1.Click += new System.EventHandler(this.metroProgressSpinner1_Click);
            // 
            // chartPie
            // 
            this.chartPie.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartPie.BackColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea1";
            this.chartPie.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartPie.Legends.Add(legend1);
            this.chartPie.Location = new System.Drawing.Point(3, 3);
            this.chartPie.Name = "chartPie";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartPie.Series.Add(series1);
            this.chartPie.Size = new System.Drawing.Size(435, 247);
            this.chartPie.TabIndex = 1;
            this.chartPie.Text = "chart1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanelLeft);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanelRight);
            this.splitContainer1.Size = new System.Drawing.Size(697, 507);
            this.splitContainer1.SplitterDistance = 252;
            this.splitContainer1.TabIndex = 2;
            // 
            // tableLayoutPanelLeft
            // 
            this.tableLayoutPanelLeft.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tableLayoutPanelLeft.ColumnCount = 3;
            this.tableLayoutPanelLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanelLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanelLeft.Controls.Add(this.sortingImageBox, 0, 4);
            this.tableLayoutPanelLeft.Controls.Add(this.pauseBt, 2, 1);
            this.tableLayoutPanelLeft.Controls.Add(this.stopBt, 0, 1);
            this.tableLayoutPanelLeft.Controls.Add(this.stopLabel, 0, 2);
            this.tableLayoutPanelLeft.Controls.Add(this.playLabel, 1, 2);
            this.tableLayoutPanelLeft.Controls.Add(this.metroProgressSpinner1, 0, 3);
            this.tableLayoutPanelLeft.Controls.Add(this.pauseLabel, 2, 2);
            this.tableLayoutPanelLeft.Controls.Add(this.startBt, 1, 1);
            this.tableLayoutPanelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelLeft.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelLeft.Name = "tableLayoutPanelLeft";
            this.tableLayoutPanelLeft.RowCount = 5;
            this.tableLayoutPanelLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tableLayoutPanelLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanelLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanelLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelLeft.Size = new System.Drawing.Size(252, 507);
            this.tableLayoutPanelLeft.TabIndex = 0;
            this.tableLayoutPanelLeft.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanelLeft_Paint);
            // 
            // sortingImageBox
            // 
            this.sortingImageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanelLeft.SetColumnSpan(this.sortingImageBox, 3);
            this.sortingImageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sortingImageBox.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.sortingImageBox.Location = new System.Drawing.Point(3, 254);
            this.sortingImageBox.Name = "sortingImageBox";
            this.sortingImageBox.Size = new System.Drawing.Size(246, 250);
            this.sortingImageBox.TabIndex = 2;
            this.sortingImageBox.TabStop = false;
            this.sortingImageBox.WaitOnLoad = true;
            this.sortingImageBox.Click += new System.EventHandler(this.sortingImageBox_Click);
            // 
            // pauseBt
            // 
            this.pauseBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.pauseBt.BackColor = System.Drawing.Color.Transparent;
            this.pauseBt.BackgroundImage = global::SV360.Properties.Resources.pause_circular_button;
            this.pauseBt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pauseBt.FlatAppearance.BorderSize = 0;
            this.pauseBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pauseBt.Location = new System.Drawing.Point(170, 73);
            this.pauseBt.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pauseBt.MaximumSize = new System.Drawing.Size(150, 150);
            this.pauseBt.Name = "pauseBt";
            this.pauseBt.Size = new System.Drawing.Size(79, 32);
            this.pauseBt.TabIndex = 7;
            this.pauseBt.UseVisualStyleBackColor = false;
            this.pauseBt.Click += new System.EventHandler(this.stopBt_Click);
            // 
            // stopBt
            // 
            this.stopBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.stopBt.BackColor = System.Drawing.Color.Transparent;
            this.stopBt.BackgroundImage = global::SV360.Properties.Resources.stop;
            this.stopBt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.stopBt.FlatAppearance.BorderSize = 0;
            this.stopBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stopBt.Location = new System.Drawing.Point(3, 73);
            this.stopBt.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.stopBt.MaximumSize = new System.Drawing.Size(150, 150);
            this.stopBt.Name = "stopBt";
            this.stopBt.Size = new System.Drawing.Size(77, 32);
            this.stopBt.TabIndex = 8;
            this.stopBt.UseVisualStyleBackColor = false;
            this.stopBt.Click += new System.EventHandler(this.stopBt_Click_1);
            // 
            // stopLabel
            // 
            this.stopLabel.AutoSize = true;
            this.stopLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.stopLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.stopLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.stopLabel.Location = new System.Drawing.Point(3, 105);
            this.stopLabel.Name = "stopLabel";
            this.stopLabel.Size = new System.Drawing.Size(77, 13);
            this.stopLabel.TabIndex = 9;
            this.stopLabel.Text = "Stop";
            this.stopLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.stopLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // playLabel
            // 
            this.playLabel.AutoSize = true;
            this.playLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.playLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.playLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.playLabel.Location = new System.Drawing.Point(86, 105);
            this.playLabel.Name = "playLabel";
            this.playLabel.Size = new System.Drawing.Size(78, 13);
            this.playLabel.TabIndex = 10;
            this.playLabel.Text = "Play";
            this.playLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pauseLabel
            // 
            this.pauseLabel.AutoSize = true;
            this.pauseLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.pauseLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.pauseLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pauseLabel.Location = new System.Drawing.Point(170, 105);
            this.pauseLabel.Name = "pauseLabel";
            this.pauseLabel.Size = new System.Drawing.Size(79, 13);
            this.pauseLabel.TabIndex = 12;
            this.pauseLabel.Text = "Pause";
            this.pauseLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.pauseLabel.Click += new System.EventHandler(this.label3_Click);
            // 
            // startBt
            // 
            this.startBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.startBt.BackColor = System.Drawing.Color.Transparent;
            this.startBt.BackgroundImage = global::SV360.Properties.Resources.play_button;
            this.startBt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.startBt.FlatAppearance.BorderSize = 0;
            this.startBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startBt.Location = new System.Drawing.Point(87, 73);
            this.startBt.MaximumSize = new System.Drawing.Size(150, 150);
            this.startBt.Name = "startBt";
            this.startBt.Size = new System.Drawing.Size(75, 29);
            this.startBt.TabIndex = 13;
            this.startBt.UseVisualStyleBackColor = true;
            this.startBt.Click += new System.EventHandler(this.startBt_Click);
            // 
            // tableLayoutPanelRight
            // 
            this.tableLayoutPanelRight.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanelRight.ColumnCount = 1;
            this.tableLayoutPanelRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelRight.Controls.Add(this.morphoShow, 0, 1);
            this.tableLayoutPanelRight.Controls.Add(this.chartPie, 0, 0);
            this.tableLayoutPanelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelRight.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelRight.Name = "tableLayoutPanelRight";
            this.tableLayoutPanelRight.RowCount = 2;
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelRight.Size = new System.Drawing.Size(441, 507);
            this.tableLayoutPanelRight.TabIndex = 0;
            // 
            // morphoShow
            // 
            this.morphoShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.morphoShow.Location = new System.Drawing.Point(3, 256);
            this.morphoShow.Name = "morphoShow";
            this.morphoShow.Size = new System.Drawing.Size(435, 248);
            this.morphoShow.TabIndex = 3;
            // 
            // SortingUC2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "SortingUC2";
            this.Size = new System.Drawing.Size(697, 507);
            ((System.ComponentModel.ISupportInitialize)(this.chartPie)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanelLeft.ResumeLayout(false);
            this.tableLayoutPanelLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sortingImageBox)).EndInit();
            this.tableLayoutPanelRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Emgu.CV.UI.ImageBox sortingImageBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPie;
        private MorphoShow morphoShow;
        private MetroFramework.Controls.MetroProgressSpinner metroProgressSpinner1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelLeft;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelRight;
        private System.Windows.Forms.Button pauseBt;
        private System.Windows.Forms.Button stopBt;
        private System.Windows.Forms.Label stopLabel;
        private System.Windows.Forms.Label playLabel;
        private System.Windows.Forms.Label pauseLabel;
        private System.Windows.Forms.Button startBt;
    }
}
