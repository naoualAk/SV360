namespace SV360.IHM.Elements
{
    partial class Identificator
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label_id = new System.Windows.Forms.Label();
            this.label_mdp = new System.Windows.Forms.Label();
            this.button_valider = new System.Windows.Forms.Button();
            this.textBox_id = new System.Windows.Forms.TextBox();
            this.textBox_mdp = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.textBox_mdp, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label_id, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label_mdp, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.button_valider, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.textBox_id, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.9992F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.9992F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.9992F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.9992F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0032F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(249, 260);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label_id
            // 
            this.label_id.AutoSize = true;
            this.label_id.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_id.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_id.Location = new System.Drawing.Point(3, 0);
            this.label_id.Name = "label_id";
            this.label_id.Size = new System.Drawing.Size(243, 51);
            this.label_id.TabIndex = 0;
            this.label_id.Text = "Identifiant";
            this.label_id.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label_mdp
            // 
            this.label_mdp.AutoSize = true;
            this.label_mdp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_mdp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_mdp.Location = new System.Drawing.Point(3, 102);
            this.label_mdp.Name = "label_mdp";
            this.label_mdp.Size = new System.Drawing.Size(243, 51);
            this.label_mdp.TabIndex = 1;
            this.label_mdp.Text = "Mot de passe";
            this.label_mdp.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // button_valider
            // 
            this.button_valider.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button_valider.Location = new System.Drawing.Point(87, 207);
            this.button_valider.Name = "button_valider";
            this.button_valider.Size = new System.Drawing.Size(75, 23);
            this.button_valider.TabIndex = 2;
            this.button_valider.Text = "Valider";
            this.button_valider.UseVisualStyleBackColor = true;
            this.button_valider.Click += new System.EventHandler(this.button_valider_Click);
            // 
            // textBox_id
            // 
            this.textBox_id.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBox_id.Location = new System.Drawing.Point(74, 54);
            this.textBox_id.MinimumSize = new System.Drawing.Size(100, 0);
            this.textBox_id.Name = "textBox_id";
            this.textBox_id.Size = new System.Drawing.Size(100, 20);
            this.textBox_id.TabIndex = 3;
            // 
            // textBox_mdp
            // 
            this.textBox_mdp.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBox_mdp.Location = new System.Drawing.Point(74, 156);
            this.textBox_mdp.MinimumSize = new System.Drawing.Size(100, 0);
            this.textBox_mdp.Name = "textBox_mdp";
            this.textBox_mdp.Size = new System.Drawing.Size(100, 20);
            this.textBox_mdp.TabIndex = 4;
            // 
            // Identificator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Identificator";
            this.Size = new System.Drawing.Size(249, 260);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox textBox_mdp;
        private System.Windows.Forms.Label label_id;
        private System.Windows.Forms.Label label_mdp;
        private System.Windows.Forms.Button button_valider;
        private System.Windows.Forms.TextBox textBox_id;
    }
}
