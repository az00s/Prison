using Prison.App.Common.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prison.App.Web.Models
{
    public class EmployeeEditViewModel
    {
        [Display(Name ="")]
        public int EmployeeID { get; set; }
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }
        [Display(Name = "Должность")]
        public IEnumerable<Position> Positions { get; set; }

        public int PositionID { get; set; }


    }
}