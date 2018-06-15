using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Web.Models
{
    public class MaritalDropDownViewModel
    {
        public IEnumerable<MaritalStatus> Statuses { get; set; }

        public int SelectedID { get; set; }
    }
}