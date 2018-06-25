using Prison.App.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prison.App.Web.Models
{
    public class DetentionCreateViewModel
    {
        public int DetaineeID { get; set; }

        public int DetentionID { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy H:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата задержания")]
        public DateTime DetentionDate { get; set; } = DateTime.Now;

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата доставления")]
        public DateTime DeliveryDate { get; set; } = DateTime.Today;

        [Display(Name = "Место содержания")]
        public string PlaceOfDetention { get; set; }

        [Required(ErrorMessage = "Выберите сотрудника!")]
        [Display(Name = "Доставлен сотрудником")]
        public int DeliveredByWhomID { get; set; }


        [Required(ErrorMessage = "Выберите сотрудника!")]
        [Display(Name = "Задержан сотрудником")]
        public int DetainedByWhomID { get; set; }

        [Required(ErrorMessage = "Выберите место содержания!")]
        [Display(Name = "Место содержания")]
        public int PlaceID { get; set; }

        [Display(Name = "Место содержания")]
        public IEnumerable<PlaceOfStay> Places { get; set; }

        [Display(Name = "Сотрудник")]
        public IEnumerable<Employee> Employees { get; set; }

    }
}