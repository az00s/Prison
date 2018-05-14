using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prison.App.Data.Repositories.Common
{
    public interface IDataCommonOperation<T>
    {
        void Create(T dtn);
        void Update(T dtn);
        void Delete(int id);
        IEnumerable<T> GetAllRecordsFromTable();
    }
}
