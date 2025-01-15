using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class PublicationViewModel
    {
        public Guid? Id { get; set; }
        [Display(Name = "Заголовок")]
        [Required(ErrorMessage ="Не указан заголовок публикации")]
        public string? Title { get; set; }

        [Display(Name = "Содержимое")]
        [Required(ErrorMessage = "Не указан содержимое публикации")]
        public string? Description { get; set; }

        [Display(Name ="Категории")]
        public IEnumerable<SelectListItem>? SelectListCategories { get; set; }

        [Display(Name = "изображение")]
        public IFormFile? File { get; set; }

        public string? Image {  get; set; }

        public string? ImageFullName { get; set; }

        [Display(Name = "Seo описание( до 155 символов)")]
        [Required(ErrorMessage ="Не указано Seo описание")]
        [MaxLength(155, ErrorMessage ="Укажите до 155 символов")]
        public string? SeoDescription { get; set; }

        [Display(Name = "Seo ключевые слова")]
        public string? Keywords { get; set; }
    }
}
