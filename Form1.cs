using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FTPc
{
    public partial class Tela : Form
    {
        private int PassoTimer = 0;
        private int Transferencias = 0;
        private string camLocal = "";
        private string PastaBaseFTP = "";
        private string host = "";
        private DateTime UltDt;
        private FileInfo ArqEsc;
        private INI2 MeuIni;
        private FTP cFPT;                

        public Tela()
        {
            InitializeComponent();
        }

        private void Inicializa()
        {
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.PassoTimer = 0;
            string user = MeuIni.ReadString("Config", "user", "");
            string pass = MeuIni.ReadString("Config", "pass", "");
            this.cFPT = new FTP(this.host, user, pass);
            this.camLocal = MeuIni.ReadString("Config", "CamLocal", "");
            this.PastaBaseFTP = MeuIni.ReadString("Config", "PastaBaseFTP", "");
            Console.WriteLine("cFPT.setBarra(ref ProgressBar1)");
            this.cFPT.setBarra(ref ProgressBar1);
        }

        private void Tela_Load(object sender, EventArgs e)
        {
            MeuIni = new INI2();
            UltDt = new DateTime(2001, 1, 1);
            this.host = MeuIni.ReadString("Config", "host", "");
            if (this.host.Length == 0)
            {
                Config FConfig = new Config();
                FConfig.ShowDialog();
                this.host = MeuIni.ReadString("Config", "host", "");
                if (this.host.Length == 0)
                {
                    MessageBox.Show("Não foi configurado", "O programa será fechado");
                    Environment.Exit(0);
                }
            }
            else
                timer1.Enabled = true;
        } 

        #region Obtem Arquivo a atualizar

        private void UltAtualizado(String Pasta)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(Pasta);
            BuscaArquivos(dirInfo);
        }

        private void BuscaArquivos(DirectoryInfo diret)
        {
            DirectoryInfo objDirectoryInfo = new System.IO.DirectoryInfo(diret.FullName);
            SearchFiles(objDirectoryInfo);
            SearchDirectories(objDirectoryInfo);
        }

        private void SearchDirectories(DirectoryInfo objDirectoryInfo)
        {
            //Boolean bolReturn = false;
            foreach (DirectoryInfo DirectorioInfo in objDirectoryInfo.GetDirectories())
            {
                //bolReturn = true;
                if ((DirectorioInfo.Exists == true) && (DirectorioInfo.Name != "System Volume Information") && (DirectorioInfo.Name != "RECYCLER"))
                {
                    SearchFiles(DirectorioInfo);
                    SearchDirectories(DirectorioInfo);
                }
            }
        }

        private void SearchFiles(DirectoryInfo info)
        {
            // É POSSÍVEL MELHORAR A PERFORMANCE, FILTRANDO AS EXTENSÕES JÁ NO CARREGAMENTO DA LISTA DOS ARQUIVOS
            FileInfo[] arquivos = info.GetFiles().OrderByDescending(p => p.CreationTime).ToArray();
            foreach (FileInfo arquivo in arquivos)
            {
                DateTime EssaData = arquivo.LastWriteTime;
                if (EssaData > UltDt)
                {
                    if ((arquivo.Extension == ".php") || (arquivo.Extension == ".js"))
                    {
                        UltDt = EssaData;
                        ArqEsc = arquivo;
                    }
                }
            }
        }

        #endregion

        #region Operações do Usuário
        private bool Atualiza()
        {
            UltAtualizado(this.camLocal);
            this.Text = "Ftpeia : " + ArqEsc.Name;
            Label1.Text = ArqEsc.FullName;
            Console.WriteLine(ArqEsc.FullName);
            string ese = ArqEsc.FullName;
            int pos = ArqEsc.DirectoryName.IndexOf(this.PastaBaseFTP) + this.PastaBaseFTP.Length;
            string Resto = ArqEsc.FullName.Substring(pos);
            string NmArq = ArqEsc.Name;
            int TamNome = NmArq.Length;
            int TamResto = Resto.Length;
            string CamfTP = Resto.Substring(0, TamResto - TamNome);
            if (this.cFPT.Upload(ese, CamfTP))
            {

                // Gambiarra pra mostrar um Progress fake, na primeira vez, não sei pq não aparece o Progress na primeira faz
                if (this.Transferencias==0)
                    pictureBox1.Visible = true;

                this.Transferencias++;
                timer1.Enabled = true;
                Console.WriteLine("Upload realizado");
                ProgressBar1.Value = ProgressBar1.Maximum;
                ProgressBar1.Refresh();                
                return true;
            }
            else
            {
                string Erro = this.cFPT.getErro();
                Console.WriteLine("Erro: " + Erro);
                Console.WriteLine("ProgressBar1.Visible = false");
                ProgressBar1.Visible = false;
                lbErro.Text = Erro;
                lbErro.Visible = true;
                return false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Transferencias==0)
            {
                this.PassoTimer++;
                int Tmp = 6 - this.PassoTimer;
                if (Tmp==0)
                {
                    this.Transferencias = 1;
                    Label1.Text = "";
                    this.ClicouInicio();                    
                } else
                {
                    Label1.Text = Tmp.ToString();
                }
            } else
            {
                this.PassoTimer++;
                switch (PassoTimer)
                {
                    case 1: // Desabilit
                        ProgressBar1.Value = 0;
                        Console.WriteLine("Barra desabilitada");                        
                        break;
                    case 2: // Invisivel
                        ProgressBar1.Visible = false;

                        // Gambiarra pra mostrar um Progress fake, na primeira vez, não sei pq não aparece o Progress na primeira faz
                        pictureBox1.Visible = false;

                        Console.WriteLine("Barra Invisível");
                        break;
                    default:
                        Console.WriteLine("PassoTimer = 0");
                        timer1.Enabled = false;
                        this.WindowState = FormWindowState.Minimized;
                        this.PassoTimer = 0;
                        break;
                }
            }
        }

        private void ClicouInicio()
        {
            btInicio.Visible = false;
            btConfig.Visible = false;
            this.Refresh();
            Inicializa();
            Atualiza();
            if (ArqEsc != null)
                Label1.Text = ArqEsc.FullName;
        }

        private void btInicio_Click(object sender, EventArgs e)
        {
            this.ClicouInicio();
        }

        private void btConfig_Click(object sender, EventArgs e)
        {
            this.Label1.Text = "";
            this.timer1.Enabled = false;
            Config FConfig = new Config();
            FConfig.ShowDialog();
            this.host = MeuIni.ReadString("Config", "host", "");
            if (this.host.Length == 0)
            {
                MessageBox.Show("Não foi configurado", "O programa será fechado");
            }
        }

        private void Tela_Resize_1(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                // Gambiarra pra mostrar um Progress fake, na primeira vez, não sei pq não aparece o Progress na primeira faz
                pictureBox1.Visible = false;

                Label1.Text = "Procurando arquivo a atualizar";
                Label1.Refresh();
                Atualiza();
            }
        }
        #endregion        
        
    }
}
