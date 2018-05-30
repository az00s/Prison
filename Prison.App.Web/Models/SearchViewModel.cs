using System.ComponentModel.DataAnnotations;

namespace Prison.App.Web.Models
{
    public class SearchViewModel
    {
        [Display(Name ="Фамилия")]
        public string  LastName { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Отчество")]
        public string Middlename { get; set; }

        [Display(Name = "Адрес прописки")]
        public string ResidenceAddress { get; set; }

        [Display(Name = "Дата задержания")]
        public string DetentionDate { get; set; }

    }
}