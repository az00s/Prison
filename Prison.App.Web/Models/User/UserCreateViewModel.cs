using Prison.App.Common.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prison.App.Web.Models.User
{
    public class UserCreateViewModel
    {
        public int UserID { get; set; }

        [StringLength(35, ErrorMessage = "Пожалуйста введите строку длиной от 2 до 35 символов", MinimumLength = 2)]
        [Display(Name = "Фамилия сотрудника")]
        public IEnumerable<Employee> UserName { get; set; }

        [StringLength(250, ErrorMessage = "Пожалуйста введите строку длиной от 8 до 250 символов", MinimumLength = 8)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите электронную почту!")]
        [EmailAddress]
        [Display(Name = "Электронная почта")]
        public string Email { get; set; }

        [StringLength(35, ErrorMessage = "Пожалуйста введите строку длиной от 5 до 35 символов", MinimumLength = 5)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите пароль!")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Роль")]
        public Role[] Roles { get; set; }
    }
}