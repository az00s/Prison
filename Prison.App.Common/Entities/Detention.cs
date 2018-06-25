using System;

namespace Prison.App.Common.Entities
{
    public class Detention
    {
        public int DetentionID { get; set; }

        public DateTime DetentionDate { get; set; }

        public DateTime DeliveryDate { get; set; }

        public int DeliveredByWhomID { get; set; }

        public int DetainedByWhomID { get; set; }

        public int PlaceID { get; set; }
    }
}
