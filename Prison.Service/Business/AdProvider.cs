using Prison.AdvertismentService.Repositories;
using System.Collections.Generic;

namespace Prison.AdvertismentService.Business
{
    internal class AdProvider
    {
        private AdRepository _rep;

        public AdProvider()
        {
            _rep = new AdRepository();
        }

        public IReadOnlyCollection<Blurb> GetAd(int numOfElements)
        {
            return _rep.GetRandomAd(numOfElements);
        }
    }
}