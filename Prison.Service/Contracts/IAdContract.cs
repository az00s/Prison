using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Prison.Service.Contracts
{
    [ServiceContract(Namespace = "http://Prison.Service.Contracts")]
    public interface IAdContract
    {
        [OperationContract]
        IEnumerable<Blurb> GetAds();
        
    }
}
