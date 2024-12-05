using HW_08.Models;
using HW_08.Service;
using HW_08.VIewModel;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text;

namespace HW_08.Controllers
{
    public class BookController : Controller
    {
        private readonly BookService _bookService;

        public BookController (BookService bookService)
        {
            _bookService = bookService;
        }

        private List<BookViewModel> GetBooks()
        {
            List<BookViewModel> bookViewModels = _bookService.GetBooks().Select(e => new BookViewModel 
            { 
                Title = e.Title, 
                Author =  e.Author ,
                Genre = e.Genre ,
                Year = e.Year ,
            }).ToList();
            bookViewModels.Insert(0, new BookViewModel { Title = "Title", Author = "Author", Genre = "Genre", Year = "Year" });

            return bookViewModels;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(GetBooks());
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(BookViewModel book)
        {
            if(ModelState.IsValid)
            {
                Book newBook = new Book
                {
                    Id = _bookService.GetBooks().Max(b => b.Id) + 1,
                    Title = book.Title,
                    Author = book.Author,
                    Genre = book.Genre,
                    Year = book.Year                   
                };
                _bookService.AddBook(newBook);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }


    }
}
