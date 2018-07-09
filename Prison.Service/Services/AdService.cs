using Prison.AdvertismentService.Business;
using Prison.AdvertismentService.Contracts;
using System.Collections.Generic;
using System.ServiceModel;

namespace Prison.AdvertismentService.Services
{
    public class AdService:IAdContract
    {
        private AdProvider _adProvider;

        public AdService()
        {
            _adProvider = new AdProvider();
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