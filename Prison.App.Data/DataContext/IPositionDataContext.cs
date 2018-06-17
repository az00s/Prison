using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Data.DataContext
{
    public interface IPositionDataContext
    {
        IReadOnlyCollection<Position> GetAllPositions();
        Position GetPositionByID(int id);
        void Create(Position position);
        void Update(Position position);
        void Delete(int id);
    }
}
