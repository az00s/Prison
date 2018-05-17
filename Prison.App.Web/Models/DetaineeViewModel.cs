using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prison.App.Web.Models
{
    public class DetaineeViewModel
    {
        public int DetaineeID { get; set; }
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public DateTime BirstDate { get; set; }

        public string Image { get; set; }



    }
}