

using System.ComponentModel.DataAnnotations;

namespace HW_17_Mini_Blog.ViewModel
{
    public class ViewModelCreatePublication
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }
    }
}
