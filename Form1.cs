using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FTPc
{
    public partial class Tela : Form
    {

        private Boolean AlterandoTela = true;
        private Boolean Ativou = false;
        private FileInfo ArqEsc;
        private DateTime UltDt;
        private INI2 MeuIni;
        private Boolean Conectado = false;
        private long BytesTT = 0;
        private FTP cFPT;
        private string camLocal = "";
        private string PastaBaseFTP = "";
        private int PassoTimer = 0;

        public Tela()
        {
            InitializeComponent();
        }

        private void Tela_Load(object sender, EventArgs e)
        {

        }

        private void Inicializa()
        {
            MeuIni = new INI2();
            UltDt = new DateTime(2001, 1, 1);
            string host = MeuIni.ReadString("Config", "host", "");
            string user = MeuIni.ReadString("Config", "user", "");
            string pass = MeuIni.ReadString("Config", "pass", "");
            this.cFPT = new FTP(host, user, pass);
            this.camLocal = MeuIni.ReadString("Config", "CamLocal", "");
            this.PastaBaseFTP = MeuIni.ReadString("Config", "PastaBaseFTP", "");
            this.cFPT.setBarra(ref ProgressBar1);
        }

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
                timer1.Enabled = true;
                Console.WriteLine("Upload realizado");
                return true;
            } else
            {
                string Erro = this.cFPT.getErro();
                Console.WriteLine("Erro: "+ Erro);
                ProgressBar1.Visible = false;                
                lbErro.Text = Erro;
                lbErro.Visible = true;
                return false;
            }
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

        private void Tela_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                Label1.Text = "Procurando arquivo a atualizar";
                Label1.Refresh();
                Atualiza();
            }
                
            /* if (!AlterandoTela)
                {
                    AlterandoTela = false;                    
                } */
        }

        private void Tela_Activated(object sender, EventArgs e)
        {
            if (this.Ativou==false)
            {
                Inicializa();
                Atualiza();
                if (ArqEsc != null)
                    Label1.Text = ArqEsc.FullName;
                this.Ativou = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            PassoTimer++;
            switch (PassoTimer)
            {
                case 1: // Desabilit
                    ProgressBar1.Value = 0;
                    Console.WriteLine("Barra desabilitada");
                    break;
                case 2: // Invisivel
                    ProgressBar1.Visible = false;                    
                    Console.WriteLine("Barra Invisível");
                    break;
                default:
                    timer1.Enabled = false;
                    this.WindowState = FormWindowState.Minimized;
                    PassoTimer = 0;
                    break;
            }
        }
    }
}
