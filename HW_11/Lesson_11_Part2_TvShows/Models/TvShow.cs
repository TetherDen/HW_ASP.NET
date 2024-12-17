using System.ComponentModel.DataAnnotations;

namespace Lesson_11_Part2_TvShows.Models
{
    public class TvShow
    {
        public int Id { get; set; }
        [Required]
        public string Titlle { get; set; }
        [Required]
        public decimal Rating { get; set; }
        [Url]
        public string ImdUrl { get; set; }
        public Genre Genre { get; set; }
        public string? Poster { get; set; }
    }
}
