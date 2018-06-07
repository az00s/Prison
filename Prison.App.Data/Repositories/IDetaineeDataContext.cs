using Prison.App.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prison.App.Data.Repositories
{
    public interface IDetaineeDataContext
    {
        IEnumerable<Detainee> GetAll();
    }
}
