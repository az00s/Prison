using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Data.DataContext
{
    public interface IMaritalStatusDataContext
    {
        IReadOnlyCollection<MaritalStatus> GetAllStatuses();
        MaritalStatus GetStatusByID(int id);
        void Create(MaritalStatus status);
        void Update(MaritalStatus status);
        void Delete(int id);
    }
}
