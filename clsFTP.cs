using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FTPc
{
    public class FTP
    {
        internal void Upa(string camfTP, string camLocal, string host, string user, string pass)
        {
            try
            {
                /* 
                Forma de acesso via FTP:
                Acesso Via FTP:
                Host:                ediac.correios.com.br
                Portas:              21 e o Range (55000 a 65535) 
                Pasta:              /e-CARTA

Usuário: 557500
Senha: Ab1234>=


                 */

                //string host = "";
                //string user = "";
                //string pass = "";
                int bufferSize = 2048;

                //FtpWebRequest ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host);
                FtpWebRequest ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + camfTP);

                /* Log in to the FTP Server with the User Name and Password Provided */
                ftpRequest.Credentials = new NetworkCredential(user, pass);
                /* When in doubt, use these options */
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = true;
                ftpRequest.KeepAlive = true;
                /* Specify the Type of FTP Request */
                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                /* Establish Return Communication with the FTP Server */
                Stream ftpStream = ftpRequest.GetRequestStream();
                /* Open a File Stream to Read the File for Upload */
                FileStream localFileStream = new FileStream(camLocal, FileMode.Create);
                /* Buffer for the Downloaded Data */
                byte[] byteBuffer = new byte[bufferSize];
                int bytesSent = localFileStream.Read(byteBuffer, 0, bufferSize);
                /* Upload the File by Sending the Buffered Data Until the Transfer is Complete */
                try
                {
                    while (bytesSent != 0)
                    {
                        ftpStream.Write(byteBuffer, 0, bytesSent);
                        bytesSent = localFileStream.Read(byteBuffer, 0, bufferSize);
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                /* Resource Cleanup */
                localFileStream.Close();
                ftpStream.Close();
                ftpRequest = null;
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.ToString()); 
            }
            return;
        }

        //internal void Upload()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
