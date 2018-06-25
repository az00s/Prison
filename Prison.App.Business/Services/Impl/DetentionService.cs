using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Data.Repositories;

namespace Prison.App.Business.Services.Impl
{
    public class DetentionService: IDetentionService
    {
        private IDetentionRepository _rep;

        public DetentionService(IDetentionRepository rep)
        {
            ArgumentHelper.ThrowExceptionIfNull(rep, "IDetentionRepository");
            _rep = rep;
        }

        public void Create(Detention detention)
        {
            _rep.Create(detention);
        }

    }
}
