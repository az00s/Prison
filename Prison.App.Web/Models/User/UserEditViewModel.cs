using Prison.App.Common.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prison.App.Web.Models.User
{
    public class UserEditViewModel
    {
        [Display(Name = "ID")]
        public int UserID { get; set; }

        [Display(Name = "Имя")]
        public string UserName { get; set; }

        [Display(Name = "Электронная почта")]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Роли")]
        public IEnumerable<Role> AllRoles { get; set; }

        [Display(Name = "Роль пользователя")]
        public Role[] Roles { get; set; }
    }
}