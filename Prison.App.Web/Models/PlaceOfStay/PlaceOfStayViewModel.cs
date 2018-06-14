using System.ComponentModel.DataAnnotations;

namespace Prison.App.Web.Models
{
    public class PlaceOfStayViewModel
    {
        public int PlaceID { get; set; }

        [StringLength(300, ErrorMessage = "Пожалуйста введите строку длиной от 5 до 300 символов", MinimumLength = 5)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите адрес!")]
        [Display(Name ="Адрес")]
        public string Address { get; set; }
    }
}