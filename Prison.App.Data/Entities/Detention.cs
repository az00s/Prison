using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prison.App.Business
{
    public class Detention
    {
        public DateTime DetentionDate { get; set; }

        public DateTime DateOfDelivery { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string PlaceOfDetention { get; set; }

        public decimal AmountForStaying { get; set; }

        public decimal PaidAmount { get; set; }

        public Employee DeliveredByWhom { get; set; }

        public Employee ReleasedByWhom { get; set; }

        public Employee DetainedByWhom { get; set; }

        public Address AddressOfStay { get; set; }


    }
}
