using Prison.App.Business.Interfaces;
using Prison.App.Common.Entities;
using Prison.App.Data.Interfaces;
using System.Collections.Generic;

namespace Prison.App.Business.Providers
{
    public class AdvertismentProvider:IAdvertismentProvider
    {
        private IAdService _proxy;

        public AdvertismentProvider(IAdService proxy)
        {
            _proxy = proxy;
        }

        public IEnumerable<IBlurb> GetElementsFromRep(int numOfElements)
        {
            return _proxy.GetElementsFromRep(numOfElements);
        }

    }
}
