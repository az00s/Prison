using Prison.Service.Contracts;
using Prison.Service.Repositories;
using System.Collections.Generic;
using System.ServiceModel;

namespace Prison.Service.Services
{
    public class AdService:IAdContract
    {
        public IEnumerable<Blurb> GetRandomElementsFromRep(int numOfElements)
        {
            var rep = new Repository();

            var list = rep.GetRandomElementsFromRep(numOfElements);

            if (list == null)
            {
                throw new FaultException("List of Blurbs is empty!");
            }
            return list;
        }
    }
}