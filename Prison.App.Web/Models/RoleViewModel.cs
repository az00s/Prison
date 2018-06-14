using System.ComponentModel.DataAnnotations;

namespace Prison.App.Web.Models
{
    public class RoleViewModel
    {
        [Display(Name = "ID")]
        public int RoleID { get; set; }

        [Display(Name = "Наименование")]
        [StringLength(250, ErrorMessage = "Пожалуйста введите строку от 3 до 250 символов", MinimumLength = 3)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите роль!")]
        public string RoleName { get; set; }
    }
}