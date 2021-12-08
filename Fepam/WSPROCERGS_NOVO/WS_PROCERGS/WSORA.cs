using FEPAM.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS_PROCERGS
{
    
    // Primeiro nível, capa, repositório das informações que etão em sub-classes
    [DataObject]
    [Serializable]
    public class WSORA
    {
        public int SOLICITACAO { get; set; }
        public int WS_ORA_nro_funcionarios { get; set; }
        public float WS_ORA_area_terreno { get; set; }
        public float WS_ORA_area_construida { get; set; }

        public bool? WS_ORA_alteracao_ampliacao { get; set; }
        public List<WS_ORA_combustivel> WS_ORA_combustivel { get; set; }
        public List<WS_ORA_tancagem> WS_ORA_tancagem { get; set; }
        public List<WS_ORA_cap_prod> WS_ORA_cap_prod { get; set; }
        public List<WSORA_equipamento> WS_ORA_equipamento { get; set; }
        public List<wsora_processo_etapa> WS_ORA_processo_etapa { get; set; }
       

        internal void Insert()
        {

#region Dsdos individuais
            if (WS_ORA_nro_funcionarios > 0)
                WSE5("WS_ORA_nro_funcionarios", WS_ORA_nro_funcionarios, null);
            if (WS_ORA_area_terreno > 0)
                WSE5("WS_ORA_area_terreno", WS_ORA_area_terreno, null);
            if (WS_ORA_area_construida > 0)
                WSE5("WS_ORA_area_construida", WS_ORA_area_construida, null);
            if(WS_ORA_alteracao_ampliacao != null && WS_ORA_alteracao_ampliacao.HasValue)
            {
                WSE5("WS_ORA_alteracao_ampliacao", null, WS_ORA_alteracao_ampliacao.Value ? "S" : "N" );
            }
#endregion

#region Dsdos dinâmicos
            if (WS_ORA_combustivel != null)
                foreach (var item in WS_ORA_combustivel)
                    WS10(item.id, item.WS_ORA_consumo_dia_comb);
            if (WS_ORA_tancagem != null)
                foreach (var item in WS_ORA_tancagem) 
                    WS11(item.WS_ORA_nome_tanque, item.WS_ORA_substancia, item.WS_ORA_vol_tanque, item.WS_ORA_unid_medida.id, item.WS_ORA_estado_materia, item.WS_ORA_contencao, item.WS_ORA_local_tancagem);                        
            if (WS_ORA_cap_prod != null)
                foreach (var item in WS_ORA_cap_prod)
                    WSE6(item.id, item.WS_ORA_qtd_max_mes, item.WS_ORA_unid_medida.id);
            if (WS_ORA_equipamento != null)
                foreach (var item in WS_ORA_equipamento)
                {
                    string cap_nominal = item.WS_ORA_cap_nominal.ToString().Replace(',', '.');
                    int? numberIDUM = null;
                    if(item.WS_ORA_unid_medida != null)
                    {
                        numberIDUM = item.WS_ORA_unid_medida.id;
                    }                    
                    WSE7(item.id, item.WS_ORA_qtd_equipamento, cap_nominal, numberIDUM);
                }
            if (WS_ORA_processo_etapa != null)
                foreach (var item1 in WS_ORA_processo_etapa)
                {         
                    
                    WSE8(item1.ordemItem, item1.txtItem);
                    if (item1.itensSublista != null)
                        foreach (var item2 in item1.itensSublista)
                            WSE9(item1.ordemItem , item2.ordemSubItem,item2.txtSubItem);
                }
#endregion

            //private string ConvValor(float VlrOrig) {

            //}

        }

        //private void WSE5(string p1, float WS_ORA_area_terreno, double p2)
        //{
        //    throw new NotImplementedException();
        //}

        //private void WSE5(string Origem, double Valor, double ValorDescritivo)
        //{

        //}

        #region LAC Gravação
 
        /// <summary>
        /// Dados individuais
        /// </summary>
        private void WSE5(string Origem, float? Valor, string ValorDescritivo)
        {
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string sqlCommand = "PCK_NET_WEBSERV01.SP_WSE5_INS";
            db.AddInParameter("OPR", "p_solicitacao", SOLICITACAO, DalTypes.Integer);
            db.AddInParameter("OPR", "p_origem", Origem, DalTypes.String);
            db.AddInParameter("OPR", "p_vlr_numerico", Valor, DalTypes.Double);
            db.AddInParameter("OPR", "p_vlr_descritivo", ValorDescritivo, DalTypes.String);
            db.ExeuteNonQuery(sqlCommand, CommandType.StoredProcedure);
        }

        /// <summary>
        /// WS_ORA_cap_prod
        /// </summary>
        private void WSE6(int Wsora_mate_id, float Wsora_qtd_max_mes, int Wsora_unme_id)
        {
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string sqlCommand = "PCK_NET_WEBSERV01.SP_WSE6_INS";
            db.AddInParameter("OPR", "P_SEQ_WEBS", SOLICITACAO, DalTypes.Integer);
            db.AddInParameter("OPR", "Wsora_mate_id", Wsora_mate_id, DalTypes.Integer);
            db.AddInParameter("OPR", "Wsora_qtd_max_mes", Wsora_qtd_max_mes, DalTypes.Double);
            db.AddInParameter("OPR", "Wsora_unme_id", Wsora_unme_id, DalTypes.Integer);
            db.ExeuteNonQuery(sqlCommand, CommandType.StoredProcedure);
        }

        /// <summary>
        /// WSORA_equipamento
        /// </summary>
        private void WSE7(int Wsora_tieq_id, int Wsora_qtd_equipamento, string Wsora_cap_nominal, int? Wsora_unme_id)
        {
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string sqlCommand = "PCK_NET_WEBSERV01.SP_WSE7_INS";
            db.AddInParameter("OPR", "P_SEQ_WEBS", SOLICITACAO, DalTypes.Integer);
            db.AddInParameter("OPR", "Wsora_tieq_id", Wsora_tieq_id, DalTypes.Integer);
            db.AddInParameter("OPR", "Wsora_qtd_equipamento", Wsora_qtd_equipamento, DalTypes.Integer);
            db.AddInParameter("OPR", "Wsora_cap_nominal", Wsora_cap_nominal, DalTypes.String);

            db.AddInParameter("OPR", "Wsora_unme_id", Wsora_unme_id, DalTypes.Integer);

            db.ExeuteNonQuery(sqlCommand, CommandType.StoredProcedure);
        }

        /// <summary>
        /// "wsora_processo_etapa"
        /// </summary>
        private void WSE8(int ordemItem, string txtItem)
        {

            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string sqlCommand = "PCK_NET_WEBSERV01.SP_WSE8_INS";
            db.AddInParameter("OPR", "P_SEQ_WEBS", SOLICITACAO, DalTypes.Integer);
            db.AddInParameter("OPR", "ws_ora_ordemItem", ordemItem, DalTypes.Integer);
            db.AddInParameter("OPR", "Ws_ora_txtItem", txtItem, DalTypes.String);
            db.ExeuteNonQuery(sqlCommand, CommandType.StoredProcedure);

            //// Gambiarra porque a Proc acima não foi feita da maneira que possa já pegar a informação, ATC
            //string sqlCommandSelect = "select Max(WSE8_ID) from Fep_aux_procergs_prpr";
            //DataSet ds = db.ExecuteDataSet(sqlCommandSelect, CommandType.Text);
            //Int64 WSE8_ID = Convert.ToInt64(ds.Tables[0].Rows[0][0].ToString());
            //return WSE8_ID;
        }

        /// <summary>
        /// itensSublista
        /// </summary>
        private void WSE9(Int64 ordemItem, int ordemSubItem, string txtsubitem)
        {
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string sqlCommand = "PCK_NET_WEBSERV01.SP_WSE9_INS";
            db.AddInParameter("OPR", "p_solicitacao", SOLICITACAO, DalTypes.Integer);
            db.AddInParameter("OPR", "ws_ora_ordemItem", ordemItem, DalTypes.Integer);
            db.AddInParameter("OPR", "ws_ora_ordemSubItem", ordemSubItem, DalTypes.Integer);
            db.AddInParameter("OPR", "Wsora_txtsubitem", txtsubitem, DalTypes.String);
            db.ExeuteNonQuery(sqlCommand, CommandType.StoredProcedure);
        }

        /// <summary>
        /// WS_ORA_combustivel
        /// </summary>
        private void WS10(int comb_id, float consumo_dia)
        {
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string sqlCommand = "PCK_NET_WEBSERV01.SP_WSE10_INS";
            db.AddInParameter("OPR", "p_solicitacao", SOLICITACAO, DalTypes.Integer);
            db.AddInParameter("OPR", "ws_ora_comb_id", comb_id, DalTypes.Integer);
            db.AddInParameter("OPR", "ws_ora_consumo_dia_comb", consumo_dia, DalTypes.Double);
            db.ExeuteNonQuery(sqlCommand, CommandType.StoredProcedure);
        }

        /// <summary>
        /// WS_ORA_tancagem
        /// </summary>
        private void WS11(string nome_tanque, string substancia, float vol_tanque, int unme_id, string estado_materia, bool contencao, string local)
        {
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string sqlCommand = "PCK_NET_WEBSERV01.SP_WSE11_INS";
            db.AddInParameter("OPR", "p_solicitacao", SOLICITACAO, DalTypes.Integer);
            db.AddInParameter("OPR", "ws_ora_nome_tanque", nome_tanque, DalTypes.String);
            db.AddInParameter("OPR", "ws_ora_substancia", substancia, DalTypes.String);
            db.AddInParameter("OPR", "ws_ora_vol_tanque", vol_tanque, DalTypes.Double);
            db.AddInParameter("OPR", "ws_ora_unme_id", unme_id, DalTypes.Integer);
            db.AddInParameter("OPR", "ws_ora_estado_materia", estado_materia, DalTypes.String);
            db.AddInParameter("OPR", "ws_ora_contencao", (contencao ? "S" : "N"), DalTypes.String);
            db.AddInParameter("OPR", "ws_ora_local_tancagem", local, DalTypes.String);
            db.ExeuteNonQuery(sqlCommand, CommandType.StoredProcedure);
        }

        #endregion

        #region Verifica Solicitacao
        public bool verificaExisteSolicitacao()
        {
            Int64 id_ret = 0;
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string sqlCommand = " select * from Fep_aux_procergs_ind wse5 WHERE SOLICITACAO = '" + this.SOLICITACAO.ToString() + "'";
            DataSet ds = db.ExecuteDataSet(sqlCommand, CommandType.Text);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                id_ret = Convert.ToInt64(ds.Tables[0].Rows[0][0].ToString());
            if (id_ret > 0)
                return true;
            return false;
        }
        #endregion

    }

    #region SubClasses

    [DataObject]
    [Serializable]
    public class WS_ORA_combustivel
    {
        public string tabela { get; set; }
        public int id { get; set; }
        public string descricao { get; set; }
        public float WS_ORA_consumo_dia_comb { get; set; }
        public UnidadeMedida WSORA_unid_medida { get; set; }
    }

    [DataObject]
    [Serializable]
    public class WS_ORA_tancagem
    {
        public string WS_ORA_nome_tanque { get; set; }
        public string WS_ORA_substancia { get; set; }
        public float WS_ORA_vol_tanque { get; set; }
        public UnidadeMedida WS_ORA_unid_medida { get; set; }
        public string WS_ORA_estado_materia { get; set; }
        public bool WS_ORA_contencao { get; set; }
        public string WS_ORA_local_tancagem { get; set; }
    }

    [DataObject]
    [Serializable]
    public class WS_ORA_cap_prod
    {
        public string tabela { get; set; }
        public int id { get; set; }
        public string descricao { get; set; }
        public float WS_ORA_qtd_max_mes { get; set; }
        public UnidadeMedida WS_ORA_unid_medida { get; set; }
    }

    [DataObject]
    [Serializable]
    public class WSORA_equipamento
    {
        public string tabela { get; set; }
        public int id { get; set; }
        public string descricao { get; set; }
        public int WS_ORA_qtd_equipamento { get; set; }
        public string WS_ORA_cap_nominal { get; set; }

        public UnidadeMedida WS_ORA_unid_medida { get; set; }
    }

    [DataObject]
    [Serializable]
    public class wsora_processo_etapa
    {
        public int ordemItem { get; set; }
        public string txtItem { get; set; }
        public List<itensSublista> itensSublista { get; set; }
    }

    [DataObject]
    [Serializable]
    public class itensSublista
    {
        public int ordemSubItem { get; set; }
        public string txtSubItem { get; set; }
    }

    [DataObject]
    [Serializable]
    public class UnidadeMedida
    {
        public string tabela { get; set; }
        public int id { get; set; }
        public string descricao { get; set; }
    }

    #endregion

}
