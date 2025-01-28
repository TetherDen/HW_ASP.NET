using System.ComponentModel.DataAnnotations;

namespace HW_19_TODO_LIST_RazorPages.ViewModels
{
    public class TaskItemViewModel
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(50, ErrorMessage = "Title must be less than 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(250, ErrorMessage = "Description must be less than 250 characters.")]
        public string Description { get; set; }
    }
}
