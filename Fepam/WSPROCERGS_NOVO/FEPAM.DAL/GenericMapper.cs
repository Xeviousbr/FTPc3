/****************************************************************************/
/* FEPAM - Fundação Estadual de Proteção Ambiental                          */
/* Projeto:                                                                 */
/* Author: Nilton Jardim do Nascimento                                      */
/*                                                                          */
/* Date Generated: 23/07/2014                                               */
/*                                                                          */
/****************************************************************************/
using System;
using System.Data;


namespace FEPAM.DAL
{
    public class GenericMapper
    {
        private DataRow drGLobal { get; set; }

        public GenericMapper(DataRow dr)
        {
            drGLobal = dr;
        }

        /// <summary>
        /// Retorna o valor da coluna para a variável correspondente
        /// </summary>
        /// <param name="Prop">Variável a que será atribuida o retorno da função</param>
        /// <param name="coluna">Nome da coluna do DataRow</param>
        /// <returns>Valor Int16 da coluna</returns>
        public Int16? Map(Int16? Prop, String coluna)
        {
            try { Prop = drGLobal[coluna] != DBNull.Value ? Convert.ToInt16(drGLobal[coluna]) : Prop = null; }
            catch { Prop = null; }
            return Prop;
        }

        /// <summary>
        /// Retorna o valor da coluna para a variável correspondente
        /// </summary>
        /// <param name="Prop">Variável a que será atribuida o retorno da função</param>
        /// <param name="coluna">Nome da coluna do DataRow</param>
        /// <returns>Valor Int32 da coluna</returns>
        public Int32? Map(Int32? Prop, String coluna)
        {
            try { Prop = drGLobal[coluna] != DBNull.Value ? Convert.ToInt32(drGLobal[coluna]) : Prop = null; }
            catch { Prop = null; }
            return Prop;
        }

        /// <summary>
        /// Retorna o valor da coluna para a variável correspondente
        /// </summary>
        /// <param name="Prop">Variável a que será atribuida o retorno da função</param>
        /// <param name="coluna">Nome da coluna do DataRow</param>
        /// <returns>Valor Int64 da coluna</returns>
        public Int64? Map(Int64? Prop, String coluna)
        {
            try { return (drGLobal[coluna] != DBNull.Value ? Convert.ToInt64(drGLobal[coluna]) : Prop = null); }
            catch { return null; }
        }

        /// <summary>
        /// Retorna o valor da coluna para a variável correspondente
        /// </summary>
        /// <param name="Prop">Variável a que será atribuida o retorno da função</param>
        /// <param name="coluna">Nome da coluna do DataRow</param>
        /// <param name="seNulo">Valor de retorno se for nulo</param>
        /// <returns>Valor Int16 da coluna</returns>
        public Int16? Map(Int16? Prop, String coluna, Int16? seNulo)
        {
            try { return (drGLobal[coluna] != DBNull.Value ? Convert.ToInt16(drGLobal[coluna]) : Prop = seNulo); }
            catch { return seNulo; }
        }

        /// <summary>
        /// Retorna o valor da coluna para a variável correspondente
        /// </summary>
        /// <param name="Prop">Variável a que será atribuida o retorno da função</param>
        /// <param name="coluna">Nome da coluna do DataRow</param>
        /// <param name="seNulo">Valor de retorno se for nulo</param>
        /// <returns>Valor Int32 da coluna</returns>
        public Int32? Map(Int32? Prop, String coluna, Int32? seNulo)
        {
            try { return (drGLobal[coluna] != DBNull.Value ? Convert.ToInt32(drGLobal[coluna]) : Prop = seNulo); }
            catch { return seNulo; }
        }

        /// <summary>
        /// Retorna o valor da coluna para a variável correspondente
        /// </summary>
        /// <param name="Prop">Variável a que será atribuida o retorno da função</param>
        /// <param name="coluna">Nome da coluna do DataRow</param>
        /// <param name="seNulo">Valor de retorno se for nulo</param>
        /// <returns>Valor Int64 da coluna</returns>
        public Int64? Map(Int64? Prop, String coluna, Int64? seNulo)
        {
            try { return (drGLobal[coluna] != DBNull.Value ? Convert.ToInt64(drGLobal[coluna]) : Prop = seNulo); }
            catch { return seNulo; }
        }

        /// <summary>
        /// Retorna o valor da coluna para a variável correspondente
        /// </summary>
        /// <param name="Prop">Variável a que será atribuida o retorno da função</param>
        /// <param name="coluna">Nome da coluna do DataRow</param>
        /// <param name="seNulo">Valor de retorno se for nulo</param>
        /// <returns>Valor char da coluna</returns>
        public char Map(char Prop, string coluna, char seNulo)
        {
            try { return drGLobal[coluna] != DBNull.Value ? Convert.ToChar(drGLobal[coluna]) : Prop = seNulo; }
            catch { return seNulo; }
        }

        /// <summary>
        /// Retorna o valor da coluna para a variável correspondente
        /// </summary>
        /// <param name="Prop">Variável a que será atribuida o retorno da função</param>
        /// <param name="coluna">Nome da coluna do DataRow</param>
        /// <returns>Valor String da coluna</returns>
        public String Map(String Prop, String coluna)
        {
            try { return drGLobal[coluna] != DBNull.Value ? Convert.ToString(drGLobal[coluna]) : Prop = null; }
            catch { return null; }
        }

        /// <summary>
        /// Retorna o valor da coluna para a variável correspondente
        /// </summary>
        /// <param name="Prop">Variável a que será atribuida o retorno da função</param>
        /// <param name="coluna">Nome da coluna do DataRow</param>
        /// <param name="seNulo">Valor de retorno se for nulo</param>
        /// <returns>Valor String da coluna</returns>
        public String Map(String Prop, String coluna, String seNulo)
        {
            try { return drGLobal[coluna] != DBNull.Value ? Convert.ToString(drGLobal[coluna]) : Prop = seNulo; }
            catch { return seNulo; }
        }

        /// <summary>
        /// Retorna uma string do campo data no formato informado
        /// </summary>
        /// <param name="Prop">Variável a que será atribuida o retorno da função</param>
        /// <param name="coluna">Nome da coluna do DataRow</param>
        /// <param name="seNulo">Valor de retorno se for nulo</param>
        /// <param name="formatoData">Formato de retorno da data ex:dd/MM/yyyy</param>
        /// <returns>Objeto DataTable</returns>
        public String Map(String Prop, String coluna, String seNulo, String formatoData)
        {
            try
            {
                String retorno = drGLobal[coluna] != DBNull.Value ? Convert.ToString(drGLobal[coluna]) : Prop = seNulo;
                return Convert.ToDateTime(retorno).ToString(formatoData);
            }
            catch { return seNulo; }
        }

        public Decimal? Map(Decimal? Prop, String coluna)
        {
            try { return drGLobal[coluna] != DBNull.Value ? Convert.ToDecimal(drGLobal[coluna]) : Prop = null; }
            catch { return null; }
        }

        public Double? Map(Double? Prop, String coluna)
        {
            try { return drGLobal[coluna] != DBNull.Value ? Convert.ToDouble(drGLobal[coluna]) : Prop = null; }
            catch { return null; }
        }

        public Boolean? Map(Boolean? Prop, String coluna)
        {
            try { return drGLobal[coluna] != DBNull.Value ? Convert.ToBoolean(drGLobal[coluna]) : Prop = null; }
            catch { return null; }
        }
    }
}
