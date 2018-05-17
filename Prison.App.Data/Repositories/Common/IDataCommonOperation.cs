using System.Collections.Generic;

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
