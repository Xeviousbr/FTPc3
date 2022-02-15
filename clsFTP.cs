using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTPc
{
    public class FTP
    {
        string ftpIPServidor = "";
        string ftpUsuarioID = "";
        string ftpSenha = "";

        public FTP(string ftpIPServidor, string ftpUsuarioID, string ftpSenha)
        {
            this.ftpIPServidor = ftpIPServidor;
            this.ftpUsuarioID = ftpUsuarioID;
            this.ftpSenha = ftpSenha;
        }


        public void Upload(string _nomeArquivo, string Caminho)
        {
            string Cam = Caminho.Replace(@"\", @"/");
            FileInfo _arquivoInfo = new FileInfo(_nomeArquivo);
            string Suri = "ftp://" + this.ftpIPServidor + Cam + _arquivoInfo.Name;
            FtpWebRequest requisicaoFTP;

            // Cria um objeto FtpWebRequest a partir da Uri fornecida
            requisicaoFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(Suri));
            // requisicaoFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + this.ftpIPServidor + "/" + Cam + "/" + _arquivoInfo.Name));

            // Fornece as credenciais de WebPermission
            requisicaoFTP.Credentials = new NetworkCredential(this.ftpUsuarioID, this.ftpSenha);

            // Por padrão KeepAlive é true, 
            requisicaoFTP.KeepAlive = false;

            // Especifica o comando a ser executado
            requisicaoFTP.Method = WebRequestMethods.Ftp.UploadFile;

            // Especifica o tipo de dados a ser transferido
            requisicaoFTP.UseBinary = true;

            // Notifica o servidor seobre o tamanho do arquivo a enviar
            requisicaoFTP.ContentLength = _arquivoInfo.Length;

            // Define o tamanho do buffer para 2kb
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int _tamanhoConteudo;

            // Abre um stream (System.IO.FileStream) para o arquivo a ser enviado
            FileStream fs = _arquivoInfo.OpenRead();

            try
            {
                // Stream  para o qual o arquivo a ser enviado será escrito
                Stream strm = requisicaoFTP.GetRequestStream();

                // Lê a partir do arquivo stream, 2k por vez
                _tamanhoConteudo = fs.Read(buff, 0, buffLength);

                // ate o conteudo do stream terminar
                while (_tamanhoConteudo != 0)
                {
                    // Escreve o conteudo a partir do arquivo para o stream FTP 
                    strm.Write(buff, 0, _tamanhoConteudo);
                    _tamanhoConteudo = fs.Read(buff, 0, buffLength);
                }

                // Fecha o stream a requisição
                strm.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro de Upload");
            }
        }

        //public void EnviarArquivo()
        //{
        //    FtpWebRequest ftpRequest;
        //    FtpWebResponse ftpResponse;
        //    try
        //    {
        //        //define os requesitos para se conectar com o servidor
        //        ftpRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://31.220.21.96"));
        //        // ftpRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri(@"ftp://www.fepam.rs.gov.br"));
        //        // ftpRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://tele-tudo.com"));
        //        // 
        //        ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
        //        ftpRequest.Proxy = null;
        //        ftpRequest.UseBinary = true;
        //        // ftpRequest.Credentials = new NetworkCredential("dmz/fepam", "j5n8m7f8");
        //        ftpRequest.Credentials = new NetworkCredential("admin_teletudo", "#^7fJx%a9pai8OsF");

        //        //Seleção do arquivo a ser enviado
        //        FileInfo arquivo = new FileInfo(@"C:\Temp\teste.txt");
        //        byte[] fileContents = new byte[arquivo.Length];

        //        using (FileStream fr = arquivo.OpenRead())
        //        {
        //            fr.Read(fileContents, 0, Convert.ToInt32(arquivo.Length));
        //        }

        //        using (Stream writer = ftpRequest.GetRequestStream())
        //        {
        //            writer.Write(fileContents, 0, fileContents.Length);
        //        }

        //        //obtem o FtpWebResponse da operação de upload
        //        ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
        //        MessageBox.Show(ftpResponse.StatusDescription);
        //    }
        //    catch (WebException webex)
        //    {
        //        MessageBox.Show(webex.ToString());
        //    }
        //}

    }
}

            //        internal void Upa(string camfTP, string camLocal, string host, string user, string pass)
            //        {
            //            try
            //            {
            //                /* 
            //                Forma de acesso via FTP:
            //                Acesso Via FTP:
            //                Host:                ediac.correios.com.br
            //                Portas:              21 e o Range (55000 a 65535) 
            //                Pasta:              /e-CARTA

            //Usuário: 557500
            //Senha: Ab1234>=


            //                 */

            //                //string host = "";
            //                //string user = "";
            //                //string pass = "";
            //                int bufferSize = 2048;

            //                //FtpWebRequest ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host);
            //                FtpWebRequest ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + camfTP);

            //                /* Log in to the FTP Server with the User Name and Password Provided */
            //                ftpRequest.Credentials = new NetworkCredential(user, pass);
            //                /* When in doubt, use these options */
            //                ftpRequest.UseBinary = true;
            //                ftpRequest.UsePassive = true;
            //                ftpRequest.KeepAlive = true;
            //                /* Specify the Type of FTP Request */
            //                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
            //                /* Establish Return Communication with the FTP Server */
            //                Stream ftpStream = ftpRequest.GetRequestStream();
            //                /* Open a File Stream to Read the File for Upload */
            //                FileStream localFileStream = new FileStream(camLocal, FileMode.Create);
            //                /* Buffer for the Downloaded Data */
            //                byte[] byteBuffer = new byte[bufferSize];
            //                int bytesSent = localFileStream.Read(byteBuffer, 0, bufferSize);
            //                /* Upload the File by Sending the Buffered Data Until the Transfer is Complete */
            //                try
            //                {
            //                    while (bytesSent != 0)
            //                    {
            //                        ftpStream.Write(byteBuffer, 0, bytesSent);
            //                        bytesSent = localFileStream.Read(byteBuffer, 0, bufferSize);
            //                    }
            //                }
            //                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            //                /* Resource Cleanup */
            //                localFileStream.Close();
            //                ftpStream.Close();
            //                ftpRequest = null;
            //            }
            //            catch (Exception ex) { 
            //                Console.WriteLine(ex.ToString()); 
            //            }
            //            return;
            //        }

            //internal void Upload()
            //{
            //    throw new NotImplementedException();
            //}
        // }
// }
