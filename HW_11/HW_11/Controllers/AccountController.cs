using Microsoft.AspNetCore.Mvc;
using HW_11.Models;
using HW_11.ViewModels;

namespace HW_11.Controllers
{
    public class AccountController : Controller
    {
        private List<User> _users = new()
        {
            new User { UserName = "Admin"},
            new User { UserName = "admin"},
        };

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
				_users.Add(new User { UserName = registerModel.UserName });
                return Content(registerModel.ToString());
			}
            return View(registerModel);
        }

        // Проверка для Аттрибута [Remote] в RegVM
        public IActionResult IsUsernameAvailable(string username)
        {
            if(_users.Any(e=> e.UserName == username))
            {
                ModelState.AddModelError("UserName", "Username is already taken");
                return Json(false);
            }
            else
            {
                return Json(true);
            }
        }

    }
}
