using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Prison.App.Data.DataContext
{
    internal interface IDataContext<T>
    {
        DataSet ExecuteQuery(string cmdText, IDictionary<string, object> parameters, CommandType commandType);
        void ExecuteNonQuery(string cmdText, IDictionary<string, object> parameters, CommandType commandType);
        void ExecuteNonQuery(string cmdText, SqlParameter[] parameters, CommandType commandType);
        List<SqlParameter> GetParameterList(IDictionary<string, object> parameters);
        SqlParameter CreateCustomParameter(string parameterName, IEnumerable<int> value, SqlDbType sqlDbType = SqlDbType.Variant, string typeName = null);    }
}
