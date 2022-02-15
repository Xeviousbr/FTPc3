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

        public Tela()
        {
            InitializeComponent();
        }

        private void Tela_Load(object sender, EventArgs e)
        {
            MeuIni = new INI2();
            UltDt = new DateTime(2001, 1, 1);

            string host = MeuIni.ReadString("Config", "host", "");
            string user = MeuIni.ReadString("Config", "user", "");
            string pass = MeuIni.ReadString("Config", "pass", "");

            this.cFPT = new FTP(host, user, pass);

            this.camLocal = MeuIni.ReadString("Config", "CamLocal", "");
            this.PastaBaseFTP = MeuIni.ReadString("Config", "PastaBaseFTP", "");

            Atualiza();
        }

        private bool Atualiza()
        {
            UltAtualizado(this.camLocal);
            string ese = ArqEsc.FullName;
            int pos = ArqEsc.DirectoryName.IndexOf(this.PastaBaseFTP)+ this.PastaBaseFTP.Length;
            string Resto = ArqEsc.FullName.Substring(pos);
            string CamfTP = this.PastaBaseFTP + Resto;
            // CamfTP = ArqEsc.Name;

            CamfTP = @"\public_html\resources\views\layouts\";

            this.cFPT.Upload(ese, CamfTP);
            return true;
        }

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
                if ((DirectorioInfo.Exists == true) && (DirectorioInfo.Name != "System Volume Information") && (DirectorioInfo.Name != "RECYCLER") )
                {
                    SearchFiles(DirectorioInfo);
                    SearchDirectories(DirectorioInfo);
                }
            }
        }

        private void SearchFiles (DirectoryInfo info)
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

        private void Tela_Resize(object sender, EventArgs e)
        {
            if (!AlterandoTela)
            {
                AlterandoTela = false;
                //if (!Conectado)
                //{
                //    Conectar();
                //}
                if (Atualiza())
                {
                    this.WindowState = FormWindowState.Minimized;
                    ProgressBar1.Value = 0;
                    AlterandoTela = false;
                }

            }
        //    If AlterandoTela = False Then
        //        AlterandoTela = True
        //    If Conectado = False Then
        //        Conectar()
        //    End If
        //    If(Atualiza() = True) Then
        //        Me.WindowState = FormWindowState.Minimized
        //    End If
        //    ProgressBar1.Value = 0
        //    AlterandoTela = False
        //End If
        }
    }
}
