using HW_08.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HW_08.Controllers
{
    public class HomeController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }


    }
}
