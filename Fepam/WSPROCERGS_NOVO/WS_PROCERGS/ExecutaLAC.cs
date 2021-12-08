using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FEPAM.DAL;
using System.Net;
using System.Net.Http;
using System.Web.Script.Serialization;

namespace WS_PROCERGS
{
    public class ExecutaLAC
    {
        
        //string senha = "";

        //Com XML
        public static async Task Executa(Int64 Solicitacoes, string sproxy, string porta, string httpUserName, string httpPassword, string usuario, string senha)
        {

            //URLWebServiceProcergs = "https://sectre.procergs.com.br/sra/public/rest/solicitacao/detalhamentoPorSolicitacao/88295";
            //URLWebServiceProcergs = "https://sectre.procergs.com.br/sra/public/rest/solicitacao/detalhamentoPorSolicitacao/88297";

            string URLWebServiceProcergs = Config.GetSingleNode("configuracoes/URLWebServiceJuntadaProcergs").InnerText.ToString() + Solicitacoes.ToString();


            Logs.WriteLog("Serviço consulta->" + URLWebServiceProcergs);
            try
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

                HttpResponseMessage response = await client.GetAsync(URLWebServiceProcergs);
                response.EnsureSuccessStatusCode();
                var jsonData = await response.Content.ReadAsStringAsync();

                JavaScriptSerializer ser = new JavaScriptSerializer();
                if (jsonData != null && jsonData.Length > 0)
                {
                    Logs.WriteLog("Serviço JSON:" + jsonData);
                    var vWSORA = ser.Deserialize<WSORA>(jsonData);
                    if (vWSORA != null)
                    {
                        validarTamanho(vWSORA);
                        InsertLac(vWSORA);
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

        private static void validarTamanho(WSORA vW)
        {
            if (vW.WS_ORA_tancagem != null)
            {
                foreach (WS_ORA_tancagem tancagem in vW.WS_ORA_tancagem)
                {
                    if (tancagem.WS_ORA_nome_tanque != null && tancagem.WS_ORA_nome_tanque.Length > 15)
                    {
                        tancagem.WS_ORA_nome_tanque = tancagem.WS_ORA_nome_tanque.Substring(0, 15);
                    }
                    if (tancagem.WS_ORA_substancia != null && tancagem.WS_ORA_substancia.Length > 30)
                    {
                        tancagem.WS_ORA_substancia = tancagem.WS_ORA_substancia.Substring(0, 30);
                    }
                    if (tancagem.WS_ORA_estado_materia != null && tancagem.WS_ORA_estado_materia.Length > 25)
                    {
                        tancagem.WS_ORA_estado_materia = tancagem.WS_ORA_estado_materia.Substring(0, 25);
                    }
                    if (tancagem.WS_ORA_local_tancagem != null && tancagem.WS_ORA_local_tancagem.Length > 25)
                    {
                        tancagem.WS_ORA_local_tancagem = tancagem.WS_ORA_local_tancagem.Substring(0, 25);
                    }
                }
            }

            if (vW.WS_ORA_processo_etapa != null)
            {
                foreach (wsora_processo_etapa etapa in vW.WS_ORA_processo_etapa)
                {
                    if (etapa.txtItem != null && etapa.txtItem.Length > 80)
                    {
                        etapa.txtItem = etapa.txtItem.Substring(0, 80);
                    }
                    foreach (itensSublista itS in etapa.itensSublista)
                    {
                        if (itS.txtSubItem != null && itS.txtSubItem.Length > 80)
                        {
                            itS.txtSubItem = itS.txtSubItem.Substring(0, 80);
                        }
                    }
                }
            }
        }

        public static void InsertLac(WSORA wSora)
        {
            try
            {
                //if (!wSora.verificaExisteSolicitacao())
                {
                    wSora.Insert();
                }
                Logs.WriteLog("Serviço executado com sucesso!");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    Logs.WriteLog("ERRO no serviço \n MENSAGEM DE ERRO:" + ex.InnerException.Message);
                }
                else
                {
                    Logs.WriteLog("ERRO no serviço \n MENSAGEM DE ERRO:" + ex.Message);
                }
            }

        }


    }

}
