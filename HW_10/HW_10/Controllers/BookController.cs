using HW_10.Data;
using HW_10.Models;
using HW_10.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HW_10.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationContext _db;
        public BookController(ApplicationContext context)
        {
            _db = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _db.Books.Include(b => b.Comments).ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var book = await _db.Books.Include(b => b.Comments).FirstOrDefaultAsync(e => e.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            var model = new BookDetailsViewModel
            {
                Book = book,
                NewComment = new Comment { BookId = id }
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(BookDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Book = await _db.Books.Include(e => e.Comments).FirstOrDefaultAsync(b => b.Id == model.NewComment.BookId);
                return View("Details", model);
            }

            _db.Comments.Add(model.NewComment);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = model.NewComment.BookId });
        }




    }
}
