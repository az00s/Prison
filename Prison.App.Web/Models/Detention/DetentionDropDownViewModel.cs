using System.ComponentModel.DataAnnotations;

namespace Prison.App.Web.Models
{
    public class DetentionDropDownViewModel
    {
        [Required(ErrorMessage = "Выберите задержание!")]
        [Display(Name = "Номер задержания")]
        public int DetentionID { get; set; }

        [Display(Name = "Задержание")]
        public string DetentionHeader { get; set; }
    }
}