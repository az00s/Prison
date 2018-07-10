using Prison.AdvertismentService.Business;
using Prison.AdvertismentService.Contracts;
using System.Collections.Generic;
using System.ServiceModel;

namespace Prison.AdvertismentService.Services
{
    internal class AdService:IAdContract
    {
        private IAdProvider _adProvider;

        public AdService(IAdProvider adProvider)
        {
            _adProvider = adProvider;
        }

        public IEnumerable<Blurb> GetAd(int numOfElements)
        {
            var list = _adProvider.GetAd(numOfElements);

            if (list == null)
            {
                throw new FaultException("List of Blurbs is empty!");
            }

            return list;
        }
    }
}