using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Data.DataContext;
using System.Collections.Generic;

namespace Prison.App.Data.Repositories.Impl
{
    class StatusRepository:IStatusRepository
    {
        private IMaritalStatusDataContext _statusContext;

        public StatusRepository(IMaritalStatusDataContext statusContext)
        {
            ArgumentHelper.ThrowExceptionIfNull(statusContext, "IMaritalStatusDataContext");

            _statusContext = statusContext;
        }

        public IReadOnlyCollection<MaritalStatus> GetAllStatuses()
        {
            return _statusContext.GetAllStatuses();
        }

        public MaritalStatus GetStatusByID(int id)
        {
            return _statusContext.GetStatusByID(id);
        }

        public void Create(MaritalStatus status)
        {
            _statusContext.Create(status);
        }

        public void Update(MaritalStatus status)
        {
            _statusContext.Update(status);
        }

        public void Delete(int id)
        {
            _statusContext.Delete(id);
        }
    }
}
