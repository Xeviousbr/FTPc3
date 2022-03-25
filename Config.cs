﻿using System;
using System.Windows.Forms;

namespace FTPc
{
    public partial class Config : Form
    {
        public Config()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            INI2 MeuIni = new INI2();
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
            if (cFPT.Testa()==false)
            {
                MessageBox.Show("Impossível conectar", "Erro na configuração");
            }

            btTeste.Enabled = true;
        }
    }
}
