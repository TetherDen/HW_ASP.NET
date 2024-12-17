using Lesson_11_Part2_TvShows.Data;
using Lesson_11_Part2_TvShows.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lesson_11_Part2_TvShows.ViewComponents
{
    public class AddRandomMooviesViewComponent : ViewComponent
    {
        private readonly Context _context;
        
        public AddRandomMooviesViewComponent(Context context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int currentMoovieId, Genre genre)
        {
            var randomMoovies = await _context.TvShow
                .Where(e => e.Genre == genre && e.Id != currentMoovieId)
                .OrderBy(r => EF.Functions.Random()) // генерация случайного порядка
                .Take(5)
                .ToListAsync();

            return View(randomMoovies);
        }

    }
}
