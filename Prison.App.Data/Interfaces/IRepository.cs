
using Prison.Common;
using Prison.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prison.App.Data.Interfaces
{
    public interface IRepository
    {
        ICollection<Detainee> Detainees { get; set; }

        ICollection<Detention> Detentions { get; set; }

        ICollection<Employee> Employees { get; set; }

        ICollection<PlaceOfDetention> PlacesOfDetention { get; set; }

        void ErrorMethod();
    }
}
