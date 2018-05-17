using System.Collections.Generic;
using System.ServiceModel;

namespace Prison.Service.Contracts
{
    [ServiceContract]
    public interface IAdContract
    {
        [OperationContract]
        IEnumerable<Blurb> GetRandomElementsFromRep(int numOfElements);
        
    }
}
