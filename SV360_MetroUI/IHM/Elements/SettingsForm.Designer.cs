namespace SV360.IHM
{
    partial class SettingsForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.metroButton4 = new MetroFramework.Controls.MetroButton();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.titleAutomatisme = new MetroFramework.Controls.MetroLabel();
            this.metroButton2 = new MetroFramework.Controls.MetroButton();
            this.metroButton3 = new MetroFramework.Controls.MetroButton();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.btTakeAcq = new MetroFramework.Controls.MetroButton();
            this.metroButton5 = new MetroFramework.Controls.MetroButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.45992F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.54008F));
            this.tableLayoutPanel1.Controls.Add(this.metroButton5, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.metroButton4, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.metroButton1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.titleAutomatisme, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.metroButton2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.metroButton3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.metroLabel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btTakeAcq, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(20, 60);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(474, 351);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // metroButton4
            // 
            this.metroButton4.Location = new System.Drawing.Point(3, 123);
            this.metroButton4.Name = "metroButton4";
            this.metroButton4.Size = new System.Drawing.Size(199, 23);
            this.metroButton4.TabIndex = 6;
            this.metroButton4.Text = "Ejecter";
            this.metroButton4.UseSelectable = true;
            this.metroButton4.Click += new System.EventHandler(this.metroButton4_Click);
            // 
            // metroButton1
            // 
            this.metroButton1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroButton1.Location = new System.Drawing.Point(3, 33);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(200, 24);
            this.metroButton1.TabIndex = 0;
            this.metroButton1.Text = "Demarrer vibrateur";
            this.metroButton1.UseSelectable = true;
            this.metroButton1.Click += new System.EventHandler(this.Vibrator_Click);
            // 
            // titleAutomatisme
            // 
            this.titleAutomatisme.AutoSize = true;
            this.titleAutomatisme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titleAutomatisme.Location = new System.Drawing.Point(3, 0);
            this.titleAutomatisme.Name = "titleAutomatisme";
            this.titleAutomatisme.Size = new System.Drawing.Size(200, 30);
            this.titleAutomatisme.TabIndex = 2;
            this.titleAutomatisme.Text = "Automatisme";
            this.titleAutomatisme.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // metroButton2
            // 
            this.metroButton2.Location = new System.Drawing.Point(3, 93);
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.Size = new System.Drawing.Size(199, 23);
            this.metroButton2.TabIndex = 1;
            this.metroButton2.Text = "Envoyer grain dans classe 1";
            this.metroButton2.UseSelectable = true;
            this.metroButton2.Click += new System.EventHandler(this.ButtonAutoClass_Click);
            // 
            // metroButton3
            // 
            this.metroButton3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroButton3.Location = new System.Drawing.Point(3, 63);
            this.metroButton3.Name = "metroButton3";
            this.metroButton3.Size = new System.Drawing.Size(200, 24);
            this.metroButton3.TabIndex = 3;
            this.metroButton3.Text = "Demarrer convoyeur";
            this.metroButton3.UseSelectable = true;
            this.metroButton3.Click += new System.EventHandler(this.Conveyor_Click);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.metroLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroLabel1.Location = new System.Drawing.Point(209, 0);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(262, 30);
            this.metroLabel1.TabIndex = 4;
            this.metroLabel1.Text = "Vision";
            this.metroLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroLabel1.Click += new System.EventHandler(this.metroLabel1_Click);
            // 
            // btTakeAcq
            // 
            this.btTakeAcq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btTakeAcq.Location = new System.Drawing.Point(209, 33);
            this.btTakeAcq.Name = "btTakeAcq";
            this.btTakeAcq.Size = new System.Drawing.Size(262, 24);
            this.btTakeAcq.TabIndex = 5;
            this.btTakeAcq.Text = "Prendre acquisition";
            this.btTakeAcq.UseSelectable = true;
            // 
            // metroButton5
            // 
            this.metroButton5.Location = new System.Drawing.Point(3, 153);
            this.metroButton5.Name = "metroButton5";
            this.metroButton5.Size = new System.Drawing.Size(199, 23);
            this.metroButton5.TabIndex = 7;
            this.metroButton5.Text = "Lire M1";
            this.metroButton5.UseSelectable = true;
            this.metroButton5.Click += new System.EventHandler(this.metroButton5_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 431);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MetroFramework.Controls.MetroButton metroButton1;
        private MetroFramework.Controls.MetroButton metroButton2;
        private MetroFramework.Controls.MetroLabel titleAutomatisme;
        private MetroFramework.Controls.MetroButton metroButton3;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroButton btTakeAcq;
        private MetroFramework.Controls.MetroButton metroButton4;
        private MetroFramework.Controls.MetroButton metroButton5;
    }
}