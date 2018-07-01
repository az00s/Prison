using Prison.App.Common.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Prison.App.Data.DataContext
{
    internal interface IDataContext<T>
    {
        DataSet ExecuteQuery(string cmdText, IDictionary<string, object> parameters, CommandType commandType);
        void ExecuteNonQuery(string cmdText, IDictionary<string, object> parameters, CommandType commandType);
        void ExecuteNonQuery(string cmdText, IDataParameter[] parameters, CommandType commandType);
        List<IDataParameter> GetParameterList(IDictionary<string, object> parameters);
        IDataParameter CreateCustomParameter(string parameterName, IEnumerable values, string columnName, SqlDbType sqlDbType = SqlDbType.Variant, string typeName = null);
        IDataParameter CreateCustomParameter(string parameterName, Detention value, SqlDbType sqlDbType = SqlDbType.Variant, string typeName = null);
    }
}
