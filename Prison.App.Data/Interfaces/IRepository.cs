
using Prison.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prison.App.Data.Interfaces
{
    public interface IRepository
    {
         ICollection<Detainee> Detainees { get;  }

         ICollection<Detention> Detentions { get; }

         ICollection<Employee> Employees { get; }
        void ErrorMethod();
    }
}
