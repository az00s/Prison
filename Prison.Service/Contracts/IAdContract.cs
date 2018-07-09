using System.Collections.Generic;
using System.ServiceModel;

namespace Prison.AdvertismentService.Contracts
{
    [ServiceContract(Namespace = "http://Prison.AdvertismentService.Contracts")]
    public interface IAdContract
    {
        [OperationContract]
        IEnumerable<Blurb> GetAd(int numOfElements);
    }
}
