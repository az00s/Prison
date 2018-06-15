using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Business.Providers
{
    public interface IStatusProvider
    {
        IEnumerable<MaritalStatus> GetAllStatuses();
        MaritalStatus GetStatusByID(int id);
    }
}
