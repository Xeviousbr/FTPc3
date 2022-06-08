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
            INI MeuIni = new INI();
            MeuIni.WriteString("Config", "host", txHost.Text);
            MeuIni.WriteString("Config", "user", txUser.Text);
            MeuIni.WriteString("Config", "pass", txSenha.Text);
            MeuIni.WriteString("Config", "CamLocal", txLocal.Text);
            MeuIni.WriteString("Config", "PastaBaseFTP", txCamFTP.Text);
            Close();
        }

        private void btTeste_Click(object sender, EventArgs e)
        {
            btTeste.Enabled = false;            
            FTP cFPT;
            cFPT = new FTP(txHost.Text, txUser.Text, txSenha.Text);
            if (cFPT.Testa())
                MessageBox.Show("Teste realizado com sucesso", "Credenciais válidas");
            else
                MessageBox.Show("Impossível conectar", "Erro na configuração");
            btTeste.Enabled = true;
        }
    }
}
