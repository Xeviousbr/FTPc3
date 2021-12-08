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
using System.Data;

#if oldDriver
    using Oracle.DataAccess.Client;
#else
    using Oracle.ManagedDataAccess.Client;
#endif

namespace FEPAM.DAL
{
    /// <summary>
    /// Enumerador com as opções de banco de dados
    /// </summary>
    public enum DalDbType { Oracle, SqlServer }
    /// <summary>
    /// Enumerado com os tipos de dados conhecidos
    /// </summary>
    public enum DalTypes { Integer, String, Char, ReturnString, ReturnInteger, Clob, Double }

    /// <summary>
    /// Classe para abstração ao acesso do banco de dados Oracle 9.i
    /// </summary>
    /// <remarks>
    /// Esta classe implementa a interface IDAL e fornece as operações
    /// para acesso ao banco de dados 9i ou superior através o Oracle 
    /// Data Provider
    /// </remarks>
    public class OracleAcessLayer : IDAL
    {
        private OracleCommand _cmd;
        private OracleConnection _conn;
        private List<OracleParameter> _params;
        //private ListOfParam LoP;

        /// <summary>
        /// Método construtor
        /// </summary>
        /// /// <remarks>
        /// Método construtor que captura a string de conexão da
        /// classe Connection
        /// </remarks>
        public OracleAcessLayer()
        {
            try
            {
                _conn = new OracleConnection(Connection.DbConnection());
                _cmd = new OracleCommand();
                _cmd.Connection = _conn;
                _params = new List<OracleParameter>();
                //LoP = new ListOfParam();//Parametro addicionado para facilitar a debugação

            }
            catch (Exception ex)
            {
                throw new Exception("Não é possível inicializar a conexão: " + ex.ToString());
            }

        }

        /// <summary>
        /// Método cosntrutor
        /// </summary>
        /// <remarks>
        /// Método construtor que recebe a string de conexão
        /// </remarks>
        /// <param name="connectionString">String de conexão</param>
        public OracleAcessLayer(string connectionString)
        {
            try
            {
                _conn = new OracleConnection(connectionString);
                _cmd = new OracleCommand();
                _cmd.Connection = _conn;
                _params = new List<OracleParameter>();
            }
            catch
            {
                throw new Exception("Não é possível inicializar a conexão");
            }
        }


        /// <summary>
        /// Método para adicionar parâmetros de entrada para utilização em 
        /// Stored Procedures
        /// </summary>
        /// <remarks>
        /// Prepara um parâmetro através de seu nome, tipo e valor
        /// e o adiciona a coleção de parâmetros
        /// </remarks>
        /// <param name=param name="operation">Operação: OPR (Insert,Delete,Update) e SEL (Select)</param>
        /// <param name="name">Nome do parâmetro</param>
        /// <param name="value">Valor do parâmetro</param>
        /// <param name="tp">Tipo do parâmetro</param>
        public void AddInParameter(string operation, string name, object value, DalTypes tp)
        {
            AddParameter(name, value, tp);
            OracleParameter param = new OracleParameter();
            if (tp == DalTypes.Integer)
            {
                param = new OracleParameter(name, OracleDbType.Double);
                if (Convert.ToInt64(value) == 0)
                {
                    if (operation == "OPR") value = DBNull.Value; else value = 0;
                }
                if (value == null)
                {
                    if (operation == "OPR") value = DBNull.Value; else value = 0;
                }
                param.Value = value;
            }

            else if (tp == DalTypes.Double)
            {
                param = new OracleParameter(name, OracleDbType.Double);
                if (value == null)
                {
                    if (operation == "OPR") value = DBNull.Value; else value = 0;
                }
                param.Value = value;
            }

            else if (tp == DalTypes.Clob)
            {
                param = new OracleParameter(name, OracleDbType.Clob);
                if (value == null)
                {
                    if (operation == "OPR") value = DBNull.Value; else value = "null";
                }
                param.Value = value;
            }
            else
            {
                param = new OracleParameter(name, OracleDbType.Varchar2);
                if (value == null)
                {
                    if (operation == "OPR") value = DBNull.Value; else value = "null";
                }

                try
                {
                    param.Value = value.ToString().Replace((char)39, (char)180);
                }
                catch
                {
                    param.Value = value;
                }
            }
            param.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(param);

            //LoP.AddList(value.ToString());
        }

        String AllParameterAdd = "";

        /// <summary>
        /// Este método Cria uma lista de todos os parametros add
        /// Para facilitar o debug
        /// </summary>
        /// <param name="name">Nome do parâmetro</param>
        /// <param name="value">Valor do parâmetro</param>
        /// <param name="tp">Tipo do parâmetro</param>
        public void AddParameter(string name, object value, DalTypes ParameterType)
        {
            if (name != "")
            {
                AllParameterAdd = AllParameterAdd + name.ToString() + "\t" + (value == null ? "null" : value.ToString()) + "\t" + ParameterType.ToString() + System.Environment.NewLine;
            }
        }

        /// <summary>
        /// Este método Retorna a lista dos parametros add
        /// Para facilitar o debug
        /// O dado será apresentado da seguinte forma:
        /// (Nome do parâmetro) \t (Valor do parâmetro) \t (Tipo do parâmetro)
        /// </summary>
        public String GetParameters()
        {
            return AllParameterAdd;
        }



        /// <summary>
        /// Este método adiciona o parâmetro puramente como o valor dele vem,
        /// sem necessidade de colocar o parâmetro operation que processa os dados vindos da tela
        /// </summary>
        /// <param name="name">Nome do parâmetro</param>
        /// <param name="value">Valor do parâmetro</param>
        /// <param name="tp">Tipo do parâmetro</param>
        public void AddInParameter(string name, object value, DalTypes tp)
        {
            AddParameter(name, value, tp);
            OracleParameter param = new OracleParameter();
            if (tp == DalTypes.Integer)
            {
                param = new OracleParameter(name, OracleDbType.Double);
                param.Value = value;
            }
            else if (tp == DalTypes.Double)
            {
                param = new OracleParameter(name, OracleDbType.Double);
                param.Value = value;
            }
            else if (tp == DalTypes.Clob)
            {
                param = new OracleParameter(name, OracleDbType.Clob);
                param.Value = value;
            }
            else
            {
                param = new OracleParameter(name, OracleDbType.Varchar2);
                try
                {
                    param.Value = value.ToString().Replace((char)39, (char)180);
                }
                catch
                {
                    param.Value = value;
                }
            }
            param.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(param);
            //LoP.AddList(value.ToString());// 
        }

        /// <summary>
        /// Método para adicionar parâmetros de saída para utilização em 
        /// Stored Procedures
        /// </summary>
        /// <remarks>
        /// Prepara um parâmetro através de seu nome, tipo e valor
        /// e o adiciona a coleção de parâmetros
        /// </remarks>
        /// <param name="name">Nome do parâmetro</param>
        /// <param name="value">Valor do parâmetro</param>
        /// <param name="tp">Tipo do parâmetro</param>
        public void AddOutParameter(string name, object value, DalTypes tp)
        {
            OracleParameter param = new OracleParameter();
            if (tp == DalTypes.Integer | tp == DalTypes.Double)
            {
                param = new OracleParameter(name, OracleDbType.Double);
                param.Value = value;
            }
            else if (tp == DalTypes.Clob)
            {
                param = new OracleParameter(name, OracleDbType.Clob);
                param.Value = value;
            }
            else
            {
                param = new OracleParameter(name, OracleDbType.Varchar2);

                try
                {
                    param.Value = value.ToString().Replace((char)39, (char)180);
                }
                catch
                {
                    param.Value = value;
                }
            }
            param.Direction = ParameterDirection.Output;
            _params.Add(param);
        }

        public void AddOutParameter(string name, object value, DalTypes tp, int size)
        {
            OracleParameter param = new OracleParameter();
            param.Size = size;
            if (tp == DalTypes.Integer | tp == DalTypes.Double)
            {
                param = new OracleParameter(name, OracleDbType.Double);
                param.Value = value;
            }
            else if (tp == DalTypes.Clob)
            {
                param = new OracleParameter(name, OracleDbType.Clob);
                param.Value = value;
            }
            else
            {
                param = new OracleParameter(name, OracleDbType.Varchar2);
                param.Value = value;
            }
            param.Direction = ParameterDirection.Output;
            _params.Add(param);
        }

        /// <summary>
        /// Método para adicionar parâmetros de retorno
        /// Stored Procedures
        /// </summary>
        /// <remarks>
        /// Prepara um parâmetro através de seu nome, tipo e valor
        /// e o adiciona a coleção de parâmetros
        /// </remarks>
        /// <param name="name">Nome do parâmetro</param>
        /// <param name="value">Valor do parâmetro</param>
        /// <param name="tp">Tipo do parâmetro</param>
        public void AddReturnParameter(string name, DalTypes tp)
        {
            OracleParameter param = new OracleParameter();
            if (tp == DalTypes.Integer | tp == DalTypes.Double)
            {
                param = new OracleParameter(name, OracleDbType.Double);
            }
            else if (tp == DalTypes.Clob)
            {
                param = new OracleParameter(name, OracleDbType.Clob);
            }
            else
            {
                param = new OracleParameter(name, OracleDbType.Varchar2);
            }
            param.Direction = ParameterDirection.ReturnValue;
            _params.Add(param);
        }

        /// <summary>
        /// Método que retorna o valor do parâmetro informado.
        /// </summary>
        /// <param name="name">Nome do parâmetro que deseja obter o valor</param>
        /// <returns>Retorna um objeto com o valor do parâmetro</returns>
        public object GetParameterValue(string name)
        {
            return _cmd.Parameters[name].Value;
        }

        /// <summary>
        /// Método que adiciona uma parâmetro do tipo CURSOR na Stored Procedure
        /// </summary>
        /// <remarks>
        /// Prepara um parâmetro através de seu nome, tipo e valor
        /// e o adiciona a coleção de parâmetros
        /// </remarks>
        /// <param name="name">Nome do parâmetro</param>
        public void AddCursorParameter(string name, string procedureName = "")
        {

            OracleParameter param = new OracleParameter(name, OracleDbType.RefCursor);
            /*if (procedureName.Equals("PCK_NET_COMUNICACAO.SP_NOTICIA_SRC"))
            {
                OracleParameter myParam = new OracleParameter("stexto", OracleDbType.Clob);
                myParam.Direction = ParameterDirection.Output;
                _params.Add(myParam);
            }*/
            param.Direction = ParameterDirection.Output;
            _params.Add(param);
        }

        /// <summary>
        /// Método que retorna um Dataset através de uma consulta
        /// </summary>
        /// <remarks>
        /// Recebe uma consulta e seu tipo, filtrando este último e direcionando
        /// para o método correto e retornando um DataSet com a consulta
        /// </remarks>
        /// <param name="sql">Consulta para execução</param>
        /// <param name="type">Tipo de consulta</param>
        /// <returns>Dataset com a Consulta</returns>
        public DataSet ExecuteDataSet(string sql, CommandType type)
        {
            DataSet dt;
            if (type == CommandType.Text)
            {
                dt = ExecuteDataSetQry(sql);
            }
            else
            {
                dt = ExecuteDataSetSP(sql);
            }
            return dt;
        }

        /// <summary>
        /// Método que retorna um Dataset através de uma consulta com tratamento de exceções
        /// </summary>
        /// <remarks>
        /// Recebe uma consulta e seu tipo, filtrando este último e direcionando
        /// para o método correto e retornando um DataSet com a consulta
        /// </remarks>
        /// <param name="sql">Consulta para execução</param>
        /// <param name="type">Tipo de consulta</param>
        /// <returns>Dataset com a Consulta</returns>
        public DataSet ExecuteDataSet_OracleException(string sql, CommandType type)
        {
            DataSet dt;
            try
            {
                if (type == CommandType.Text)
                {
                    dt = ExecuteDataSetQry(sql);
                }
                else
                {
                    dt = ExecuteDataSetSP(sql);
                }
                return dt;
            }
            catch (OracleException EX)
            {
                throw new Exception(EX.Number.ToString(), (Exception)EX);
            }
        }

        /// <summary>
        /// Método para executar uma consulta via dataset por 
        /// Stored Procedure
        /// <remarks>
        /// Recebe uma consulta, executa a stores procedure adicionando um cursor
        /// e retorna um DataSet com a consulta
        /// </remarks>
        /// <param name="sql">Consulta para execução</param>
        /// <returns>Dataset com a Consulta</returns>
        private DataSet ExecuteDataSetSP(string sql)
        {
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.CommandText = sql;
            DataSet dt = new DataSet();

            this.AddCursorParameter("cur", sql);
            foreach (OracleParameter p in _params)
            {
                _cmd.Parameters.Add(p);
            }
            OracleDataAdapter adapter = new OracleDataAdapter(_cmd);
            try
            {
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                string exe = ex.ToString();
                throw;
            }
            
            adapter.Dispose();
            return dt;
        }

        /// <summary>
        /// Método para executar uma consulta via dataset por 
        /// query
        /// <remarks>
        /// Recebe uma consulta, executa a query
        /// e retorna um DataSet com a consulta
        /// </remarks>
        /// <param name="sql">Consulta para execução</param>
        /// <returns>Dataset com a Consulta</returns>
        private DataSet ExecuteDataSetQry(string sql)
        {
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = sql;
            DataSet dt = new DataSet();
            OracleDataAdapter adapter = new OracleDataAdapter(_cmd);
            adapter.Fill(dt);
            return dt;
        }

        private IDataReader ExecuteDataReaderQry(string sql)
        {
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = sql;
            _cmd.Connection.Open();
            return _cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        private IDataReader ExecuteDataReaderSP(string sql)
        {
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.CommandText = sql;
            DataSet dt = new DataSet();
            this.AddCursorParameter("cur");
            foreach (OracleParameter p in _params)
            {
                _cmd.Parameters.Add(p);
            }
            _cmd.Connection.Open();
            return _cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// Método que executa uma query sem retorno
        /// </summary>
        /// <remarks>
        /// recebe uma consulta e seu tipo, filtrando este último e direcionando
        /// para o método correto
        /// </remarks>
        /// <param name="sql">Consulta para execução</param>
        /// <param name="type">Tipo de Execução</param>
        public void ExeuteNonQuery(string sql, CommandType type)
        {
            if (type == CommandType.Text)
            {
                ExecuteNonQueryQry(sql);
            }
            else
            {
                ExecuteNonQuerySP(sql);
            }
        }


        /// <summary>
        /// Método que executa uma query sem retorno
        /// </summary>
        /// <remarks>
        /// recebe uma consulta e seu tipo, filtrando este último e direcionando
        /// para o método correto
        /// </remarks>
        /// <param name="sql">Consulta para execução</param>
        /// <param name="type">Tipo de Execução</param>
        public void ExeuteNonQuery_OracleException(string sql, CommandType type)
        {
            try
            {
                if (type == CommandType.Text)
                {
                    ExecuteNonQueryQry(sql);
                }
                else
                {
                    ExecuteNonQuerySP(sql);
                }
            }
            catch (OracleException EX)
            {
                throw new Exception(EX.Number.ToString(), (Exception)EX);
            }
        }

        /// <summary>
        /// Método que executa uma query sem retorno
        /// </summary>
        /// <remarks>
        /// Recebe uma consulta e executa via stored procedure
        /// </remarks>
        /// <param name="sql">Consulta para execução</param>
        private void ExecuteNonQuerySP(string sql)
        {

            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.CommandText = sql;
            foreach (OracleParameter p in _params)
            {
                _cmd.Parameters.Add(p);
            }
            _conn.Open();
            _cmd.ExecuteNonQuery();

            _conn.Close();

        }

        /// <summary>
        /// Método que executa uma query sem retorno
        /// </summary>
        /// <remarks>
        /// Recebe uma consulta e executa via query
        /// </remarks>
        /// <param name="sql">Consulta para execução</param>
        private void ExecuteNonQueryQry(string sql)
        {
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = sql;
            _conn.Open();
            _cmd.ExecuteNonQuery();
            _conn.Close();
        }

        /// <summary>
        /// Método que executa uma query com retorno único
        /// </summary>
        /// <remarks>
        /// recebe uma consulta e seu tipo, filtrando este último e direcionando
        /// para o método correto
        /// </remarks>
        /// <param name="sql">Consulta para execução</param>
        /// <param name="type">Tipo de Execução</param>
        public object ExeuteScalar(string sql, CommandType type)
        {
            object ts = new object();
            if (type == CommandType.Text)
            {
                ts = ExecuteScalarSP(sql);
            }
            else
            {
                ts = ExecuteScalarQry(sql);
            }
            return ts;
        }

        /// <summary>
        /// Método que executa uma query de retorno simples
        /// </summary>
        /// <remarks>
        /// Recebe uma consulta e executa via stored procedure
        /// </remarks>
        /// <param name="sql">Consulta para execução</param>
        private object ExecuteScalarSP(string sql)
        {
            object ts = new object();
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.CommandText = sql;
            foreach (OracleParameter p in _params)
            {
                _cmd.Parameters.Add(p);
            }
            _conn.Open();
            ts = _cmd.ExecuteScalar();
            _conn.Close();
            return ts;
        }

        /// <summary>
        /// Método que executa uma query de retorno simples
        /// </summary>
        /// <remarks>query stored procedure
        /// </remarks>
        /// <param name="sql">Consulta para execução</param>
        private object ExecuteScalarQry(string sql)
        {
            object ts = new object();
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = sql;
            _conn.Open();
            ts = _cmd.ExecuteScalar();
            _conn.Close();
            return ts;
        }

        #region IDAL Members


        public IDataReader ExecuteDataReader(string sql, CommandType type)
        {
            IDataReader dr;
            if (type == CommandType.Text)
            {
                dr = ExecuteDataReaderQry(sql);
            }
            else
            {
                dr = ExecuteDataReaderSP(sql);
            }
            return dr;
        }

        #endregion
    }

    /// <summary>
    /// Classe para abstração de banco de dados
    /// </summary>
    /// <remarks>
    /// Esta classe implementa a abstração do banco de dados, dvendo
    /// ser instanciada utilizando uma classe que implemente a interface IDAL
    /// para geração de chamada ao banco
    /// </remarks>   
    public class DataAccessLayer : MarshalByRefObject, IDalFactory
    {
        /// <summary>
        /// Método para criação de uma nova instancia e 2 camadas
        /// </summary>
        /// <remarks>
        /// Este método cria uma instância da classe DataAcessLayer integrada 
        /// a uma classe IDAL para acasso ao banco de dados
        /// </remarks>
        /// <param name="dbType">Tipo de banco de dados a ser instanciado</param>
        /// <returns>Retorna classe de abstração de banco</returns>
        public IDAL CreateDAL(DalDbType dbType)
        {
            IDAL dataAccessLayer = null;
            switch (dbType)
            {
                case DalDbType.Oracle:
                    dataAccessLayer = new OracleAcessLayer();
                    break;
                case DalDbType.SqlServer:
                    //dataAccessLayer = new SqlServer2000Dal();
                    break;
            }

            return dataAccessLayer;
        }
    }
    //public List<String> GetAllParamAdd()
    //{
    //    return LoP.GetList()
    //}

}