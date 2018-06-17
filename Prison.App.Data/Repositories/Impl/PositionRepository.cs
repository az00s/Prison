using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Data.DataContext;
using System.Collections.Generic;

namespace Prison.App.Data.Repositories.Impl
{
    public class PositionRepository:IPositionRepository
    {
        private IPositionDataContext _positionContext;

        public PositionRepository(IPositionDataContext positionContext)
        {
            ArgumentHelper.ThrowExceptionIfNull(positionContext, "IPositionDataContext");

            _positionContext = positionContext;
        }

        public IReadOnlyCollection<Position> GetAllPositions()
        {
            return _positionContext.GetAllPositions();
        }

        public Position GetPositionByID(int id)
        {
            return _positionContext.GetPositionByID(id);
        }

        public void Create(Position position)
        {
            _positionContext.Create(position);
        }

        public void Update(Position position)
        {
            _positionContext.Update(position);
        }

        public void Delete(int id)
        {
            _positionContext.Delete(id);
        }
    }
}
