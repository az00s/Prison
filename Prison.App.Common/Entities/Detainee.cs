using System;
using System.Collections.Generic;

namespace Prison.App.Common.Entities
{
    public class Detainee
    {
        public int DetaineeID { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public DateTime BirstDate { get; set; }

        public int MaritalStatusID { get; set; }

        public string WorkPlace { get; set; }

        public ICollection<string> PhoneNumbers { get; set; }

        public string ImagePath { get; set; }

        public string ResidenceAddress { get; set; }

        public string AdditionalData { get; set; }

        public IEnumerable<Detention> Detentions { get; set; }
    }
}
