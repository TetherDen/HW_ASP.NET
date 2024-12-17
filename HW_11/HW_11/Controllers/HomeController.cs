using HW_11.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HW_11.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


    }
}
