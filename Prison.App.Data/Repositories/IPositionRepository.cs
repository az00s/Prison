using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Data.Repositories
{
    public interface IPositionRepository
    {
        
            IEnumerable<Position> GetAllPositions();
    }
}
