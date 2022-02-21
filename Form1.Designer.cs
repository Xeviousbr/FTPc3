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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tela));
            this.Label1 = new System.Windows.Forms.Label();
            this.ProgressBar1 = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbErro = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(2, 9);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(418, 22);
            this.Label1.TabIndex = 0;
            // 
            // ProgressBar1
            // 
            this.ProgressBar1.Location = new System.Drawing.Point(12, 34);
            this.ProgressBar1.Name = "ProgressBar1";
            this.ProgressBar1.Size = new System.Drawing.Size(402, 23);
            this.ProgressBar1.TabIndex = 1;
            this.ProgressBar1.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbErro
            // 
            this.lbErro.Location = new System.Drawing.Point(2, 27);
            this.lbErro.Name = "lbErro";
            this.lbErro.Size = new System.Drawing.Size(418, 22);
            this.lbErro.TabIndex = 2;
            this.lbErro.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbErro.Visible = false;
            // 
            // Tela
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 76);
            this.Controls.Add(this.lbErro);
            this.Controls.Add(this.ProgressBar1);
            this.Controls.Add(this.Label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(597, 752);
            this.MaximizeBox = false;
            this.Name = "Tela";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FTPeia";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.Tela_Activated);
            this.Load += new System.EventHandler(this.Tela_Load);
            this.Resize += new System.EventHandler(this.Tela_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.ProgressBar ProgressBar1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lbErro;
    }
}

