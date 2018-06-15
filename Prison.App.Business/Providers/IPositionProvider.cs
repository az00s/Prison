using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Business.Providers
{
    public interface IPositionProvider
    {
        IEnumerable<Position> GetAllPositions();
        Position GetPositionByID(int id);
    }
}
