using System.ComponentModel.DataAnnotations;

namespace HW_10.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Comment text is required")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Text Length from 5 to 100 characters")]
        public string Text { get; set; }

        public int BookId { get; set; }

        public Book? Book { get; set; }
    }
}
