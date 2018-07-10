using Prison.AdvertismentService.Data.Repositories;
using System.Collections.Generic;

namespace Prison.AdvertismentService.Business
{
    internal class AdProvider:IAdProvider
    {
        private IAdRepository _repository;

        public AdProvider(IAdRepository repository)
        {
            _repository = repository;
        }

        public IReadOnlyCollection<Blurb> GetAd(int numOfElements)
        {
            return _repository.GetRandomAd(numOfElements);
        }
    }
}