using Prison.App.Common.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prison.App.Web.Models
{
    public class DetaineeDetailsViewModel
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

        [Display(Name = "Семейное положение")]
        public string MaritalStatus { get; set; }

        [Display(Name = "Место работы")]
        public string WorkPlace { get; set; }

        [Display(Name = "Телефон")]
        public ICollection<string> PhoneNumbers { get; set; }

        [Display(Name = "Место жительства")]
        public string ResidenceAddress { get; set; }

        [Display(Name = "Примечание")]
        public string AdditionalData { get; set; }

        [Display(Name = "Задержания")]
        public IEnumerable<Detention> Detentions { get; set; }

    }
}