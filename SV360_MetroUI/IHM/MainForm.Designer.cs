namespace SV360.IHM
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.tabPageSeuils = new System.Windows.Forms.TabPage();
            this.tabPageTrier = new System.Windows.Forms.TabPage();
            this.tabPageAnalyser = new System.Windows.Forms.TabPage();
            this.settings_button = new System.Windows.Forms.Button();
            this.lang_button = new System.Windows.Forms.Button();
            this.metroTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.tabPageSeuils);
            this.metroTabControl1.Controls.Add(this.tabPageTrier);
            this.metroTabControl1.Controls.Add(this.tabPageAnalyser);
            this.metroTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTabControl1.Location = new System.Drawing.Point(23, 60);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 0;
            this.metroTabControl1.Size = new System.Drawing.Size(730, 421);
            this.metroTabControl1.Style = MetroFramework.MetroColorStyle.Red;
            this.metroTabControl1.TabIndex = 0;
            this.metroTabControl1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroTabControl1.UseSelectable = true;
            // 
            // tabPageSeuils
            // 
            this.tabPageSeuils.BackColor = System.Drawing.Color.Transparent;
            this.tabPageSeuils.Location = new System.Drawing.Point(4, 38);
            this.tabPageSeuils.Name = "tabPageSeuils";
            this.tabPageSeuils.Size = new System.Drawing.Size(722, 379);
            this.tabPageSeuils.TabIndex = 0;
            this.tabPageSeuils.Text = "Selectionner les seuils";
            // 
            // tabPageTrier
            // 
            this.tabPageTrier.Location = new System.Drawing.Point(4, 38);
            this.tabPageTrier.Name = "tabPageTrier";
            this.tabPageTrier.Size = new System.Drawing.Size(722, 379);
            this.tabPageTrier.TabIndex = 1;
            this.tabPageTrier.Text = "Lancer le tri";
            this.tabPageTrier.UseVisualStyleBackColor = true;
            this.tabPageTrier.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // tabPageAnalyser
            // 
            this.tabPageAnalyser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageAnalyser.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.tabPageAnalyser.Location = new System.Drawing.Point(4, 38);
            this.tabPageAnalyser.Name = "tabPageAnalyser";
            this.tabPageAnalyser.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tabPageAnalyser.Size = new System.Drawing.Size(722, 379);
            this.tabPageAnalyser.TabIndex = 2;
            this.tabPageAnalyser.Text = "Analyser";
            this.tabPageAnalyser.UseVisualStyleBackColor = true;
            // 
            // settings_button
            // 
            this.settings_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.settings_button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("settings_button.BackgroundImage")));
            this.settings_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.settings_button.FlatAppearance.BorderSize = 0;
            this.settings_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settings_button.Location = new System.Drawing.Point(627, 6);
            this.settings_button.Name = "settings_button";
            this.settings_button.Size = new System.Drawing.Size(20, 20);
            this.settings_button.TabIndex = 1;
            this.settings_button.UseVisualStyleBackColor = true;
            this.settings_button.Click += new System.EventHandler(this.settings_button_Click);
            // 
            // lang_button
            // 
            this.lang_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lang_button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lang_button.BackgroundImage")));
            this.lang_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.lang_button.FlatAppearance.BorderSize = 0;
            this.lang_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lang_button.Location = new System.Drawing.Point(664, 6);
            this.lang_button.Name = "lang_button";
            this.lang_button.Size = new System.Drawing.Size(21, 20);
            this.lang_button.TabIndex = 2;
            this.lang_button.UseVisualStyleBackColor = true;
            this.lang_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(776, 501);
            this.Controls.Add(this.lang_button);
            this.Controls.Add(this.settings_button);
            this.Controls.Add(this.metroTabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(23, 60, 23, 20);
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation;
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "SV360 Controller";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.metroTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private System.Windows.Forms.TabPage tabPageSeuils;
        private System.Windows.Forms.TabPage tabPageTrier;
        private System.Windows.Forms.TabPage tabPageAnalyser;
        private System.Windows.Forms.Button settings_button;
        private System.Windows.Forms.Button lang_button;
    }
}