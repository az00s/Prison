using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Data.Repositories;

namespace Prison.App.Business.Services.Impl
{
    public class PositionService:IPositionService
    {
        private IPositionRepository _rep;

        public PositionService(IPositionRepository rep)
        {
            ArgumentHelper.ThrowExceptionIfNull(rep, "IPositionRepository");

            _rep = rep;
        }

        public void Create(Position pos)
        {
            _rep.Create(pos);
        }

        public void Update(Position pos)
        {
            _rep.Update(pos);
        }

        public void Delete(int id)
        {
            _rep.Delete(id);
        }
    }
}
