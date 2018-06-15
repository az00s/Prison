using Prison.App.Common.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prison.App.Web.Models
{
    public class EmployeeEditViewModel
    {
        public int EmployeeID { get; set; }

        [StringLength(35, ErrorMessage = "Пожалуйста введите строку от 2 до 35 символов", MinimumLength = 2)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите фамилию!")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [StringLength(35, ErrorMessage = "Пожалуйста введите строку от 3 до 35 символов", MinimumLength = 3)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите имя!")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [StringLength(40, ErrorMessage = "Пожалуйста введите строку от 2 до 40 символов", MinimumLength = 2)]
        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }


        [Display(Name = "Должность")]
        public IEnumerable<Position> Positions { get; set; }

        [Required(ErrorMessage = "Выберите должность!")]
        public int PositionID { get; set; }


    }
}