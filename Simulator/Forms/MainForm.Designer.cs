namespace Simulator
{
    partial class MainForm
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
            this.btnBasicThread = new System.Windows.Forms.Button();
            this.btnConcDictionary = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBasicThread
            // 
            this.btnBasicThread.Location = new System.Drawing.Point(12, 12);
            this.btnBasicThread.Name = "btnBasicThread";
            this.btnBasicThread.Size = new System.Drawing.Size(154, 23);
            this.btnBasicThread.TabIndex = 0;
            this.btnBasicThread.Text = "Basic Thread Sim";
            this.btnBasicThread.UseVisualStyleBackColor = true;
            this.btnBasicThread.Click += new System.EventHandler(this.btnBasicThread_Click);
            // 
            // btnConcDictionary
            // 
            this.btnConcDictionary.Location = new System.Drawing.Point(12, 41);
            this.btnConcDictionary.Name = "btnConcDictionary";
            this.btnConcDictionary.Size = new System.Drawing.Size(154, 23);
            this.btnConcDictionary.TabIndex = 1;
            this.btnConcDictionary.Text = "Concurrent Dictionary Sim";
            this.btnConcDictionary.UseVisualStyleBackColor = true;
            this.btnConcDictionary.Click += new System.EventHandler(this.btnConcDictionary_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 79);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(154, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Blocking Collection Sim";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnBlockForm);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnConcDictionary);
            this.Controls.Add(this.btnBasicThread);
            this.Name = "MainForm";
            this.Text = "Main Form";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBasicThread;
        private System.Windows.Forms.Button btnConcDictionary;
        private System.Windows.Forms.Button button1;
    }
}

