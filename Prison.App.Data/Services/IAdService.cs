using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Data.Interfaces
{
    public interface IAdService
    {
        IReadOnlyCollection<IBlurb> GetAds(int numOfElements);
    }
}
