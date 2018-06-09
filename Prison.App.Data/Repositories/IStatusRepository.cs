using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Data.Repositories
{
    public interface IStatusRepository
    {
        IEnumerable<MaritalStatus> GetAllStatuses();
        void Create(MaritalStatus emp);
        void Update(MaritalStatus emp);
        void Delete(int id);
        MaritalStatus GetStatusByID(int id);
    }
}
