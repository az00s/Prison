using System.ComponentModel.DataAnnotations;

namespace Prison.App.Web.Models
{
    public class SearchViewModel
    {
        [StringLength(maximumLength: 35, ErrorMessage = "Неверная фамилия! Пожалуйста введите фамилию длиной до 35 символов")]
        [Display(Name ="Фамилия")]
        public string  LastName { get; set; }

        [StringLength(maximumLength: 35, ErrorMessage = "Неверное имя! Пожалуйста введите имя длиной до 35 символов")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [StringLength(maximumLength: 40, ErrorMessage = "Неверное Отчество! Пожалуйста введите Отчество длиной до 40 символов")]
        [Display(Name = "Отчество")]
        public string Middlename { get; set; }

        [StringLength(maximumLength: 300, ErrorMessage = "Неверный Адрес! Пожалуйста введите Адрес длиной до 300 символов")]
        [Display(Name = "Адрес прописки")]
        public string ResidenceAddress { get; set; }

        [StringLength(maximumLength: 10, ErrorMessage = "Неверная Дата! Пожалуйста введите Адрес дату в формате: дд.мм.гггг",MinimumLength =10)]
        [Display(Name = "Дата задержания")]
        public string DetentionDate { get; set; }

    }
}