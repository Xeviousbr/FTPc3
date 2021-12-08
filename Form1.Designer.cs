namespace FTPc
{
    partial class Tela
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.Label1 = new System.Windows.Forms.Label();
            this.ProgressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(2, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(418, 22);
            this.Label1.TabIndex = 0;
            // 
            // ProgressBar1
            // 
            this.ProgressBar1.Location = new System.Drawing.Point(12, 25);
            this.ProgressBar1.Name = "ProgressBar1";
            this.ProgressBar1.Size = new System.Drawing.Size(402, 23);
            this.ProgressBar1.TabIndex = 1;
            this.ProgressBar1.Visible = false;
            // 
            // Tela
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 76);
            this.Controls.Add(this.ProgressBar1);
            this.Controls.Add(this.Label1);
            this.Location = new System.Drawing.Point(12, 9);
            this.Name = "Tela";
            this.Text = "FTPeia";
            this.Load += new System.EventHandler(this.Tela_Load);
            this.Resize += new System.EventHandler(this.Tela_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.ProgressBar ProgressBar1;
    }
}

