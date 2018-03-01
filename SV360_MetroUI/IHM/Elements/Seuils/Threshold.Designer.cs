namespace SV360.IHM.Elements.Seuils
{
    partial class Threshold
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Threshold));
            this.boxDecimal = new MetroFramework.Controls.MetroComboBox();
            this.boxCritere = new MetroFramework.Controls.MetroComboBox();
            this.boxUnit = new MetroFramework.Controls.MetroComboBox();
            this.btCancel = new MetroFramework.Controls.MetroButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btStart = new MetroFramework.Controls.MetroButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // boxDecimal
            // 
            this.boxDecimal.AllowDrop = true;
            this.boxDecimal.DisplayFocus = true;
            this.boxDecimal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boxDecimal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.boxDecimal.ForeColor = System.Drawing.Color.Red;
            this.boxDecimal.FormattingEnabled = true;
            this.boxDecimal.ItemHeight = 23;
            this.boxDecimal.Items.AddRange(new object[] {
            ",0",
            ",25",
            ",50",
            ",75"});
            this.boxDecimal.Location = new System.Drawing.Point(437, 3);
            this.boxDecimal.Name = "boxDecimal";
            this.boxDecimal.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.boxDecimal.Size = new System.Drawing.Size(211, 29);
            this.boxDecimal.Sorted = true;
            this.boxDecimal.TabIndex = 3;
            this.boxDecimal.UseCustomBackColor = true;
            this.boxDecimal.UseCustomForeColor = true;
            this.boxDecimal.UseSelectable = true;
            this.boxDecimal.UseStyleColors = true;
            this.boxDecimal.SelectedValueChanged += new System.EventHandler(this.Next_Click);
            // 
            // boxCritere
            // 
            this.boxCritere.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boxCritere.FormattingEnabled = true;
            this.boxCritere.ItemHeight = 23;
            this.boxCritere.Items.AddRange(new object[] {
            "epaisseur",
            "largeur",
            "longueur"});
            this.boxCritere.Location = new System.Drawing.Point(3, 3);
            this.boxCritere.Name = "boxCritere";
            this.boxCritere.Size = new System.Drawing.Size(211, 29);
            this.boxCritere.TabIndex = 1;
            this.boxCritere.UseCustomBackColor = true;
            this.boxCritere.UseCustomForeColor = true;
            this.boxCritere.UseSelectable = true;
            this.boxCritere.UseStyleColors = true;
            this.boxCritere.SelectedValueChanged += new System.EventHandler(this.Next_Click);
            // 
            // boxUnit
            // 
            this.boxUnit.BackColor = System.Drawing.SystemColors.Window;
            this.boxUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boxUnit.ForeColor = System.Drawing.Color.Red;
            this.boxUnit.FormattingEnabled = true;
            this.boxUnit.ItemHeight = 23;
            this.boxUnit.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19"});
            this.boxUnit.Location = new System.Drawing.Point(220, 3);
            this.boxUnit.Name = "boxUnit";
            this.boxUnit.Size = new System.Drawing.Size(211, 29);
            this.boxUnit.TabIndex = 2;
            this.boxUnit.UseCustomBackColor = true;
            this.boxUnit.UseCustomForeColor = true;
            this.boxUnit.UseSelectable = true;
            this.boxUnit.UseStyleColors = true;
            this.boxUnit.SelectedValueChanged += new System.EventHandler(this.Next_Click);
            // 
            // btCancel
            // 
            this.btCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btCancel.BackgroundImage")));
            this.btCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btCancel.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btCancel.ForeColor = System.Drawing.Color.White;
            this.btCancel.Location = new System.Drawing.Point(654, 3);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(36, 31);
            this.btCancel.Style = MetroFramework.MetroColorStyle.Blue;
            this.btCancel.TabIndex = 4;
            this.btCancel.UseCustomBackColor = true;
            this.btCancel.UseCustomForeColor = true;
            this.btCancel.UseSelectable = true;
            this.btCancel.UseStyleColors = true;
            this.btCancel.Click += new System.EventHandler(this.Back_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btStart, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(699, 85);
            this.tableLayoutPanel1.TabIndex = 6;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // btStart
            // 
            this.btStart.BackColor = System.Drawing.SystemColors.Highlight;
            this.btStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btStart.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btStart.ForeColor = System.Drawing.Color.White;
            this.btStart.Location = new System.Drawing.Point(3, 3);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(693, 37);
            this.btStart.Style = MetroFramework.MetroColorStyle.Blue;
            this.btStart.TabIndex = 5;
            this.btStart.Text = "Initialisation";
            this.btStart.UseCustomBackColor = true;
            this.btStart.UseCustomForeColor = true;
            this.btStart.UseMnemonic = false;
            this.btStart.UseSelectable = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.btCancel, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.boxDecimal, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.boxUnit, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.boxCritere, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.ForeColor = System.Drawing.Color.Red;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 46);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(693, 37);
            this.tableLayoutPanel2.TabIndex = 6;
            this.tableLayoutPanel2.Visible = false;
            // 
            // Threshold
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Threshold";
            this.Size = new System.Drawing.Size(699, 85);
            this.UseCustomBackColor = true;
            this.UseCustomForeColor = true;
            this.UseStyleColors = true;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroComboBox boxDecimal;
        private MetroFramework.Controls.MetroComboBox boxCritere;
        private MetroFramework.Controls.MetroComboBox boxUnit;
        private MetroFramework.Controls.MetroButton btCancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MetroFramework.Controls.MetroButton btStart;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}
