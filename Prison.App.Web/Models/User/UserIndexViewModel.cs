using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Prison.App.Web.Models.User
{
    public class UserIndexViewModel
    {
        [Display(Name = "ID")]
        public int UserID { get; set; }

        [Display(Name = "Имя")]
        public string UserName { get; set; }

        [Display(Name ="Электронная почта")]
        public string Email { get; set; }
    }
}