using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using WS_PROCERGS;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //var t = Task.Run(() => ExecuteMain());
                //t.Wait();

                var t = Task.Run(() => ExecutaWs.Executa(true));
                t.Wait();
            }
            catch
            { }
        }

        //static async Task ExecuteMain()
        //{
        //    try
        //    {
        //        string httpUserName = "ERNANIU";
        //        string httpPassword = "fepam@2401";

        //        string usuario = "sisu";
        //        string senha = "mapef10";

        //        string sproxy = "proxy";
        //        string porta = "3128";

        //        string url = "https://sectre.procergs.com.br/sra/public/rest/solicitacao/detalhamentoPorSolicitacao/88296";
        //        string proxyUri = string.Format("{0}:{1}", sproxy, porta);

        //        NetworkCredential proxyCreds = new NetworkCredential(httpUserName, httpPassword);

        //        WebProxy proxy = new WebProxy(proxyUri, false)
        //        {
        //            UseDefaultCredentials = false,
        //            Credentials = proxyCreds,
        //        };

        //        // Now create a client handler which uses that proxy

        //        HttpClient client = null;
        //        HttpClientHandler httpClientHandler = new HttpClientHandler()
        //        {
        //            Proxy = proxy,
        //            PreAuthenticate = true,
        //            UseDefaultCredentials = false,
        //        };

        //        // You only need this part if the server wants a username and password:
        //        httpClientHandler.Credentials = new NetworkCredential(usuario, senha);

        //        client = new HttpClient(httpClientHandler);


        //        HttpResponseMessage response = await client.GetAsync(url);
        //        response.EnsureSuccessStatusCode();
        //        var jsonData = await response.Content.ReadAsStringAsync();


        //        JavaScriptSerializer ser = new JavaScriptSerializer();
        //        //var Wsora = ser.Deserialize<WSORA>(jsonData.Replace("\"", "'"));
        //        var Wsora = ser.Deserialize<WSORA>(jsonData);

        //        if (Wsora != null)
        //        {

        //        }


        //    }
        //    catch(Exception exc)
        //    { }
        //}
    }
}
