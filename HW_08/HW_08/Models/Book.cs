using System.ComponentModel.DataAnnotations;

namespace HW_08.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Author { get; set; }

        [Required]
        public string? Genre { get; set; }

        [Required]
        public string? Year { get; set; }
    }
}
