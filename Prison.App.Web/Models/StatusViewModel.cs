using System.ComponentModel.DataAnnotations;

namespace Prison.App.Web.Models
{
    public class StatusViewModel
    {
        public int StatusID { get; set; }

        [Display(Name = "Семейное положение")]
        [StringLength(50, ErrorMessage = "Пожалуйста введите строку от 3 до 50 символов", MinimumLength = 3)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите семейное положение!")]
        public string StatusName { get; set; }
    }
}