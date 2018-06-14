using System;
using System.ComponentModel.DataAnnotations;

namespace Prison.App.Web.Models
{
    public class DetentionListViewModel
    {
        [Display(Name = "Номер задержания")]
        public int DetentionID { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [Display(Name = "Дата задержания")]
        public DateTime DetentionDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [Display(Name = "Дата доставления")]
        public DateTime DeliveryDate { get; set; } 

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [Display(Name = "Дата освобождения")]
        public DateTime ReleasеDate { get; set; } 

        [Display(Name = "Место содержания")]
        public string PlaceOfDetention { get; set; }

        [Display(Name = "Начислено за содержание")]
        public decimal AmountForStaying { get; set; }

        [Display(Name = "Оплачено за содержание")]
        public decimal PaidAmount { get; set; }

        [Display(Name = "Доставлен сотрудником")]
        public int DeliveredByWhomID { get; set; }

        [Display(Name = "Освобожден сотрудником")]
        public int ReleasedByWhomID { get; set; }

        [Display(Name = "Задержан сотрудником")]
        public string Employee { get; set; }

        [Display(Name = "Место содержания")]
        public int PlaceID { get; set; }

    }
}