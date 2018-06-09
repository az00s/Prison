using System.ComponentModel.DataAnnotations;

namespace Prison.App.Web.Models
{
    public class PositionViewModel
    {
        [Display(Name ="ID")]
        public int PositionID { get; set; }

        [Display(Name = "Наименование")]
        [StringLength(150, ErrorMessage = "Пожалуйста введите строку от 3 до 150 символов", MinimumLength = 3)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите должность!")]
        public string PositionName { get; set; }
    }
}