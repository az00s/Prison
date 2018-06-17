using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Data.Repositories
{
    public interface IStatusRepository
    {
        IReadOnlyCollection<MaritalStatus> GetAllStatuses();
        void Create(MaritalStatus status);
        void Update(MaritalStatus status);
        void Delete(int id);
        MaritalStatus GetStatusByID(int id);
    }
}
