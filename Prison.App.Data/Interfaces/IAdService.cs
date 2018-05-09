using Prison.App.Data.ServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prison.App.Data.Interfaces
{
    public interface IAdService
    {
        IEnumerable<Blurb> GetRandomElementsFromRep(int numOfElements);
    }
}
