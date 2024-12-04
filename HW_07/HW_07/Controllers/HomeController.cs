using HW_07.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HW_07.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ExampleOne()
        {
            return View();
        }

        public IActionResult ExampleTwo()
        {
            return View();
        }

    }
}
