using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Business.Interfaces
{
    public interface IAdvertismentProvider
    {
        IEnumerable<IBlurb> GetElementsFromRep(int numOfElements);
    }
}
