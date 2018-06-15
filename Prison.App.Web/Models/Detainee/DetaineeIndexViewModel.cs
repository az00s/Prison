using Prison.App.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prison.App.Web.Models
{
    public class DetaineeIndexViewModel
    {
        public int DetaineeID { get; set; }

        [Display(Name ="Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        [Display(Name = "Дата рождения")]
        public string BirstDate { get; set; }

        [Display(Name = "Фото")]
        public string ImagePath { get; set; }


    }
}