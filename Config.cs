using System;
using System.Windows.Forms;

namespace FTPc
{
    public partial class Config : Form
    {
        private INI MeuIni;

        public Config()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.MeuIni.WriteString("Config", "host", txHost.Text);
            this.MeuIni.WriteString("Config", "user", txUser.Text);
            this.MeuIni.WriteString("Config", "pass", txSenha.Text);
            this.MeuIni.WriteString("Config", "CamLocal", txLocal.Text);
            this.MeuIni.WriteString("Config", "PastaBaseFTP", txCamFTP.Text);
            this.MeuIni.WriteString("Config", "Porta", txPorta.Text);            
            Close();
        }

        private void btTeste_Click(object sender, EventArgs e)
        {
            btTeste.Enabled = false;            
            FTP cFPT;
            int Porta = Convert.ToInt32(txPorta.Text);
            cFPT = new FTP(txHost.Text, txUser.Text, txSenha.Text, Porta);
            if (cFPT.Testa())
                MessageBox.Show("Teste realizado com sucesso", "Credenciais válidas");
            else
                MessageBox.Show("Impossível conectar", "Erro na configuração");
            btTeste.Enabled = true;
        }

        private void Config_Load(object sender, EventArgs e)
        {
            this.MeuIni = new INI();
            txHost.Text = this.MeuIni.ReadString("Config", "host", "");
            txUser.Text = this.MeuIni.ReadString("Config", "user","");
            txSenha.Text = this.MeuIni.ReadString("Config", "pass","");
            txLocal.Text = this.MeuIni.ReadString("Config", "CamLocal","");
            txCamFTP.Text = this.MeuIni.ReadString("Config", "PastaBaseFTP", "");
            txPorta.Text = this.MeuIni.ReadString("Config", "Porta", "20");
        }
    }
}
