using HW_09.Models;
using HW_09.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace HW_09.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductService _productService;
        public HomeController(ProductService productService ) 
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_productService.GetProducts());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
			Product product = _productService.GetProductById(id);
			ViewBag.Categories = new SelectList(_productService.GetCategories());
            return View(product);
        }


        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.EditProduct(product);
                return RedirectToAction("Index");
            }
            return View("Index");
        }


		[HttpGet]
		public IActionResult CosmicHeader()
		{

			return View();
		}





	}
}
