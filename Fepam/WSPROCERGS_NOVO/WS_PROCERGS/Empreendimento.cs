using System;
using System.ComponentModel;
using System.Data;
using FEPAM.DAL;

namespace WS_PROCERGS
{
    [DataObject]
    [Serializable]
    public class Empreendimento
    {
        public Int64? nroSolicitacao { get; set; }      // Este campo não tem no JSON mas é setado pelo campo em Processo
        public Int64? nroCodEmpreendimento { get; set; }
        public double? vlrPorteEmpreendimento { get; set; }
        public string txtCepEmpreendimento { get; set; }
        public string txtTipoLogradouroEmpreendimento { get; set; }
        public string txtLogradouroEmpreendimento { get; set; }
        public string txtNumeroEmpreendimento { get; set; }
        public string txtComplementoEmpreendimento { get; set; }
        public string txtReferenciaEmpreendimento { get; set; }
        public string txtBairroEmpreendimento { get; set; }
        public string txtMunicipioEmpreendimento { get; set; }
        public double? nroLatitudeEmpreendimento { get; set; }
        public double? nroLongitudeEmpreendimento { get; set; }
        public string txtCodCar { get; set; }
        public bool indMaisDeUmMunicipio { get; set; }  // Este campo não tem no JSON mas é setado pelo campo em Processo

        public void Insert()
        {
            try
            {
                IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
                string sqlCommand = "PCK_NET_WEBSERV01.SP_WSE3_INS";
                db.AddInParameter("OPR", "P_SEQ_WEBS", nroSolicitacao, DalTypes.Integer);
                db.AddInParameter("OPR", "nroCodEmpto", nroCodEmpreendimento, DalTypes.Integer);
                db.AddInParameter("OPR", "vlrPorteEmpto", vlrPorteEmpreendimento, DalTypes.Double);
                db.AddInParameter("OPR", "txtCepEmpto", txtCepEmpreendimento, DalTypes.String);
                db.AddInParameter("OPR", "txtTipoLogradouroEmpto", txtTipoLogradouroEmpreendimento, DalTypes.String);
                db.AddInParameter("OPR", "txtLogradouroEmpto", txtLogradouroEmpreendimento, DalTypes.String);
                db.AddInParameter("OPR", "txtNumeroEmpto", txtNumeroEmpreendimento, DalTypes.String);
                db.AddInParameter("OPR", "txtComplementoEmpto", txtComplementoEmpreendimento, DalTypes.String);
                db.AddInParameter("OPR", "txtReferenciaEmpto", txtReferenciaEmpreendimento, DalTypes.String);
                db.AddInParameter("OPR", "txtBairroEmpto", txtBairroEmpreendimento, DalTypes.String);
                db.AddInParameter("OPR", "txtMunicipioEmpto", txtMunicipioEmpreendimento, DalTypes.String);
                db.AddInParameter("OPR", "nroLatitudeEmpto", nroLatitudeEmpreendimento, DalTypes.Double);
                db.AddInParameter("OPR", "nroLongitudeEmpto", nroLongitudeEmpreendimento, DalTypes.Double);
                db.AddInParameter("OPR", "indMaisDeUmMunicipio", indMaisDeUmMunicipio ? "S" : "N", DalTypes.String);
                db.AddInParameter("OPR", "txtCodCar", txtCodCar, DalTypes.String);
                db.ExeuteNonQuery(sqlCommand, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Logs.WriteLog(ex.Message);
            }
        }

    }
}