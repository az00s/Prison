using Prison.App.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prison.App.Web.Models
{
    public class DetentionReleaseDetaineeViewModel
    {
        public int DetentionID { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата освобождения")]
        public DateTime ReleasеDate { get; set; } = DateTime.Today;

        [Display(Name = "Начислено за содержание")]
        public decimal AmountForStaying { get; set; }

        [Display(Name = "Оплачено за содержание")]
        public decimal PaidAmount { get; set; }

        [Required(ErrorMessage = "Выберите сотрудника!")]
        [Display(Name = "Освобожден сотрудником")]
        public int ReleasedByWhomID { get; set; }

        [Display(Name = "Сотрудник")]
        public IEnumerable<Employee> Employees { get; set; }
    }
}