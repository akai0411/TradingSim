namespace Simulator
{
    partial class SimulatorBlockingCollectionForm
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
            this.dataGridViewPrices = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPrices)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewPrices
            // 
            this.dataGridViewPrices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPrices.Location = new System.Drawing.Point(146, 81);
            this.dataGridViewPrices.Name = "dataGridViewPrices";
            this.dataGridViewPrices.Size = new System.Drawing.Size(462, 259);
            this.dataGridViewPrices.TabIndex = 0;
            // 
            // SimulatorBlockingCollectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridViewPrices);
            this.Name = "SimulatorBlockingCollectionForm";
            this.Text = "Blocking Collection Sim";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SimulatorBlockingCollectionForm_FormClosing);
            this.Load += new System.EventHandler(this.SimulatorBlockingCollectionForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPrices)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewPrices;
    }
}