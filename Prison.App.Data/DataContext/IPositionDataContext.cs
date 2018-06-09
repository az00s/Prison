using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Data.DataContext
{
    public interface IPositionDataContext
    {
        IEnumerable<Position> GetAllPositions();
        Position GetPositionByID(int id);
        void Create(Position dtn);
        void Update(Position dtn);
        void Delete(int id);
    }
}
