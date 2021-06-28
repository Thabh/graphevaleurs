
namespace MesurePotentiomètre
{
    partial class Form3
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
            this.components = new System.ComponentModel.Container();
            this.cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.mesuresBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.anneeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tempsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valeurDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mesuresBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // cartesianChart1
            // 
            this.cartesianChart1.Location = new System.Drawing.Point(12, 12);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(923, 445);
            this.cartesianChart1.TabIndex = 0;
            this.cartesianChart1.Text = "cartesianChart1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.anneeDataGridViewTextBoxColumn,
            this.tempsDataGridViewTextBoxColumn,
            this.valeurDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.mesuresBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 463);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(344, 152);
            this.dataGridView1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(804, 624);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(131, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Charger le graphique";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // mesuresBindingSource
            // 
            this.mesuresBindingSource.DataSource = typeof(MesurePotentiomètre.Mesures);
            // 
            // anneeDataGridViewTextBoxColumn
            // 
            this.anneeDataGridViewTextBoxColumn.DataPropertyName = "Annee";
            this.anneeDataGridViewTextBoxColumn.HeaderText = "Annee";
            this.anneeDataGridViewTextBoxColumn.Name = "anneeDataGridViewTextBoxColumn";
            // 
            // tempsDataGridViewTextBoxColumn
            // 
            this.tempsDataGridViewTextBoxColumn.DataPropertyName = "Temps";
            this.tempsDataGridViewTextBoxColumn.HeaderText = "Temps";
            this.tempsDataGridViewTextBoxColumn.Name = "tempsDataGridViewTextBoxColumn";
            // 
            // valeurDataGridViewTextBoxColumn
            // 
            this.valeurDataGridViewTextBoxColumn.DataPropertyName = "Valeur";
            this.valeurDataGridViewTextBoxColumn.HeaderText = "Valeur";
            this.valeurDataGridViewTextBoxColumn.Name = "valeurDataGridViewTextBoxColumn";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 659);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cartesianChart1);
            this.Name = "Form3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Graphique des données";
            this.Load += new System.EventHandler(this.Form3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mesuresBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private LiveCharts.WinForms.CartesianChart cartesianChart1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource mesuresBindingSource;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn anneeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tempsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn valeurDataGridViewTextBoxColumn;
    }
}