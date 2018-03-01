namespace SV360.IHM.PageTab
{
    public partial class AnalysisUC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnalysisUC));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripBtExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.mainTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.chartSplitContainer = new System.Windows.Forms.SplitContainer();
            this.menuTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.menuButtonC4 = new MetroFramework.Controls.MetroButton();
            this.menuButtonC3 = new MetroFramework.Controls.MetroButton();
            this.menuButtonC2 = new MetroFramework.Controls.MetroButton();
            this.menuButtonC1 = new MetroFramework.Controls.MetroButton();
            this.menuButtonGeneral = new MetroFramework.Controls.MetroButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statsTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.toolStrip.SuspendLayout();
            this.mainTableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartSplitContainer)).BeginInit();
            this.chartSplitContainer.Panel1.SuspendLayout();
            this.chartSplitContainer.Panel2.SuspendLayout();
            this.chartSplitContainer.SuspendLayout();
            this.menuTableLayout.SuspendLayout();
            this.panel1.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(35, 35);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripBtExcel,
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip.Size = new System.Drawing.Size(688, 40);
            this.toolStrip.Stretch = true;
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
            this.toolStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip_ItemClicked);
            // 
            // toolStripBtExcel
            // 
            this.toolStripBtExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtExcel.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtExcel.Image")));
            this.toolStripBtExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtExcel.Name = "toolStripBtExcel";
            this.toolStripBtExcel.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripBtExcel.RightToLeftAutoMirrorImage = true;
            this.toolStripBtExcel.Size = new System.Drawing.Size(39, 37);
            this.toolStripBtExcel.Text = "Sauvegarder en fichier excel sous...";
            this.toolStripBtExcel.Click += new System.EventHandler(this.toolStripBtExcel_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(39, 37);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(39, 37);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.BackColor = System.Drawing.Color.Transparent;
            this.toolStripButton3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(39, 37);
            this.toolStripButton3.Text = "Statistiques";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // mainTableLayout
            // 
            this.mainTableLayout.AutoSize = true;
            this.mainTableLayout.ColumnCount = 1;
            this.mainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayout.Controls.Add(this.chartSplitContainer, 0, 1);
            this.mainTableLayout.Controls.Add(this.toolStrip, 0, 0);
            this.mainTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayout.Location = new System.Drawing.Point(0, 2);
            this.mainTableLayout.Name = "mainTableLayout";
            this.mainTableLayout.RowCount = 2;
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayout.Size = new System.Drawing.Size(688, 492);
            this.mainTableLayout.TabIndex = 3;
            // 
            // chartSplitContainer
            // 
            this.chartSplitContainer.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.chartSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.chartSplitContainer.IsSplitterFixed = true;
            this.chartSplitContainer.Location = new System.Drawing.Point(0, 40);
            this.chartSplitContainer.Margin = new System.Windows.Forms.Padding(0);
            this.chartSplitContainer.Name = "chartSplitContainer";
            // 
            // chartSplitContainer.Panel1
            // 
            this.chartSplitContainer.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chartSplitContainer.Panel1.Controls.Add(this.menuTableLayout);
            this.chartSplitContainer.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // chartSplitContainer.Panel2
            // 
            this.chartSplitContainer.Panel2.Controls.Add(this.panel1);
            this.chartSplitContainer.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chartSplitContainer.Size = new System.Drawing.Size(688, 452);
            this.chartSplitContainer.SplitterDistance = 159;
            this.chartSplitContainer.TabIndex = 1;
            // 
            // menuTableLayout
            // 
            this.menuTableLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.menuTableLayout.ColumnCount = 1;
            this.menuTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.menuTableLayout.Controls.Add(this.menuButtonC4, 0, 4);
            this.menuTableLayout.Controls.Add(this.menuButtonC3, 0, 3);
            this.menuTableLayout.Controls.Add(this.menuButtonC2, 0, 2);
            this.menuTableLayout.Controls.Add(this.menuButtonC1, 0, 1);
            this.menuTableLayout.Controls.Add(this.menuButtonGeneral, 0, 0);
            this.menuTableLayout.Location = new System.Drawing.Point(0, 12);
            this.menuTableLayout.Margin = new System.Windows.Forms.Padding(0);
            this.menuTableLayout.Name = "menuTableLayout";
            this.menuTableLayout.RowCount = 5;
            this.menuTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.9992F));
            this.menuTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.9992F));
            this.menuTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.9992F));
            this.menuTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.9992F));
            this.menuTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0032F));
            this.menuTableLayout.Size = new System.Drawing.Size(159, 217);
            this.menuTableLayout.TabIndex = 2;
            // 
            // menuButtonC4
            // 
            this.menuButtonC4.BackColor = System.Drawing.Color.Transparent;
            this.menuButtonC4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuButtonC4.Location = new System.Drawing.Point(0, 175);
            this.menuButtonC4.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.menuButtonC4.Name = "menuButtonC4";
            this.menuButtonC4.Size = new System.Drawing.Size(159, 39);
            this.menuButtonC4.TabIndex = 4;
            this.menuButtonC4.Text = "Classe 4";
            this.menuButtonC4.UseSelectable = true;
            // 
            // menuButtonC3
            // 
            this.menuButtonC3.BackColor = System.Drawing.Color.Transparent;
            this.menuButtonC3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuButtonC3.Location = new System.Drawing.Point(0, 132);
            this.menuButtonC3.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.menuButtonC3.Name = "menuButtonC3";
            this.menuButtonC3.Size = new System.Drawing.Size(159, 37);
            this.menuButtonC3.TabIndex = 3;
            this.menuButtonC3.Text = "Classe 3";
            this.menuButtonC3.UseSelectable = true;
            this.menuButtonC3.Click += new System.EventHandler(this.menuButtonC3_Click);
            // 
            // menuButtonC2
            // 
            this.menuButtonC2.BackColor = System.Drawing.Color.Transparent;
            this.menuButtonC2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuButtonC2.Location = new System.Drawing.Point(0, 89);
            this.menuButtonC2.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.menuButtonC2.Name = "menuButtonC2";
            this.menuButtonC2.Size = new System.Drawing.Size(159, 37);
            this.menuButtonC2.TabIndex = 2;
            this.menuButtonC2.Text = "Classe 2";
            this.menuButtonC2.UseSelectable = true;
            this.menuButtonC2.Click += new System.EventHandler(this.menuButtonC2_Click);
            // 
            // menuButtonC1
            // 
            this.menuButtonC1.BackColor = System.Drawing.Color.Transparent;
            this.menuButtonC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuButtonC1.Location = new System.Drawing.Point(0, 46);
            this.menuButtonC1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.menuButtonC1.Name = "menuButtonC1";
            this.menuButtonC1.Size = new System.Drawing.Size(159, 37);
            this.menuButtonC1.TabIndex = 1;
            this.menuButtonC1.Text = "Classe 1";
            this.menuButtonC1.UseSelectable = true;
            this.menuButtonC1.Click += new System.EventHandler(this.menuButtonC1_Click);
            // 
            // menuButtonGeneral
            // 
            this.menuButtonGeneral.BackColor = System.Drawing.Color.WhiteSmoke;
            this.menuButtonGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuButtonGeneral.Location = new System.Drawing.Point(0, 3);
            this.menuButtonGeneral.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.menuButtonGeneral.Name = "menuButtonGeneral";
            this.menuButtonGeneral.Size = new System.Drawing.Size(159, 37);
            this.menuButtonGeneral.TabIndex = 0;
            this.menuButtonGeneral.Text = "General";
            this.menuButtonGeneral.Theme = MetroFramework.MetroThemeStyle.Light;
            this.menuButtonGeneral.UseCustomBackColor = true;
            this.menuButtonGeneral.UseSelectable = true;
            this.menuButtonGeneral.Click += new System.EventHandler(this.menuButtonGeneral_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.statsTableLayout);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(525, 452);
            this.panel1.TabIndex = 1;
            // 
            // statsTableLayout
            // 
            this.statsTableLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statsTableLayout.ColumnCount = 1;
            this.statsTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.statsTableLayout.Location = new System.Drawing.Point(0, 0);
            this.statsTableLayout.Margin = new System.Windows.Forms.Padding(0);
            this.statsTableLayout.MinimumSize = new System.Drawing.Size(0, 800);
            this.statsTableLayout.Name = "statsTableLayout";
            this.statsTableLayout.Padding = new System.Windows.Forms.Padding(20);
            this.statsTableLayout.RowCount = 3;
            this.statsTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.statsTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.statsTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.statsTableLayout.Size = new System.Drawing.Size(474, 800);
            this.statsTableLayout.TabIndex = 0;
            // 
            // mainPanel
            // 
            this.mainPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mainPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.mainPanel.Controls.Add(this.mainTableLayout);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 10);
            this.mainPanel.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.mainPanel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mainPanel.Size = new System.Drawing.Size(688, 494);
            this.mainPanel.TabIndex = 2;
            // 
            // AnalysisUC
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.mainPanel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "AnalysisUC";
            this.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Size = new System.Drawing.Size(688, 504);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.mainTableLayout.ResumeLayout(false);
            this.mainTableLayout.PerformLayout();
            this.chartSplitContainer.Panel1.ResumeLayout(false);
            this.chartSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartSplitContainer)).EndInit();
            this.chartSplitContainer.ResumeLayout(false);
            this.menuTableLayout.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripBtExcel;
        private System.Windows.Forms.TableLayoutPanel mainTableLayout;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.SplitContainer chartSplitContainer;
        private System.Windows.Forms.TableLayoutPanel menuTableLayout;
        private MetroFramework.Controls.MetroButton menuButtonC4;
        private MetroFramework.Controls.MetroButton menuButtonC3;
        private MetroFramework.Controls.MetroButton menuButtonC2;
        private MetroFramework.Controls.MetroButton menuButtonC1;
        private MetroFramework.Controls.MetroButton menuButtonGeneral;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.TableLayoutPanel statsTableLayout;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
    }
}
