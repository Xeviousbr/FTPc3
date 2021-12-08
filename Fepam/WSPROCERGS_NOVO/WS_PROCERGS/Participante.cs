using System;
using System.ComponentModel;
using System.Data;
using FEPAM.DAL;

namespace WS_PROCERGS
{
    [DataObject]
    [Serializable]
    public class Participante
    {
        public int? nroSolicitacao { get; set; }    // Este campo não tem no JSON mas é setado pelo campo em Processo
        public string txtTipoPapel { get; set; }
        public string txtTipoPessoa { get; set; }
        public string txtDocumentoIdentificacao { get; set; }
        public string txtNome { get; set; }
        public string txtCep { get; set; }
        public string txtTipoLogradouro { get; set; }
        public string txtLogradouro { get; set; }
        public string txtNumero { get; set; }
        public string txtComplemento { get; set; }
        public int? nroCodIbgeMunicipio { get; set; }
        public string txtNomeMunicipio { get; set; }
        public string txtSiglaUfMunicipio { get; set; }
        public string txtEmail { get; set; }
        public string txtFone { get; set; }
        public string txtReferencia { get; set; }
        public string txtBairro { get; set; }
        public string txtNomeContato { get; set; }
        public string txtVinculoContato { get; set; }
        public string txtEmailContato { get; set; }
        public string txtFoneContato { get; set; }
        public string txtNroArt { get; set; }
        public string txtInclusao { get; set; }
        public string txtTipoPessoaRepresentada { get; set; }
        public string txtDocIdentPessoaRepresentada { get; set; }

        public void Insert()
        {
            try
            {

                IDAL db = new DataAccessLayer().CreateDAL(DalDbType.Oracle);
                string sqlCommand = "PCK_NET_WEBSERV01.SP_WSE2_INS";
                db.AddInParameter("OPR", "P_SEQ_WEBS", nroSolicitacao, DalTypes.Integer);
                db.AddInParameter("OPR", "txtTipoPapel", txtTipoPapel, DalTypes.String);
                db.AddInParameter("OPR", "txtTipoPessoa", txtTipoPessoa, DalTypes.String);
                db.AddInParameter("OPR", "txtDocumentoIdentificacao", txtDocumentoIdentificacao, DalTypes.String);
                db.AddInParameter("OPR", "txtNome", txtNome, DalTypes.String);
                db.AddInParameter("OPR", "txtCep", txtCep, DalTypes.String);
                db.AddInParameter("OPR", "txtTipoLogradouro", txtTipoLogradouro, DalTypes.String);
                db.AddInParameter("OPR", "txtLogradouro", txtLogradouro, DalTypes.String);
                db.AddInParameter("OPR", "txtNumero", txtNumero, DalTypes.String);
                db.AddInParameter("OPR", "txtComplemento", txtComplemento, DalTypes.String);
                db.AddInParameter("OPR", "txtEmail", txtEmail, DalTypes.String);
                db.AddInParameter("OPR", "txtFone", txtFone, DalTypes.String);
                db.AddInParameter("OPR", "txtReferencia", txtReferencia, DalTypes.String);
                db.AddInParameter("OPR", "txtBairro", txtBairro, DalTypes.String);
                db.AddInParameter("OPR", "txtNomeContato", txtNomeContato, DalTypes.String);
                db.AddInParameter("OPR", "txtVinculoContato", txtVinculoContato, DalTypes.String);
                db.AddInParameter("OPR", "txtEmailContato", txtEmailContato, DalTypes.String);
                db.AddInParameter("OPR", "txtFoneContato", txtFoneContato, DalTypes.String);
                db.AddInParameter("OPR", "txtNroArt", txtNroArt, DalTypes.String);
                db.AddInParameter("OPR", "nroCodIbgeMunicipio", nroCodIbgeMunicipio, DalTypes.Integer);
                db.AddInParameter("OPR", "txtNomeMunicipio", txtNomeMunicipio, DalTypes.String);
                db.AddInParameter("OPR", "txtSiglaMunicipio", txtSiglaUfMunicipio, DalTypes.String);
                db.AddInParameter("OPR", "txtTipoPessoaRepresentada", txtTipoPessoaRepresentada, DalTypes.String);
                db.AddInParameter("OPR", "txtDocIdentPessoaRepresentada", txtDocIdentPessoaRepresentada, DalTypes.String);
                db.ExeuteNonQuery(sqlCommand, CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                Logs.WriteLog(ex.Message);

            }
        }


    }
}