using System.Collections.Generic;

namespace Prison.AdvertismentService.Business
{
    public interface IAdProvider
    {
        IReadOnlyCollection<Blurb> GetAd(int numOfElements);
    }
}
