using System;
using System.ComponentModel.DataAnnotations;

namespace Prison.App.Web.Models
{
    public class DetentionDetailsViewModel
    {
        [Display(Name ="Номер задержания")]
        public int DetentionID { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy H:mm}")]
        [Display(Name = "Дата задержания")]
        public DateTime DetentionDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [Display(Name = "Дата доставления")]
        public DateTime DeliveryDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [Display(Name = "Дата освобождения")]
        public string ReleasеDate { get; set; } 

        [Display(Name = "Начислено за содержание")]
        public decimal AmountForStaying { get; set; }

        [Display(Name = "Оплачено за содержание")]
        public decimal PaidAmount { get; set; }

        [Display(Name = "Доставлен сотрудником")]
        public string DeliveredByWhom { get; set; }

        [Display(Name = "Освобожден сотрудником")]
        public string ReleasedByWhom { get; set; }

        [Display(Name = "Задержан сотрудником")]
        public string DetainedByWhom { get; set; }

        [Display(Name = "Место содержания")]
        public string Place { get; set; }

    }
}