namespace SV360.IHM
{
    partial class MorphoShow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MorphoShow));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelW = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelT = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labelL = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.timeLabel = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.fpsLabel = new System.Windows.Forms.Label();
            this.counterPanel = new System.Windows.Forms.Panel();
            this.counterLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.counterPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.67347F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.32653F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.32653F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.67347F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.counterPanel, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 3, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2.083333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.91666F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.91666F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2.083333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(415, 237);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Controls.Add(this.labelW);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(133, 107);
            this.panel1.TabIndex = 8;
            // 
            // labelW
            // 
            this.labelW.BackColor = System.Drawing.Color.Transparent;
            this.labelW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelW.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.labelW.Location = new System.Drawing.Point(0, 0);
            this.labelW.Name = "labelW";
            this.labelW.Size = new System.Drawing.Size(133, 107);
            this.labelW.TabIndex = 1;
            this.labelW.Text = "     8.32";
            this.labelW.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelW.TextChanged += new System.EventHandler(this.labelW_TextChanged);
            this.labelW.Click += new System.EventHandler(this.labelW_Click);
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.tableLayoutPanel1.SetColumnSpan(this.panel2, 2);
            this.panel2.Controls.Add(this.labelT);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(142, 7);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(128, 107);
            this.panel2.TabIndex = 9;
            // 
            // labelT
            // 
            this.labelT.BackColor = System.Drawing.Color.Transparent;
            this.labelT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.labelT.Location = new System.Drawing.Point(0, 0);
            this.labelT.Name = "labelT";
            this.labelT.Size = new System.Drawing.Size(128, 107);
            this.labelT.TabIndex = 2;
            this.labelT.Text = "     4.25";
            this.labelT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel3.Controls.Add(this.labelL);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(276, 7);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(136, 107);
            this.panel3.TabIndex = 10;
            // 
            // labelL
            // 
            this.labelL.BackColor = System.Drawing.Color.Transparent;
            this.labelL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelL.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.labelL.Location = new System.Drawing.Point(0, 0);
            this.labelL.Name = "labelL";
            this.labelL.Size = new System.Drawing.Size(136, 107);
            this.labelL.TabIndex = 3;
            this.labelL.Text = "     10.52";
            this.labelL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel4.BackgroundImage")));
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel4.Controls.Add(this.timeLabel);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(276, 120);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(136, 107);
            this.panel4.TabIndex = 11;
            // 
            // timeLabel
            // 
            this.timeLabel.BackColor = System.Drawing.Color.Transparent;
            this.timeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeLabel.ForeColor = System.Drawing.Color.White;
            this.timeLabel.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.timeLabel.Location = new System.Drawing.Point(0, 0);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(136, 107);
            this.timeLabel.TabIndex = 7;
            this.timeLabel.Text = "                   2min26s      ";
            this.timeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.timeLabel.Click += new System.EventHandler(this.timeLabel_Click);
            // 
            // panel5
            // 
            this.panel5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel5.BackgroundImage")));
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel5.Controls.Add(this.fpsLabel);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 120);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(133, 107);
            this.panel5.TabIndex = 12;
            // 
            // fpsLabel
            // 
            this.fpsLabel.BackColor = System.Drawing.Color.Transparent;
            this.fpsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpsLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.fpsLabel.Location = new System.Drawing.Point(0, 0);
            this.fpsLabel.Name = "fpsLabel";
            this.fpsLabel.Size = new System.Drawing.Size(133, 107);
            this.fpsLabel.TabIndex = 6;
            this.fpsLabel.Text = "      2.6";
            this.fpsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fpsLabel.Click += new System.EventHandler(this.fpsLabel_Click);
            // 
            // counterPanel
            // 
            this.counterPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("counterPanel.BackgroundImage")));
            this.counterPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.tableLayoutPanel1.SetColumnSpan(this.counterPanel, 2);
            this.counterPanel.Controls.Add(this.counterLabel);
            this.counterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.counterPanel.Location = new System.Drawing.Point(142, 120);
            this.counterPanel.Name = "counterPanel";
            this.counterPanel.Size = new System.Drawing.Size(128, 107);
            this.counterPanel.TabIndex = 13;
            // 
            // counterLabel
            // 
            this.counterLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.counterLabel.BackColor = System.Drawing.Color.Transparent;
            this.counterLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.counterLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.counterLabel.Location = new System.Drawing.Point(63, 0);
            this.counterLabel.Name = "counterLabel";
            this.counterLabel.Size = new System.Drawing.Size(65, 107);
            this.counterLabel.TabIndex = 0;
            this.counterLabel.Text = "# Seed";
            this.counterLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MorphoShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MorphoShow";
            this.Size = new System.Drawing.Size(415, 237);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.counterPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelT;
        private System.Windows.Forms.Label labelL;
        private System.Windows.Forms.Label fpsLabel;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelW;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel counterPanel;
        private System.Windows.Forms.Label counterLabel;
    }
}
