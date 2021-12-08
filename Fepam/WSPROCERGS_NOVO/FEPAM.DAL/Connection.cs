/****************************************************************************/
/* FEPAM - Fundação Estadual de Proteção Ambiental                          */
/* Projeto: Framework de Desenvolvimento                                    */
/* Author: Leonardo Tremper                                                 */
/*                                                                          */
/*                                                                          */
/* Date Generated: 10/10/2007                                               */
/*                                                                          */
/* Microsoft.Practices                                                      */
/****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace FEPAM.DAL
{
    /// <summary>
    /// Classe para acesso a string de conexão
    /// </summary>
    /// <remarks>
    /// Esta classe permite o acesso a string de conexão de 
    /// maneira encapsulada e segura
    /// </remarks>
    public class Connection
    {
        /// <summary>
        /// Método para retorno da string de conexão
        /// </summary>
        /// <remarks>
        /// Este método retorna a string de conexão com o banco de dados
        /// para utilização com a classe DataAcessLayer.
        /// </remarks>
        /// <returns>String com a conexão com o banco de dados</returns>
        public static string DbConnection()
        {
            //return "User Id=fepam;Password=x4tur2p5;Data Source=homologa;Pooling=false;";
            //return "User Id=fepam;Password=x4tur2p5;Data Source=fprod;Pooling=false;";
            try
            {
#if DEBUG
            return "User Id=fepam; Password=x4tur2p5; Data Source=(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = SEMARSPAEAS01.SEMARS.INTRA.RS.GOV.BR)(PORT = 1521))) (CONNECT_DATA = (SID = hml11g) (SERVER = DEDICATED)));Pooling=false;";
#else
            return System.Configuration.ConfigurationSettings.AppSettings["appConexao"];
#endif
            }
            catch
            {
                return "User Id=fepam;Password=x4tur2p5;Data Source=fprod;Pooling=false;";
            }
        }
    }
}