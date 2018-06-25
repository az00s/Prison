using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Data.DataContext;
using System.Collections.Generic;

namespace Prison.App.Data.Repositories.Impl
{
    public class DetentionRepository: IDetentionRepository
    {
        private IDetentionDataContext _detentionContext;

        public DetentionRepository(IDetentionDataContext detentionContext)
        {
            ArgumentHelper.ThrowExceptionIfNull(detentionContext, "IDetentionDataContext");

            _detentionContext = detentionContext;
        }

        public IReadOnlyCollection<Detention> GetAll()
        {
            return _detentionContext.GetAll();
        }

        public IReadOnlyCollection<Detention> GetDetentionsForLast3Days()
        {
            return _detentionContext.GetDetentionsForLast3Days();
        }

        public Detention GetLast(int id)
        {
            return _detentionContext.GetLast(id);
        }

        public Detention GetByID(int id)
        {
            return _detentionContext.GetByID(id);
        }

        public void Create(Detention detention)
        {
            _detentionContext.Create(detention);
        }

    }
}
