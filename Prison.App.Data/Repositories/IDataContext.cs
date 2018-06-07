using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prison.App.Data.Repositories
{
    public interface IDataContext<T>
    {
        DataSet ExecuteQuery(string cmdText, IDictionary<string, object> parameters, CommandType commandType);
    }
}
