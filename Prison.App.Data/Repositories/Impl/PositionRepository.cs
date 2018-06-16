using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Data.DataContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void Create(Position emp)
        {
            _positionContext.Create(emp);
        }

        public void Update(Position emp)
        {
            _positionContext.Update(emp);
        }

        public void Delete(int id)
        {
            _positionContext.Delete(id);
        }


    }
}
