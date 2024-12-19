using HW_12.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HW_12.Controllers
{
    public class HomeController : Controller
    {
        private List<News> _news = new()
        {
            new News { Title = "Title1", Text = "\"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.4"},
            new News { Title = "Title2", Text = "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."},
            new News { Title = "Title3", Text = "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\""},
            new News { Title = "Title4", Text = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed qu"},
        };

        public IActionResult Index()
        {
            string bg = HttpContext.Request.Cookies["bgColor"] ?? "white";
            ViewData["bgColor"] = bg;

            return View(_news);
        }

        [HttpPost]
        public IActionResult SaveSettings(string backgroundColor)
        {
            if(!string.IsNullOrEmpty(backgroundColor))
            {
                CookieOptions options = new CookieOptions()
                {
                    Expires = DateTimeOffset.Now.AddDays(7),
                    Secure = true,
                    HttpOnly = true,
                };
                HttpContext.Response.Cookies.Append("bgColor", backgroundColor, options);

            }

            return RedirectToAction(nameof(Index));
        }
    }
}

