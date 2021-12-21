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

        public Tela()
        {
            InitializeComponent();
        }

        private void Tela_Load(object sender, EventArgs e)
        {
            MeuIni = new INI2();
            UltDt = new DateTime(2001, 1, 1);
            Atualiza();
        }

        private bool Atualiza()
        {
            //MeuIni.WriteString("Config", "CamLocal", @"C:\TeleTudo\public_html");
            string camLocal = MeuIni.ReadString("Config", "CamLocal", "");
            UltAtualizado(camLocal);
            string ese = ArqEsc.FullName;
            String PastaBaseFTP = MeuIni.ReadString("Config", "PastaBaseFTP", "");
            int pos = ArqEsc.DirectoryName.IndexOf(PastaBaseFTP)+ PastaBaseFTP.Length;
             string Resto = ArqEsc.FullName.Substring(pos);
            string CamfTP = PastaBaseFTP + Resto;

            //MeuIni.WriteString("Config", "host", "tele-tudo.com");
            //MeuIni.WriteString("Config", "user", "teletu76");
            //MeuIni.WriteString("Config", "pass", "ufrsufrs3753");
            //string host = MeuIni.ReadString("Config", "host", "");
            //string user = MeuIni.ReadString("Config", "user", "");
            //string pass = MeuIni.ReadString("Config", "pass", "");
            CamfTP = ArqEsc.Name;

            //string host = "ftp.dlptest.com";
            //string user = "dlpuser";
            //string pass = "rNrKYTX9g7z3RgJRmxWuGHbeu";

            string host = "www.fepam.rs.gov.br";
            string user = "dmz/fepam";
            string pass = "j5n8m7f8";

            FTP cFPT = new FTP();
            cFPT.Upa(CamfTP, camLocal, host, user, pass);
             
            // cFTP.upload(CamfTP, camLocal, host, user, pass);

            // Testar
            //    String parDir = ArqEsc.DirectoryName.Substring(camLocal).Replace(@"\", @"/"); 

            //    // String camFTP = MeuIni.ReadString("Config", "CamFTP", "");
            //    String DiretFTP = PastaBase + parDir;
            //    Label1.Text = ArqEsc.FullName;
            //    Directory.SetCurrentDirectory(ArqEsc.DirectoryName);
            //    FTP cFPT = new FTP();

            ////FTP URL: ftp.dlptest.com or ftp://ftp.dlptest.com/
            ////FTP User: dlpuser
            ////Password: rNrKYTX9g7z3RgJRmxWuGHbeu

            //// https://dlptest.com/ftp-test/

            //    string host = MeuIni.ReadString("Config", "host", "");
            //    string user = MeuIni.ReadString("Config", "user", "");
            //    string pass = MeuIni.ReadString("Config", "pass", "");

            //    // Resolver
            //    string CaminhoArqApartirDaPastaBase = "";

            //    string remoteFile = PastaBase + CaminhoArqApartirDaPastaBase;
            //    cFPT.upload("remoteFile", "localfile", host, user, pass);
            //    this.Text = ArqEsc.Name + " " + DateTime.Now.ToShortDateString();

            // testar
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
