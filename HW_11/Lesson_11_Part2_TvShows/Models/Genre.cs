using System.ComponentModel.DataAnnotations;

namespace Lesson_11_Part2_TvShows.Models
{
    public enum Genre
    {
        Drama,
        Comedy,
        Romance,
        [Display(Name ="Romantic Comedy")]
        RomCom,
        Crime,
        Mysteria
    }
}
