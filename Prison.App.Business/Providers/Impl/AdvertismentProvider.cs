using Prison.App.Business.Interfaces;
using Prison.App.Common.Entities;
using Prison.App.Data.Interfaces;
using Prison.App.Data.ServiceReference;
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

        public IEnumerable<IBlurb> GetRandomElementsFromRep(int numOfElements)
        {
            return _proxy.GetRandomElementsFromRep(numOfElements);
        }

    }
}
