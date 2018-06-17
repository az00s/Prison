using System.ComponentModel.DataAnnotations;

namespace Prison.App.Web.Models
{
    public class EmployeeIndexViewModel
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
        public string Position { get; set; }
    }
}