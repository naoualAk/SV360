namespace Process_Tester
{
    partial class MainForm
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

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.btLoad = new System.Windows.Forms.Button();
            this.btExe = new System.Windows.Forms.Button();
            this.lbPath = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btPrint = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btLoad
            // 
            this.btLoad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btLoad.Location = new System.Drawing.Point(152, 107);
            this.btLoad.Name = "btLoad";
            this.btLoad.Size = new System.Drawing.Size(193, 23);
            this.btLoad.TabIndex = 0;
            this.btLoad.Text = "Choisir un fichier ou dossier";
            this.btLoad.UseVisualStyleBackColor = true;
            this.btLoad.Click += new System.EventHandler(this.btLoad_Click);
            // 
            // btExe
            // 
            this.btExe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btExe.Location = new System.Drawing.Point(152, 237);
            this.btExe.Name = "btExe";
            this.btExe.Size = new System.Drawing.Size(193, 45);
            this.btExe.TabIndex = 1;
            this.btExe.Text = "Executer process";
            this.btExe.UseVisualStyleBackColor = true;
            this.btExe.Click += new System.EventHandler(this.btExe_Click);
            // 
            // lbPath
            // 
            this.lbPath.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbPath.AutoSize = true;
            this.lbPath.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lbPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this.lbPath, 3);
            this.lbPath.Location = new System.Drawing.Point(231, 133);
            this.lbPath.Name = "lbPath";
            this.lbPath.Size = new System.Drawing.Size(37, 15);
            this.lbPath.TabIndex = 2;
            this.lbPath.Text = "label2";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Controls.Add(this.btLoad, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbPath, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btExe, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btPrint, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 56.83761F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 43.16239F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 143F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(499, 378);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // btPrint
            // 
            this.btPrint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btPrint.Location = new System.Drawing.Point(3, 237);
            this.btPrint.Name = "btPrint";
            this.btPrint.Size = new System.Drawing.Size(143, 45);
            this.btPrint.TabIndex = 3;
            this.btPrint.Text = "Afficher image";
            this.btPrint.UseVisualStyleBackColor = true;
            this.btPrint.Click += new System.EventHandler(this.btPrint_Click);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(499, 378);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
            this.Text = "Process Tester !";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btLoad;
        private System.Windows.Forms.Button btExe;
        private System.Windows.Forms.Label lbPath;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btPrint;
    }
}

