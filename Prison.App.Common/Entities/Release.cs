using System;

namespace Prison.App.Common.Entities
{
    public class Release
    {
        public int ReleaseID { get; set; }

        public int DetentionID { get; set; }

        public int DetaineeID { get; set; }

        public DateTime ReleasеDate { get; set; } 

        public decimal AmountForStaying { get; set; }

        public decimal PaidAmount { get; set; }

        public int ReleasedByWhomID { get; set; }

    }
}
