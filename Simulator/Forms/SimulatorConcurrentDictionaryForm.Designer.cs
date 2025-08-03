namespace Simulator
{
    partial class SimulatorConcurrentDictionaryForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridViewPrices = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPrices)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewPrices
            // 
            this.dataGridViewPrices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPrices.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewPrices.Name = "dataGridViewPrices";
            this.dataGridViewPrices.Size = new System.Drawing.Size(801, 450);
            this.dataGridViewPrices.TabIndex = 0;
            // 
            // SimulatorConcurrentDictionary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 434);
            this.Controls.Add(this.dataGridViewPrices);
            this.Name = "SimulatorConcurrentDictionary";
            this.Text = "SimulatorConcurrentDictionary";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConcurrentDictionary_FormClosing);
            this.Load += new System.EventHandler(this.SimulatorConcurrentDictionary_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPrices)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewPrices;
    }
}

