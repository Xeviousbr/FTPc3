using System;
using System.Collections.Generic;
using System.Data;
using FEPAM.DAL;

namespace WS_PROCERGS
{
    public class Processo
    {
        public int nroSolicitacao { get; set; }
        public double nroCodAtividade { get; set; }
        public int nroCodIbge { get; set; }
        public int nroCodTipoSolicitacao { get; set; }
        public int nroCodTipoAssunto { get; set; }
        public double? vlrTaxa { get; set; }
        public string dtVencimento { get; set; }
        public string dtPagamento { get; set; }
        public Int64? nroNumeroGA { get; set; }

        public List<Participante> Participantes { get; set; }
        public Empreendimento Empreendimento { get; set; }
        public string txtCodProcesso { get; set; }
        public string dthGeracaoProcesso { get; set; }
        public string txtCpfSolicitante { get; set; }
        public string txtNomeSolicitante { get; set; }
        public bool? indMaisDeUmMunicipio { get; set; }
        public bool? indUcEstadual { get; set; }
        public string dthPrimeiroEnvioCA { get; set; }
        public bool? indSupressaoVegetacaoNativa { get; set; }
        public double? vlrQuitacao { get; set; }
        public bool? indEmpreendimentoAreaRisco { get; set; }
        public string txtCodProcessoAnterior { get; set; }

        // Campos novos 30/09/20 ATC
        public bool? indTemMC { get; set; }
        public Int64? qtdMunicipiosAdicionais { get; set; }
        public List<int> codMunicipiosAdicionais { get; set; }

        public bool? indDetalhamento { get; set; }        

        // public WSORA dadosDetalhamento { get; set; }

        public bool verificaSeExisteSolicitacao()
        {
            Int64 retorno = 0;
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string sqlCommand = " select PCK_NET_WEBSERV01.FC_WEBSERV01_SOLIC(" + nroSolicitacao + ") from dual";
            DataSet ds = db.ExecuteDataSet(sqlCommand, CommandType.Text);
            if (ds.Tables[0].Rows.Count > 0)
                retorno = Convert.ToInt64(ds.Tables[0].Rows[0][0]);
            if (retorno == 0)
                return true;
            return false;
        }


        public void Insert()
        {
            try
            {
// #if !DEBUG
                IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
                string sqlCommand = "PCK_NET_WEBSERV01.SP_WSE1_INS";
                db.AddInParameter("OPR", "P_SEQ_WEBS", nroSolicitacao, DalTypes.Integer);
                db.AddInParameter("OPR", "CodRamoAtividade", nroCodAtividade, DalTypes.Double);
                db.AddInParameter("OPR", "nroCodIbge", nroCodIbge, DalTypes.Integer);
                db.AddInParameter("OPR", "nroCodTipoSolicitacao", nroCodTipoSolicitacao, DalTypes.Integer);
                db.AddInParameter("OPR", "nroCodTipoAssunto", nroCodTipoAssunto, DalTypes.Integer);
                db.AddInParameter("OPR", "vlrTaxa", vlrTaxa, DalTypes.Double);
                db.AddInParameter("OPR", "dtVencimento", dtVencimento, DalTypes.String);
                db.AddInParameter("OPR", "dtPagamento", dtPagamento, DalTypes.String);
                db.AddInParameter("OPR", "nroNumeroGA", nroNumeroGA, DalTypes.Integer);
                db.AddInParameter("OPR", "txtCodProcesso", txtCodProcesso, DalTypes.String);
                db.AddInParameter("OPR", "dthGeracaoProcesso", dthGeracaoProcesso, DalTypes.String);
                db.AddInParameter("OPR", "txtcpfSolicitante", txtCpfSolicitante, DalTypes.String);
                db.AddInParameter("OPR", "txtNomeSolicitante", txtNomeSolicitante, DalTypes.String);
                db.AddInParameter("OPR", "dthPrimeiroEnvioCA", dthPrimeiroEnvioCA, DalTypes.String);
                db.AddInParameter("OPR", "indSupressaoVegetacaoNativa", indSupressaoVegetacaoNativa.HasValue && indSupressaoVegetacaoNativa.Value ? "S" : "N", DalTypes.String);
                db.AddInParameter("OPR", "vlrQuitacao", vlrQuitacao, DalTypes.Double);
                db.AddInParameter("OPR", "INDEMPREENDIMENTOAREARISCO", indEmpreendimentoAreaRisco.HasValue && indEmpreendimentoAreaRisco.Value ? "S" : "N", DalTypes.String);
                db.AddInParameter("OPR", "TxtCodProcessoAnterior", txtCodProcessoAnterior, DalTypes.String);
                db.AddInParameter("OPR", "p_Medida_Compensatoria", indTemMC==true ? 1 : 0, DalTypes.Integer);
                db.ExeuteNonQuery(sqlCommand, CommandType.StoredProcedure);
                // #endif

            }
            catch (Exception ex)
            {
                Logs.WriteLog(ex.Message);
            }
        }

        internal void codMunicipiosAdicionaInsert(int nroSolicitacao, Int64? nroCodEmpto, int codMunicipiosAdicional)
        {

            try
            {
                Logs.WriteLog("Inserção codMunicipiosAdicional : nroSolicitacao = " + nroSolicitacao.ToString() + " nroCodEmpto: " + nroCodEmpto.ToString() + "  " + codMunicipiosAdicional.ToString());
                IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
                string sqlCommand = "PCK_NET_WEBSERV01.SP_WSE4_INS";
                db.AddInParameter("OPR", "P_SEQ_WEBS", nroSolicitacao, DalTypes.Integer);
                db.AddInParameter("OPR", "nroCodEmpto", nroCodEmpto, DalTypes.Integer);
                db.AddInParameter("OPR", "pcodmunicipiosadicionais", codMunicipiosAdicional, DalTypes.Integer);
                db.ExeuteNonQuery(sqlCommand, CommandType.StoredProcedure);
                Logs.WriteLog("Inserido codMunicipiosAdicional");
            }
            catch (Exception ex)
            {
                Logs.WriteLog(ex.Message);
            }
        }

    }
}
