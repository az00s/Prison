using Prison.Service.Contracts;
using Prison.Service.Repositories;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Prison.Service.Services
{
    public class AdService:IAdContract
    {
        public IEnumerable<Blurb> GetAds()
        {
            Repository rep = new Repository();

            var list = rep.GetRandomElementsFromRep(3);

            //list = null;
            if (list == null)
            {
                ArgumentNullException ex = new ArgumentNullException("IEnumerable<Blurb>", "List of Blurbs is empty!");
                throw new FaultException<ArgumentNullException>(ex,ex.Message);
            }
            return list;

        }
    }
}