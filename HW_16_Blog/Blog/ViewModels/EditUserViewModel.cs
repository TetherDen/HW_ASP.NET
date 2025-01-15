using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class EditUserViewModel
    {
        [Key]
        public string? Id {  get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        [Display(Name = "Имя")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Не указан email")]
        public string? Email { get; set; }

        public string? Phone {  get; set; }

    }
}
