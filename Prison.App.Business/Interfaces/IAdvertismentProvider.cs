using Prison.App.Data.ServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prison.App.Business.Interfaces
{
    public interface IAdvertismentProvider
    {
        IEnumerable<Blurb> GetAds();
    }
}
