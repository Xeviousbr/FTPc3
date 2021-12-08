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

namespace FEPAM.DAL
{
    /// <summary>
    /// Interface para abstração de banco de dados
    /// </summary>
    public interface IDAL
    {
        void AddInParameter(string operation, string name, object value, DalTypes tp);
        void AddInParameter(string name, object value, DalTypes tp);
        void AddOutParameter(string name, object value, DalTypes tp);
        void AddOutParameter(string name, object value, DalTypes tp, int size);
        void AddReturnParameter(string name, DalTypes tp);
        object GetParameterValue(string name);
        //void AddCursorParameter(string name);
        void AddCursorParameter(string name, string sql);
        DataSet ExecuteDataSet(string sql, CommandType type);
        DataSet ExecuteDataSet_OracleException(string sql, CommandType type);
        void ExeuteNonQuery(string sql, CommandType type);
        void ExeuteNonQuery_OracleException(string sql, CommandType type);
        object ExeuteScalar(string sql, CommandType type);
        IDataReader ExecuteDataReader(string sql, CommandType type);
        String GetParameters();
    }

    /// <summary>
    /// Interface para fábrica de abstração de banco de dados
    /// </summary>
    public interface IDalFactory
    {
        IDAL CreateDAL(DalDbType dbType);
    }
}
