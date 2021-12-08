using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace FEPAM.DAL
{
    public class oracleConnectionException
    {
        /// <summary>
        /// BUSCA DE ERRO DE TRATAMENTO DE EXCEÇÃO ORACLE
        /// </summary>
        /// <param name="natanid">Identigicador </param>
        /// <returns>String </returns>
        /// 

        public static string TrataErro(String errorCod)
        {
            try
            {
                if (IsDigitsOnly(errorCod))
                {
                    Int64 anCodigoExcecao = Convert.ToInt64(errorCod);
                    anCodigoExcecao = anCodigoExcecao * (-1);
                    IDAL db;
                    string sqlCommand;
                    db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
                    sqlCommand = "PCK_TRATAMENTO_EXCECOES.SP_TRATAR_MENSAGEM";
                    db.AddInParameter("SEL", "anCodigoExcecao", anCodigoExcecao, DalTypes.Integer);
                    db.AddInParameter("SEL", "mensagem", "", DalTypes.String);
                    DataSet ds = db.ExecuteDataSet(sqlCommand, CommandType.StoredProcedure);
                    String retorno = "";
                    if (ds.Tables[0] != null)
                        retorno = ds.Tables[0].Rows[0]["MENSAGEM"].ToString().Replace("'", "").Replace("\n", "").Replace("\"", "");
                    return retorno;
                }
                else
                    return errorCod;

            }
            catch (Exception ex)
            {
                return ex.Message;

            }
        }
        public static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }
}