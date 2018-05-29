using Prison.App.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Prison.App.Web.Models.User
{
    public class UserCreateViewModel
    {
        public int UserID { get; set; }

        [Display(Name = "Фамилия сотрудника")]
        public IEnumerable<Employee> UserName { get; set; }

        [Display(Name = "Электронная почта")]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Роль")]
        public Role[] Roles { get; set; }
    }
}