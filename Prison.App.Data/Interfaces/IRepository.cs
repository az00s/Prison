using Prison.App.Common.Entities;
using System.Collections.Generic;

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
