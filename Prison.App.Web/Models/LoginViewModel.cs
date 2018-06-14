using System.ComponentModel.DataAnnotations;

namespace Prison.App.Web.Models
{
    public class LoginViewModel
    {
        [StringLength(35, ErrorMessage = "Неверное имя пользователя! Пожалуйста введите имя длиной от 5 до 35 символов", MinimumLength = 5)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите имя пользователя!")]
        [Display(Name ="Имя")]
        public string UserName { get; set; }

        
        [StringLength(35, ErrorMessage = "Неверный пароль! Пожалуйста введите пароль от 5 до 35 символов",MinimumLength =5)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите пароль!")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}