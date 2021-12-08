using System;
using System.Collections.Generic;
using System.Xml;

namespace FEPAM.DAL
{

    /// <summary>
    /// Classe de Apoio a Configurações de Serviço
    /// </summary>
    public class Config
    {
        
        private const String ExecutionSchedulePath = @"C:\PROJETOS\WS_PROCERGS\WS_PROCERGS\Xml\ExecutionSchedule.xml";
        
        #region Properties

        //Propriedade Comum Para Todos os Serviços
        //public String Conexao { get; set; }
        public Double Intervalo { get; set; }
        //public String URLIntranet { get; set; }
        //public String WS_Nome { get; set; }
        //public Int64? WS_Id { get; set; }
        //public String WS_Username { get; set; }

        //Propriedade Privativa Para Cada Serviço
        public String Servico { get; set; }
        public String Descricao { get; set; }
        public Boolean Producao { get; set; }
        public String Horario { get; set; }
        public String WS_Email { get; set; }

        //Propriedade Privativa Para Cada Serviço Definindo Seus Destinatarios (Obrigatório)
        public List<Identificadores> ListaResponsaveis { get; set; }
        //Propriedade Privativa Para Cada Serviço Definindo Seus Destinatarios (Opcional)
        public List<Identificadores> ListaDestinatarios { get; set; }
        //Lista com Nomes dos Serviços
        public List<String> ListaServicos { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Recupera Configurações Genéricas dos Serviços
        /// </summary>
        public void GetInitialConfig()
        {
            XmlDocument documento = new XmlDocument();
            XmlNode node = null;
            XmlNodeList nodeServicos = null;
            String NomeServico;
            List<String> NomesdeServicos = new List<String>();

            try
            {
                documento.Load(@ExecutionSchedulePath);

                node = documento.SelectSingleNode("configuracoes");
                nodeServicos = documento.SelectNodes("configuracoes/servicos");

                //Conexao = node.ChildNodes.Item(0).InnerText;
                Intervalo = Convert.ToDouble(node.ChildNodes.Item(1).InnerText) * 60000;
                //URLIntranet = node.ChildNodes.Item(2).InnerText;
                //WS_Nome = node.ChildNodes.Item(3).InnerText;
                //WS_Id = Convert.ToInt64(node.ChildNodes.Item(4).InnerText);
                //WS_Username = node.ChildNodes.Item(5).InnerText;

                foreach (XmlElement element in nodeServicos)
                {
                    for (int i = 0; i < element.ChildNodes.Count; i++)
                    {
                        NomeServico = element.ChildNodes.Item(i).Name;
                        NomesdeServicos.Add(NomeServico);
                    }
                }
                ListaServicos = NomesdeServicos;
            }
            catch
            {
                throw new Exception("Ocorreu um erro ao buscar os dados no arquivo XML de configurações no caminho: " + @ExecutionSchedulePath);
            }
        }

        /// <summary>
        /// Recupera Configurações de um Único Serviço
        /// </summary>
        public void GetServiceConfig(XmlDocument documento, String serviceName)
        {
            XmlNode nodeServico = null;
            XmlNode nodeServicoResponsavel = null;

            try
            {
                Servico = serviceName;

                nodeServico = documento.SelectSingleNode("configuracoes/servicos/" + serviceName);

                Descricao = nodeServico.ChildNodes.Item(0).InnerText;
                Producao = Convert.ToBoolean(nodeServico.ChildNodes.Item(1).InnerText);
                Horario = nodeServico.ChildNodes.Item(2).InnerText;
                WS_Email = nodeServico.ChildNodes.Item(3).InnerText;

                nodeServicoResponsavel = documento.SelectSingleNode("configuracoes/servicos/" + serviceName + "/responsavel");

                //ListaResponsaveis = GetResponsaveis(serviceName);
                //ListaDestinatarios = GetDestinatarios(serviceName);
            }
            catch
            {
                throw new Exception("Ocorreu um erro ao buscar os dados do serviço específico - " + serviceName + " - no arquivo XML de configurações no caminho: " + @ExecutionSchedulePath);
            }
        }

        /// <summary>
        /// Carrega Dados do Serviço a Ser Executado
        /// </summary>
        public void GetServiceToExecute()
        {
            XmlDocument documento = new XmlDocument();
            Int32 hour = 0;
            Int32 maxMinute = 0;
            Int32 minMinute = 0;

            try
            {
                documento.Load(@ExecutionSchedulePath);

                foreach (String servico in ListaServicos)
                {
                    GetServiceConfig(documento, servico);
                    hour = Convert.ToDateTime(Horario).Hour;
                    minMinute = Convert.ToDateTime(Horario).Minute;
                    maxMinute = Convert.ToDateTime(Horario).Minute + Convert.ToInt32((Intervalo / 60000));

                    if (DateTime.Now.Hour == hour &&
                       DateTime.Now.Minute >= minMinute &&
                       DateTime.Now.Minute < maxMinute)
                    {
                        return;
                    }
                }
                Servico = null;
                Descricao = null;
                Producao = false;
                Horario = null;
                WS_Email = null;
                ListaResponsaveis = null;
                ListaDestinatarios = null;
            }
            catch
            {
                throw new Exception("Ocorreu um erro ao buscar o horário de execução do serviço - " + Servico + " - no arquivo XML de configurações no caminho: " + @ExecutionSchedulePath);
            }
        }

        /// <summary>
        /// Busca Node Contido Dentro do XML de configuração
        /// </summary>
        public static XmlNode GetSingleNode(String nodePath)
        {
            XmlDocument documento = new XmlDocument();
            XmlNode singleNode = null;

            try
            {
                documento.Load(@ExecutionSchedulePath);
                singleNode = documento.SelectSingleNode(nodePath);
                return singleNode;
            }
            catch
            {
                throw new Exception("Ocorreu um erro ao buscar o node - " + nodePath + " - no arquivo XML de configurações no caminho: " + @ExecutionSchedulePath);
            }

        }

        /// <summary>
        /// Busca Lista de Nodes Contido Dentro do XML de configuração
        /// </summary>
        public static XmlNodeList GetListNode(String nodePath)
        {
            XmlDocument documento = new XmlDocument();
            XmlNodeList listNode = null;

            try
            {
                documento.Load(@ExecutionSchedulePath);
                listNode = documento.SelectNodes(nodePath);
                return listNode;
            }
            catch
            {
                throw new Exception("Ocorreu um erro ao buscar o node - " + nodePath + " - no arquivo XML de configurações no caminho: " + @ExecutionSchedulePath);
            }
        }

        ///// <summary>
        ///// Recupera os dados do(s) responsável(is)
        ///// </summary>
        //public static List<Identificadores> GetResponsaveis(String serviceName)
        //{
        //    XmlDocument documento = new XmlDocument();
        //    XmlNodeList listNode = null;
        //    List<Identificadores> listaResponsaveis = new List<Identificadores>();

        //    try
        //    {
        //        documento.Load(@ExecutionSchedulePath);
        //        listNode = documento.SelectNodes("configuracoes/servicos/" + serviceName + "/responsaveis/responsavel");

        //        foreach (XmlElement element in listNode)
        //        {
        //            Identificadores responsavel = new Identificadores();
        //            responsavel.Nome = element.ChildNodes.Item(0).InnerText;
        //            responsavel.Email = element.ChildNodes.Item(1).InnerText;
        //            if (!String.IsNullOrEmpty(element.ChildNodes.Item(2).InnerText))
        //                responsavel.Id = Convert.ToInt64(element.ChildNodes.Item(2).InnerText);
        //            else
        //                responsavel.Id = 0;
        //            responsavel.Username = element.ChildNodes.Item(3).InnerText;
        //            listaResponsaveis.Add(responsavel);
        //        }

        //        return listaResponsaveis;
        //    }
        //    catch
        //    {
        //        throw new Exception("Ocorreu um erro ao buscar os destinatarios no arquivo XML de configurações no caminho: " + @ExecutionSchedulePath);
        //    }

        //}

        ///// <summary>
        ///// Recupera os dados do(s) destinatario(s)
        ///// </summary>
        //public static List<Identificadores> GetDestinatarios(String serviceName)
        //{
        //    XmlDocument documento = new XmlDocument();
        //    XmlNodeList listNode = null;
        //    List<Identificadores> listaDestinatarios = new List<Identificadores>();

        //    try
        //    {
        //        documento.Load(@ExecutionSchedulePath);
        //        listNode = documento.SelectNodes("configuracoes/servicos/" + serviceName + "/destinatarios/destinatario");

        //        foreach (XmlElement element in listNode)
        //        {
        //            Identificadores destinatario = new Identificadores();
        //            destinatario.Nome = element.ChildNodes.Item(0).InnerText;
        //            destinatario.Email = element.ChildNodes.Item(1).InnerText;
        //            if (!String.IsNullOrEmpty(element.ChildNodes.Item(2).InnerText))
        //                destinatario.Id = Convert.ToInt64(element.ChildNodes.Item(2).InnerText);
        //            else
        //                destinatario.Id = 0;
        //            destinatario.Username = element.ChildNodes.Item(3).InnerText;
        //            listaDestinatarios.Add(destinatario);
        //        }

        //        return listaDestinatarios;
        //    }
        //    catch
        //    {
        //        throw new Exception("Ocorreu um erro ao buscar os destinatarios no arquivo XML de configurações no caminho: " + @ExecutionSchedulePath);
        //    }

        //}

        #endregion

    }

    /// <summary>
    /// Classe de Apoio a Configurações de Serviço
    /// </summary>
    public class Identificadores
    {

        #region Properties

        public String Nome { get; set; }
        public String Email { get; set; }
        public Int64? Id { get; set; }
        public String Username { get; set; }

        #endregion

    }

}
