using HW_10.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HW_10.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

    }
}
