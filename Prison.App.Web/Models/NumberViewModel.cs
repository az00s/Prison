using Prison.App.Common.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prison.App.Web.Models
{
    public class NumberViewModel
    {
        [Display(Name = "ID")]
        public int NumberID { get; set; }

        [Display(Name = "Номер телефона")]
        [StringLength(50, ErrorMessage = "Пожалуйста введите строку от 5 до 50 символов", MinimumLength = 5)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите номер!")]
        public string Number { get; set; }

        public int DetaineeID { get; set; }

        [Display(Name = "Задержанный")]
        public string DetaineeLastname { get; set; }

        [Display(Name = "Задержанный")]
        public IEnumerable<Detainee> Detainees { get; set; }
    }
}
