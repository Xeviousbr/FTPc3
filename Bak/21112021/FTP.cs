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

        public void Aciona()
        {

            string destino = "";
            Stream arquivo = null; ;

            var request = (FtpWebRequest)WebRequest.Create("ftp://localhost/" + destino);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential("anonymous", "contato@andrealveslima.com.br");

            using (var stream = new StreamReader(arquivo))
            {
                var conteudoArquivo = Encoding.UTF8.GetBytes(stream.ReadToEnd());
                request.ContentLength = conteudoArquivo.Length;

                var requestStream = request.GetRequestStream();
                requestStream.Write(conteudoArquivo, 0, conteudoArquivo.Length);
                requestStream.Close();
            }

            var response = (FtpWebResponse)request.GetResponse();
            Console.WriteLine("Upload completo. Status: {0}", response.StatusDescription);
            response.Close();

            //string ftp = "ftp://my.server.ftp";
            //string user = "ftp_user";
            //string pass = "password";
            //string folder = "\\folder\\folder1\\";
            //string target = "C:\\temp\\";

            //NetworkCredential credentials = new NetworkCredential(user, pass);
            //string folderPath = ftp + @"/" + folder + @"/";

            //WebRequest request = WebRequest.Create(folderPath);
            //request.Method = WebRequestMethods.Ftp.UploadFile;
            //request.Credentials = credentials;

            //request.

            //WebResponse response2 = request.GetResponse();

            //StreamReader reader = new StreamReader(response2.GetResponseStream());

            // WebRequestMethods.Ftp.UploadFile();

            //var AllFiles = GetDir(folderPath, credentials);
            //    ??? $files = ($Allfiles -split "`r`n") ???

        }

    //public static Object GetDir(string url, NetworkCredential credentials)
    //{
    //    //WebRequest request = WebRequest.Create(url);
    //    //request.Method = WebRequestMethods.Ftp.ListDirectory;
    //    //request.Credentials = credentials;
    //    //WebResponse response = request.GetResponse();
    //    //StreamReader reader = new StreamReader(response.GetResponseStream());
    //    //reader.ReadToEnd();
    //    //reader.Close();
    //    //response.Close();
    //}
}
}
