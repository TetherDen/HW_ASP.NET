using Microsoft.AspNetCore.Mvc;
using Lesson_11_Part2_TvShows.Data;
using Lesson_11_Part2_TvShows.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Lesson_11_Part2_TvShows.ViewComponents
{
    public class StatisticsViewComponent : ViewComponent
    {
        private readonly Context _context;

        public StatisticsViewComponent(Context context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var genres = await _context.TvShow.Select(tv => tv.Genre).Distinct().ToListAsync();
            return View(genres);
        }
    }
}
