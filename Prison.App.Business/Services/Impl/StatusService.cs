using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Data.Repositories;

namespace Prison.App.Business.Services.Impl
{
    public class StatusService:IStatusService
    {
        private IStatusRepository _rep;

        public StatusService(IStatusRepository rep)
        {
            ArgumentHelper.ThrowExceptionIfNull(rep, "IStatusRepository");

            _rep = rep;
        }

        public void Create(MaritalStatus status)
        {
            _rep.Create(status);
        }

        public void Update(MaritalStatus status)
        {
            _rep.Update(status);
        }

        public void Delete(int id)
        {
            _rep.Delete(id);
        }
    }
}
