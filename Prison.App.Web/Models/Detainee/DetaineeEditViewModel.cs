using Prison.App.Common.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prison.App.Web.Models
{
    public class DetaineeEditViewModel
    {
        public int DetaineeID { get; set; }

        [StringLength(35, ErrorMessage = "Пожалуйста введите строку длиной от 2 до 35 символов", MinimumLength = 2)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите фамилию!")]
        [Display(Name ="Фамилия")]
        public string LastName { get; set; }

        [StringLength(35, ErrorMessage = "Пожалуйста введите строку длиной от 2 до 35 символов", MinimumLength = 2)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите имя!")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [StringLength(40, ErrorMessage = "Пожалуйста введите строку длиной от 2 до 40 символов", MinimumLength = 2)]
        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите дату!")]
        [DisplayFormat(DataFormatString ="{0:dd.MM.yyyy}", ApplyFormatInEditMode=true)]
        [Display(Name = "Дата рождения")]
        public string BirstDate { get; set; } 

        [Display(Name = "Фото")]
        public string ImagePath { get; set; }

        [Display(Name = "Семейное положение")]
        public IEnumerable<MaritalStatus> MaritalStatus { get; set; }

        [Required(ErrorMessage = "Выберите семейное положение!")]
        public int MaritalStatusID { get; set; }

        [StringLength(400, ErrorMessage = "Пожалуйста введите строку длиной от 3 до 400 символов", MinimumLength = 3)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите место работы!")]
        [Display(Name = "Место работы")]
        public string WorkPlace { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите телефон!")]
        [Display(Name = "Телефон")]
        public IEnumerable<string> PhoneNumbers { get; set; } = new List<string> { " 325"};

        [StringLength(400, ErrorMessage = "Пожалуйста введите строку длиной от 3 до 400 символов", MinimumLength = 3)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите место жительства!")]
        [Display(Name = "Место жительства")]
        public string ResidenceAddress { get; set; }

        [StringLength(500, ErrorMessage = "Пожалуйста введите строку длиной от 2 до 500 символов", MinimumLength = 2)]
        [Display(Name = "Примечание")]
        public string AdditionalData { get; set; }

        [Display(Name = "Задержания")]
        public IEnumerable<Detention> Detentions { get; set; }

    }
}