using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;

namespace FEPAM.DAL
{
    public class Functions
    {

        /// <summary>
        /// BUSCAR O PORTE DO EMPREENDIMENTO
        /// </summary>
        /// <param name="raat_id">Ramo Atividade</param>
        /// <param name="medida">medidas</param>
        /// <param name="licenciavel">?</param>
        /// <returns>String com P, M ou G</returns>
        public static string BuscaPorte(Decimal raat_id, Decimal medida, string licenciavel)
        {
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string raatst = raat_id.ToString().Replace(",", ".");
            string medidast = medida.ToString().Replace(",", ".");
            string sqlCommand = "select BUSCA_PORTE(" + raatst + "," + medidast + ",'" + licenciavel + "') from dual";
            //Console.WriteLine(sqlCommand);
            DataSet ds = db.ExecuteDataSet(sqlCommand, CommandType.Text);
            if (ds == null || ds.Tables[0].Rows.Count == 0) return null;
            string ret = ds.Tables[0].Rows[0][0].ToString();
            return ret;
        }

        /// <summary>
        /// Retorna verdadeiro se é para gerar LU.
        /// </summary>
        /// <param name="raat_id">Ramod e atividade</param>
        /// <param name="porte">Porte</param>
        /// <returns>True para sim, false para não</returns>
        public static bool ImpactoLocal(Decimal raat_id)
        {
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string raatst = raat_id.ToString().Replace(",", ".");
            string sqlCommand = "select pck_processos.fc_porte_lu(" + raatst + ") from dual";
            DataSet ds = db.ExecuteDataSet(sqlCommand, CommandType.Text);
            if (ds == null || ds.Tables[0].Rows.Count == 0) return false;
            string ret = ds.Tables[0].Rows[0][0].ToString();
            if (ret == "S")
            {
                return true;
            }
            return false;
        }

        public static Decimal? CalculaValorBoleto(Double raat_id, Int32 tido_id, Decimal medida, string pronaf, Int64 tprt_id, Int64 num_anos)
        {
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string raatst = raat_id.ToString().Replace(",", ".");
            string medidast = medida.ToString().Replace(",", ".");
            string sqlCommand = "SELECT pck_arr_bloquetos.fc_arr_valor_documen(" + raatst + "," + tido_id + "," + medida + ",'" + pronaf + "'," + tprt_id + "," + num_anos + ", null, null) FROM dual";
            //Console.WriteLine(sqlCommand);
            DataSet ds = db.ExecuteDataSet(sqlCommand, CommandType.Text);
            if (ds == null || ds.Tables[0].Rows.Count == 0) return null;
            string ret = ds.Tables[0].Rows[0][0].ToString();
            return Convert.ToDecimal(ret);
        }

        public static Decimal? CalculaValorBoletoHistorico(Int64 procid)
        {
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            // string raatst = raat_id.ToString().Replace(",", ".");
            // string medidast = medida.ToString().Replace(",", ".");
            string sqlCommand = "SELECT pck_arr_bloquetos.fc_arr_vlr_hist_proc(" + procid.ToString() + ") FROM dual";
            //Console.WriteLine(sqlCommand);
            DataSet ds = db.ExecuteDataSet(sqlCommand, CommandType.Text);
            if (ds == null || ds.Tables[0].Rows.Count == 0) return null;
            string ret = ds.Tables[0].Rows[0][0].ToString();
            return Convert.ToDecimal(ret);
        }

        public static Decimal? CalculaValorBoleto(Double raat_id, Int32 tido_id, Decimal medida, string pronaf, Int64 tprt_id, Int64 num_anos, Int64 procid)
        {
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string raatst = raat_id.ToString().Replace(",", ".");
            string medidast = medida.ToString().Replace(",", ".");
            string sqlCommand = "SELECT pck_arr_bloquetos.fc_arr_valor_documen(" + raatst + "," + tido_id + "," + medida + ",'" + pronaf + "'," + tprt_id + "," + num_anos + ", null, " + procid.ToString() + ") FROM dual";
            //Console.WriteLine(sqlCommand);
            DataSet ds = db.ExecuteDataSet(sqlCommand, CommandType.Text);
            if (ds == null || ds.Tables[0].Rows.Count == 0) return null;
            string ret = ds.Tables[0].Rows[0][0].ToString();
            return Convert.ToDecimal(ret);
        }

        public static int? ParcelasMax(Decimal valor)
        {
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string val = valor.ToString().Replace(",", ".");
            string sqlCommand = "SELECT PCK_ARR_BLOQUETOS.FC_ARR_PARCELAS_MAX(" + val + ") NRO_PARC FROM DUAL";
            DataSet ds = db.ExecuteDataSet(sqlCommand, CommandType.Text);
            if (ds == null || ds.Tables[0].Rows.Count == 0) return null;
            string ret = ds.Tables[0].Rows[0][0].ToString();
            return Convert.ToInt32(ret);
        }

        public static Decimal? CalculaValorParcela(Decimal valor, Int32 parcelas, Int32 parcela, Int32 desc)
        {
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string val = valor.ToString().Replace(",", ".");
            string sqlCommand = "SELECT PCK_ARR_BLOQUETOS.FC_ARR_VALOR_PARCELA(" + val + "," + parcelas + "," + parcela + "," + desc + ") VAL_PARC FROM DUAL";
            DataSet ds = db.ExecuteDataSet(sqlCommand, CommandType.Text);
            if (ds == null || ds.Tables[0].Rows.Count == 0) return null;
            string ret = ds.Tables[0].Rows[0][0].ToString();
            return Convert.ToDecimal(ret);
        }

        public static Int64 NumeroBloqueto()
        {
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string sqlCommand = "SELECT PCK_IRRIG_BLOQUETOS.FC_BLOQUETO_DV() NUM_BLOQ FROM DUAL";
            DataSet ds = db.ExecuteDataSet(sqlCommand, CommandType.Text);
            if (ds == null || ds.Tables[0].Rows.Count == 0) return 0;
            string ret = ds.Tables[0].Rows[0][0].ToString();
            return Convert.ToInt64(ret);
        }

        public static void CriaProcesso(Int64 hicb_id)
        {
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string sqlCommand = "PCK_NET_PROCESSOS.SP_GERA_PROC_SEAMB_DOCS";
            db.AddInParameter("OPR", "nHicb_id", hicb_id, DalTypes.Integer);
            db.ExeuteNonQuery(sqlCommand, CommandType.StoredProcedure);
        }

        public static void AlteraSenhaBanco(string usuario, string senha)
        {
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string sqlCommand = "alter user " + usuario + " identified by " + senha;
            db.ExeuteNonQuery(sqlCommand, CommandType.Text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numero"></param>
        /// <param name="origem"></param>
        /// <param name="ano"></param>
        /// <param name="dv"></param>
        /// <returns></returns>
        public static Boolean ProcessoValido(String numero, String origem, String ano, String dv)
        {
            numero = numero.PadLeft(7, '0');
            origem = origem.PadLeft(4, '0');
            ano = ano.PadLeft(2, '0');
            if (Convert.ToInt32(ano) >= 50 && Convert.ToInt32(ano) <= 87)
            {
                return false;
            }
            Int64? ndv = 0;
            String numint = numero + origem + ano;
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string sqlCommand = "SELECT rotinas_gerais.fc_valida_dig_proc(" + numint + ") FROM DUAL";
            DataSet ds = db.ExecuteDataSet(sqlCommand, CommandType.Text);
            if (ds == null || ds.Tables[0].Rows.Count == 0) return false;
            ndv = Convert.ToInt64(ds.Tables[0].Rows[0][0].ToString());
            return (ndv == Convert.ToInt64(dv));
        }


        public static void AtualizaValoresViagem(Int64 viag_id)
        {
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string sqlCommand = "PCK_NET_ADMINISTRACAO.SP_CALC_VALOR_DIARIA";
            db.AddInParameter("OPR", "viag_id", viag_id, DalTypes.Integer);
            DataSet ds = db.ExecuteDataSet(sqlCommand, CommandType.StoredProcedure);
            if (ds == null || ds.Tables[0].Rows.Count == 0) return;
            string nr_diarias = ds.Tables[0].Rows[0]["NRDIARIAS"].ToString().Replace(".", "").Replace(",", ".");
            string nr_ressarc = ds.Tables[0].Rows[0]["NRRESSARC"].ToString().Replace(".", "").Replace(",", ".");
            string valor_diaria_ser = ds.Tables[0].Rows[0]["VLRDIATOT_S"].ToString().Replace(".", "").Replace(",", ".");
            string valor_diaria_dir = ds.Tables[0].Rows[0]["VLRDIATOT_D"].ToString().Replace(".", "").Replace(",", ".");

            db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            sqlCommand = "UPDATE fep_viagens viag SET viag.nr_diarias = {0} ,viag.nr_ressarc = {1}, viag.valor_diaria_ser = {2}, viag.valor_diaria_dir = {3} WHERE viag.viag_id = {4}";
            sqlCommand = String.Format(sqlCommand, nr_diarias, nr_ressarc, valor_diaria_ser, valor_diaria_dir, viag_id);
            db.ExeuteNonQuery(sqlCommand, CommandType.Text);
        }

        /// <summary>
        /// Retorna o nome do mês por escrito
        /// </summary>
        /// <param name="mes">Número do mês</param>
        /// <returns>String com o nome do mês</returns>
        public static String NomeMes(Int32 mes)
        {
            switch (mes)
            {
                case 1:
                    return "JANEIRO";
                case 2:
                    return "FEVEREIRO";
                case 3:
                    return "MARÇO";
                case 4:
                    return "ABRIL";
                case 5:
                    return "MAIO";
                case 6:
                    return "JUNHO";
                case 7:
                    return "JULHO";
                case 8:
                    return "AGOSTO";
                case 9:
                    return "SETEMBRO";
                case 10:
                    return "OUTUBRO";
                case 11:
                    return "NOVEMBRO";
                case 12:
                    return "DEZEMBRO";
            }
            return "";
        }

        public static string ExclMicro(Int64? polq_id, Int64? para_id)
        {
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string sqlCommand = "pck_monitoramento.pr_monit_excl_micro";
            db.AddInParameter("OPR", "p_polq_id", polq_id, DalTypes.Integer);
            db.AddInParameter("OPR", "p_para_id", para_id, DalTypes.Integer);
            DataSet ds = db.ExecuteDataSet(sqlCommand, CommandType.StoredProcedure);
            if (ds == null || ds.Tables[0].Rows.Count == 0) return "";
            return ds.Tables[0].Rows[0][0].ToString();
        }


        public static string InclMicro(Int64? gmqu_id, Int64? polq_id, Int64? para_id, Int64? atan_id, String usuario)
        {
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string sqlCommand = "pck_monitoramento.pr_monit_incl_micro";
            db.AddInParameter("OPR", "nGmqu_id", gmqu_id, DalTypes.Integer);
            db.AddInParameter("OPR", "nPolq_id", polq_id, DalTypes.Integer);
            db.AddInParameter("OPR", "nPara_id", para_id, DalTypes.Integer);
            db.AddInParameter("OPR", "nAtan_id", atan_id, DalTypes.Integer);
            db.AddInParameter("OPR", "sUsuario", usuario, DalTypes.String);
            DataSet ds = db.ExecuteDataSet(sqlCommand, CommandType.StoredProcedure);
            if (ds == null || ds.Tables[0].Rows.Count == 0) return "";
            return ds.Tables[0].Rows[0][0].ToString();
        }


        /// <summary>
        /// Verifica se uma role existe
        /// </summary>
        /// <param name="role">Role a ser verificada</param>
        /// <returns>true se existe, false se não existe</returns>
        public static Boolean existeRole(String role)
        {
            int nroles = 0;
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string sqlCommand = "SELECT count(*) as total FROM dba_role_privs ropr WHERE ropr.granted_role = '" + role + "'  AND ropr.grantee = 'FEPAM'";
            DataSet ds = db.ExecuteDataSet(sqlCommand, CommandType.Text);
            if (ds == null || ds.Tables[0].Rows.Count == 0) return false;
            nroles = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            return (nroles > 0);
        }

        /// <summary>
        /// Retorna quem deve assinar a viagem
        /// </summary>
        /// <param name="viag_id"></param>
        /// <returns></returns>
        public static Int64 RetornaAssinanteViagem(Int64 viag_id)
        {
            Int64 pess_id = 0;
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string sqlCommand = "SELECT to_number(quemassina_2(viag.viag_id)) as pess_id FROM fep_viagens viag WHERE viag.viag_id = " + viag_id;
            DataSet ds = db.ExecuteDataSet(sqlCommand, CommandType.Text);
            if (ds == null || ds.Tables[0].Rows.Count == 0) return 0;
            pess_id = Convert.ToInt64(ds.Tables[0].Rows[0][0].ToString());
            return pess_id;
        }

        public static string RetDirImgSINPLI(Int64 pess_id)
        {
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string sqlCommand = "SELECT pck_net_sinpli.fnc_le_imagem_dir(" + pess_id.ToString() + ") as diretorio from dual";
            DataSet ds = db.ExecuteDataSet(sqlCommand, CommandType.Text);
            if (ds == null || ds.Tables[0].Rows.Count == 0) return "";
            return ds.Tables[0].Rows[0][0].ToString();
        }

        /// <summary>
        /// Faz as verificações do agendamento de viagem
        /// </summary>
        /// <param name="viag_id">Id da viagem</param>
        /// <param name="veic_id">Id do veículo</param>
        /// <param name="pess_id">Id do motorista</param>
        /// <returns></returns>
        public static string ValidaAgendaViagem(Int64 viag_id, Int64? veic_id, Int64? pess_id)
        {
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string sqlCommand = "SELECT PCK_NET_ADMINISTRACAO.FC_VALIDA_AGENDA_VIAGEM(" + viag_id + "," + veic_id + "," + pess_id + ") AS RETORNO FROM DUAL";
            DataSet ds = db.ExecuteDataSet(sqlCommand, CommandType.Text);
            if (ds == null || ds.Tables[0].Rows.Count == 0) return "";
            return ds.Tables[0].Rows[0][0].ToString();
        }

        /// <summary>
        /// Verifica se o usuário faz parte do DIS
        /// </summary>
        /// <param name="viag_id">Id do Usuário</param>>
        /// <returns></returns>
        public static string VerificaUsuaDis(String usua_id)
        {
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string sqlCommand = "SELECT PCK_NET_ENVIO_MAILS.FNC_VALIDA_DIS(" + usua_id + ") AS RETORNO FROM DUAL";
            DataSet ds = db.ExecuteDataSet(sqlCommand, CommandType.Text);
            if (ds == null || ds.Tables[0].Rows.Count == 0) return "";
            return ds.Tables[0].Rows[0][0].ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viag_id"></param>
        /// <returns></returns>
        public static Int64 RetModalidadeViagem(Int64 viag_id)
        {
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string sqlCommand = "SELECT MODALIDADE FROM FEP_VIAGENS WHERE VIAG_ID=" + viag_id;
            DataSet ds = db.ExecuteDataSet(sqlCommand, CommandType.Text);
            if (ds == null || ds.Tables[0].Rows.Count == 0) return 0;
            return Convert.ToInt64(ds.Tables[0].Rows[0][0]);
        }


        public static string PathDeacordo(string id)
        {
            var db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);

            string sqlCommand = string.Format(@" SELECT docu.docu_id_origina
                                                FROM fep_documentos docu
                                               WHERE docu.docu_id = {0}",id);
          
            DataSet ds = db.ExecuteDataSet(sqlCommand, CommandType.Text);
            if (ds == null || ds.Tables[0].Rows.Count == 0) return null;
            string _id = ds.Tables[0].Rows[0][0].ToString();

            sqlCommand = string.Format(@"select  SUBSTR(pck_proc_assinat.fc_path_doc_deacordo(1,{0}),1,150)  from dual", _id);//fc_path_parecer_transp

            ds = db.ExecuteDataSet(sqlCommand, CommandType.Text);
            if (ds == null || ds.Tables[0].Rows.Count == 0) return null;
            string path = ds.Tables[0].Rows[0][0].ToString();


            return path;
        }
        /// <summary>
        /// Retorna a data do processo do boleto
        /// </summary>
        /// <param name="proc_id"></param>
        /// <returns></returns>
        public static String DataProcBoleto(Int64? proc_id)
        {
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string sqlCommand = "SELECT pck_arr_bloquetos.fc_arr_dt_hist_proc(" + proc_id.ToString() + ") FROM dual";
            //Console.WriteLine(sqlCommand);
            DataSet ds = db.ExecuteDataSet(sqlCommand, CommandType.Text);
            if (ds == null || ds.Tables[0].Rows.Count == 0) return null;
            string ret = ds.Tables[0].Rows[0][0].ToString();
            ret = Convert.ToDateTime(ret).ToString("dd/MM/yyyy");
            return ret;
        }



        /// <summary>
        /// Função que atualiza a situação do processo para analizando para que
        /// o tempo de tramitação seja incrementado.
        /// </summary>
        /// <param name="proc_id">Id do processo</param>
        public static void AtualSitProcAnalizando(Int64 proc_id)
        {
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string sqlCommand = "UPDATE FEP_PROCESSOS PRO SET PRO.SITUACAO = 'L', PRO.DATA_ATUALIZACAO = SYSDATE, PRO.OPERADOR_ATUALIZACAO = '" + HttpContext.Current.Session["USERNAME"].ToString() + "' WHERE PRO.SITUACAO = 'I' AND PRO.PROC_ID = " + proc_id.ToString();
            db.ExeuteNonQuery(sqlCommand, CommandType.Text);
        }

        public static void StatusCancelarCobranca(Int64? hicb_id)
        {
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);

            string sqlCommand = "UPDATE FEP_HISTORICOS_COBRANCAS COB SET COB.STATUS = 6, COB.DATA_ATUALIZACAO = SYSDATE, COB.OPERADOR_ATUALIZACAO = (substr('" + HttpContext.Current.Session["USUA_NOME"] + "', 1, 15) || '-canc solic') WHERE COB.HICB_ID = " + hicb_id.ToString();
            db.ExeuteNonQuery(sqlCommand, CommandType.Text);

            sqlCommand = "update fep_historicos_pagamentos set status = 8, data_atualizacao = sysdate, operador_atualizacao = (substr('" + HttpContext.Current.Session["USUA_NOME"] + "', 1, 15) || '-canc solic') where status != 5 and hicb_id = " + hicb_id.ToString();
            db.ExeuteNonQuery(sqlCommand, CommandType.Text);

            sqlCommand = "update fep_cobrancas_parcelas cobp set cobp.status = 'C', cobp.data_atualizacao = sysdate, cobp.operador_atualizacao = (substr('" + HttpContext.Current.Session["USUA_NOME"] + "', 1, 15) || '-canc solic') where cobp.cobr_id in (select cobr_id from fep_cobrancas where hicb_id = " + hicb_id.ToString() + " )";
            db.ExeuteNonQuery(sqlCommand, CommandType.Text);
        }



        public static String RetornaCaminhoAssinar(Int64 docu_id)
        {
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string sqlCommand = "SELECT pck_proc_assinat.FC_PATH_ASSINAT_DOCU(" + docu_id.ToString() + ") from dual";
            DataSet ds = db.ExecuteDataSet(sqlCommand, CommandType.Text);
            if (ds == null || ds.Tables[0].Rows.Count == 0) return "";
            string ret = ds.Tables[0].Rows[0][0].ToString();
            return ret;
        }


        public static Int32 RetornaSetorAssinatura(Int64 proc_id, Int64? docu_id)
        {
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string sqlCommand = "SELECT pck_proc_assinat.FC_SETO_ASSINAT_PROC(" + proc_id.ToString() + "," + docu_id.ToString() + ") from dual";
            DataSet ds = db.ExecuteDataSet(sqlCommand, CommandType.Text);
            if (ds == null || ds.Tables[0].Rows.Count == 0) return 0;
            Int32 ret = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            return ret;
        }

        public static Boolean TemLicencaVigor(Int64? atan_id)
        {
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string sqlCommand = "SELECT DISTINCT DOCU.DOCU_ID        FROM fep_documentos             docu,             fep_tipos_documentos       tido,             fep_sub_classes_documentos sucd,             fep_processos_documentos   prdc,             fep_processos              proc,             fep_empreend_processos     epro,             fep_responsabilidades      resp       WHERE proc.proc_id = prdc.proc_id         AND docu.docu_id = prdc.docu_id         AND docu.tido_id = tido.tido_id         AND tido.sucd_id = sucd.sucd_id         AND docu.proc_id = epro.proc_id         AND epro.resp_id = resp.resp_id         AND resp.atan_id = " + atan_id.ToString() + "          AND (docu.situacao = 'E')         AND (docu.tido_id = 120 or docu.tido_id = 151)         AND (sucd.cldo_id BETWEEN 1 AND 6)      union all      SELECT DOCU.DOCU_ID TOTAL        FROM fep_documentos             docu,             fep_tipos_documentos       tido,             fep_sub_classes_documentos sucd,             fep_processos              proc,             fep_responsabilidades      resp       WHERE proc.proc_id = docu.proc_id         AND docu.tido_id = tido.tido_id         AND tido.sucd_id = sucd.sucd_id         AND proc.resp_id = resp.resp_id         AND resp.atan_id = " + atan_id.ToString() + "          AND (docu.situacao = 'E')         AND (docu.tido_id = 120 or docu.tido_id = 151)         AND (sucd.cldo_id BETWEEN 1 AND 6)         and not exists (select null                from fep_processos_documentos prdc               where prdc.proc_id = proc.proc_id                 and prdc.docu_id = docu.docu_id)";
            DataSet ds = db.ExecuteDataSet(sqlCommand, CommandType.Text);
            if (ds == null || ds.Tables[0].Rows.Count == 0) return false;
            Int32 ret = ds.Tables[0].Rows.Count;
            return (ret > 0);
        }

        /// <summary>
        /// Atualiza o id do responsável pelo processo
        /// </summary>
        /// <param name="proc_id">Id do processo</param>
        /// <param name="resp_id">Id do responsável</param>
        public static void AtualizaRespProcesso(Int64 proc_id, Int64 resp_id)
        {
            if (resp_id > 0 && proc_id > 0)
            {
                IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
                string sqlCommand;
                if (HttpContext.Current.Session["SETO_ID"] != null)
                    sqlCommand = "UPDATE FEP_PROCESSOS PRO SET PRO.RESP_ID = " + resp_id.ToString() + " WHERE PRO.SETO_ID in (2000, 2225, " + HttpContext.Current.Session["SETO_ID"].ToString() + ") AND PRO.RESP_ID IS NULL AND PRO.PROC_ID = " + proc_id.ToString();
                else
                    sqlCommand = "UPDATE FEP_PROCESSOS PRO SET PRO.RESP_ID = " + resp_id.ToString() + " WHERE PRO.SETO_ID in (2000, 2225, 2052) AND PRO.RESP_ID IS NULL AND PRO.PROC_ID = " + proc_id.ToString();
                db.ExeuteNonQuery(sqlCommand, CommandType.Text);
            }
        }


        /// <summary>
        /// Função que atualiza o status do processo para aguardando complemento
        /// para que o tempo de tramitação não seja incrementado.
        /// </summary>
        /// <param name="proc_id">Id do processo</param>
        public static void AtualSitProcAguardandoComplemento(Int64 proc_id)
        {
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string sqlCommand = "UPDATE FEP_PROCESSOS PRO SET PRO.SITUACAO = 'I', PRO.DATA_ATUALIZACAO = SYSDATE, PRO.OPERADOR_ATUALIZACAO = '" + HttpContext.Current.Session["USERNAME"].ToString() + "' WHERE PRO.SITUACAO = 'L' AND PRO.PROC_ID = " + proc_id.ToString();
            db.ExeuteNonQuery(sqlCommand, CommandType.Text);
        }




        public static string MsgParametro(Int64 atan_id, Int64 para_id, string data_medida)
        {
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string sqlCommand = "SELECT PCK_SISAUTO.FC_VALIDA_PARA_LABO(" + atan_id + "," + para_id + ", TO_DATE('" + data_medida + "','DD/MM/YYYY')) PARAM FROM DUAL";
            DataSet ds = db.ExecuteDataSet(sqlCommand, CommandType.Text);
            if (ds == null || ds.Tables[0].Rows.Count == 0) return "";
            string ret = ds.Tables[0].Rows[0][0].ToString();
            return ret;
        }

        public static Boolean NaoPodeEditar(String poll_id)
        {
            return NaoPodeEditar(Convert.ToInt64(poll_id));
        }

        public static Boolean NaoPodeEditar(Int64? poll_id)
        {
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string sqlCommand = "select poll.poll_id, poll.data_desativacao from fep_pontos_lanctos_liquidos poll where  poll.Poll_Id = " + poll_id + " and poll.data_desativacao is null";
            DataSet ds = db.ExecuteDataSet(sqlCommand, CommandType.Text);
            if (ds == null || ds.Tables[0].Rows.Count == 0) return true;
            return false;
        }

        public static Boolean NaoPodeEditar(String poll_id, String plef_id)
        {
            IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
            string sqlCommand = "";
            if (!(String.IsNullOrEmpty(plef_id)))
            {
                sqlCommand = "select poll.poll_id, poll.data_desativacao from fep_pontos_lanctos_liquidos poll, fep_planilhas_efluentes plef where  poll.Poll_Id = " + poll_id + " and plef.plef_id = " + plef_id;
                sqlCommand += " and (poll.data_desativacao > plef.data_fim or poll.data_desativacao is null)";
            }
            else
            {
                sqlCommand = "select poll.poll_id, poll.data_desativacao from fep_pontos_lanctos_liquidos poll where  poll.Poll_Id = " + poll_id + " and poll.data_desativacao is null";
            }
            DataSet ds = db.ExecuteDataSet(sqlCommand, CommandType.Text);
            if (ds == null || ds.Tables[0].Rows.Count == 0) return true;
            return false;
        }
 

    }
}