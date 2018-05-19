using System.Collections.Generic;
using System.ServiceModel;

namespace Prison.Service.Contracts
{
    [ServiceContract(Namespace = "http://Prison.Service.Contracts")]
    public interface IAdContract
    {
        [OperationContract]
        IEnumerable<Blurb> GetRandomElementsFromRep(int numOfElements);
        
    }
}
