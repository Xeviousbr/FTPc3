using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml;
using FEPAM.DAL;


namespace WS_PROCERGS
{
    public class ExecutaWs
    {

        private static string sproxy = "";
        private static string porta = "";
        private static string httpUserName = "";
        private static string httpPassword = "";
        private static string usuario = "";
        private static string senha = "";

        //Com XML
        public static async Task Executa(Boolean Agora)
        {
            try
            {
                string DataHoje = "";
                string URLWebServiceProcergs = "";

                DataHoje = DateTime.Now.ToString("yyyy-MM-dd");
                //DataHoje = "2021-09-30";

                URLWebServiceProcergs = Config.GetSingleNode("configuracoes/URLWebServiceProcergs").InnerText.ToString();

                httpUserName = Config.GetSingleNode("configuracoes/httpUserName").InnerText.ToString();
                httpPassword = Config.GetSingleNode("configuracoes/httpPassword").InnerText.ToString();

                usuario = Config.GetSingleNode("configuracoes/usuarioNome").InnerText.ToString();
                senha = Config.GetSingleNode("configuracoes/usuarioSenha").InnerText.ToString();

                sproxy = Config.GetSingleNode("configuracoes/proxy").InnerText.ToString();
                porta = Config.GetSingleNode("configuracoes/porta").InnerText.ToString();



                Config config = new Config();
                config.GetInitialConfig();

                //Verifica qual serviço está na hora de executar
                config.GetServiceToExecute();
                bool ExecutarAgora = true;


                if (ExecutarAgora)
                {
                    string proxyUri = string.Format("{0}:{1}", sproxy, porta);

                    NetworkCredential proxyCreds = new NetworkCredential(httpUserName, httpPassword);

                    WebProxy proxy = new WebProxy(proxyUri, false)
                    {
                        UseDefaultCredentials = false,
                        Credentials = proxyCreds,
                    };

                    // Now create a client handler which uses that proxy

                    HttpClient client = null;
                    HttpClientHandler httpClientHandler = new HttpClientHandler()
                    {
                        Proxy = proxy,
                        PreAuthenticate = true,
                        UseDefaultCredentials = false,
                    };

                    // You only need this part if the server wants a username and password:
                    httpClientHandler.Credentials = new NetworkCredential(usuario, senha);

                    client = new HttpClient(httpClientHandler);
                    client.Timeout = TimeSpan.FromSeconds(20);




                    Logs.WriteLog("Serviço consulta->" + URLWebServiceProcergs + DataHoje);
                    HttpResponseMessage response = await client.GetAsync(URLWebServiceProcergs + DataHoje);
                    //HttpResponseMessage response = await client.GetAsync("https://sectre.procergs.com.br/sra/public/rest/solicitacao/detalhamentoPorSolicitacao/88293");
                    response.EnsureSuccessStatusCode();
                    var jsonData = await response.Content.ReadAsStringAsync();

                    JavaScriptSerializer ser = new JavaScriptSerializer();
                    //if (jsonData != null)
                    //{
                    //    var vWSORA = ser.Deserialize<WSORA>(jsonData);
                    //    if (vWSORA != null)
                    //    {
                    //    }
                    //}
                    //ExecutaWs.InsertProcessos(listProcesso);
                    if (jsonData != null)
                    {
                        Logs.WriteLog("Serviço JSON:" + jsonData);

                        var listProcesso = ser.Deserialize<List<Processo>>(jsonData);
                        if(listProcesso != null && listProcesso.Count > 0)
                        {
                            InsertProcessos(listProcesso);
                        }
                        
                        
                    }
                    else
                    {
                        Logs.WriteLog("Serviço JSON invalido");
                    }

                    
                }

            }
            catch (Exception ex)
            {
                Logs.WriteLog("ERRO no serviço \n MENSAGEM DE ERRO:" + ex.ToString());
                if (ex.InnerException != null)
                {
                    Logs.WriteLog("ERRO no serviço \n MENSAGEM DE ERRO:" + ex.InnerException.Message);
                }
            }
        }

     
        public static void InsertProcessos(List<Processo> listProcesso)
        {
            try
            {
                foreach (Processo processo in listProcesso)
                {
                    if (!processo.verificaSeExisteSolicitacao())
                    {
                        processo.Insert();
                        foreach (Participante participante in processo.Participantes)
                        {
                            participante.nroSolicitacao = processo.nroSolicitacao;
                            participante.Insert();
                        }
                        processo.Empreendimento.indMaisDeUmMunicipio = processo.indMaisDeUmMunicipio.HasValue && processo.indMaisDeUmMunicipio.Value ? processo.indMaisDeUmMunicipio.Value : false;
                        processo.Empreendimento.nroSolicitacao = processo.nroSolicitacao;
                        processo.Empreendimento.Insert();

                        if (processo.codMunicipiosAdicionais != null)
                        {
                            Logs.WriteLog("codMunicipiosAdicionais da solicitação " + processo.nroSolicitacao.ToString() + " = " + processo.codMunicipiosAdicionais.Count.ToString());
                            for (int i = 0; i < processo.codMunicipiosAdicionais.Count; i++)
                                processo.codMunicipiosAdicionaInsert(processo.nroSolicitacao, processo.Empreendimento.nroCodEmpreendimento, processo.codMunicipiosAdicionais[i]);
                        }
                        else
                            Logs.WriteLog("codMunicipiosAdicionais da solicitação " + processo.nroSolicitacao.ToString() + " é vazio");
                        if (processo.indDetalhamento == true)
                        {                         
                            Task t = Task.Run(() => ExecutaLAC.Executa(processo.nroSolicitacao, sproxy, porta, httpUserName, httpPassword, usuario, senha));
                            t.Wait();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logs.WriteLog("ERRO no serviço \n MENSAGEM DE ERRO:" + ex.InnerException.Message);
            }
        }

    }

}
