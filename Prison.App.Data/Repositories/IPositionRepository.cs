using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Data.Repositories
{
    public interface IPositionRepository
    {
        IReadOnlyCollection<Position> GetAllPositions();
        Position GetPositionByID(int id);
        void Create(Position emp);
        void Update(Position emp);
        void Delete(int id);
    }
}
