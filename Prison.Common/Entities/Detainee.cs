using Prison.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prison.Common
{
    public class Detainee
    {
        public int DetaineeID { get; set; }
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Patronymic { get; set; }

        public DateTime BirstDate { get; set; }

        public MaritalStatus MaritalStatus { get; set; }

        public WorkPlace WorkPlace { get; set; }

        public ICollection<string> PhoneNumbers { get; set; }

        public string Photo { get; set; }

        public string ResidenceAddress { get; set; }

        public string AdditionalInfo { get; set; }

        public ICollection<Detention> Detentions { get; set; }
    }
}
