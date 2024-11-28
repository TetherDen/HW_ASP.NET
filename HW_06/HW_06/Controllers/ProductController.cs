using HW_06.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HW_06.Controllers
{
	public class ProductController : Controller
	{
        private readonly ApplicationDbContext db;

        public ProductController(ApplicationDbContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index([FromServices] ApplicationDbContext db)
	{
		return Json(await db.Products.ToListAsync());
	}

	[HttpGet]
	public IActionResult Create()
	{
		return View();
	}

        [HttpPost]
        public IActionResult Create(Product product)
        {
            db.Products.Add(product);
            db.SaveChangesAsync();
            return Ok(product);
        }


        // localhost:7196/product/search?keyword=prod
        [HttpGet]
        public async Task<IActionResult> Search(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                var products = await db.Products.Where(e => e.Name.Contains(keyword)).ToListAsync();
                return Json(products);
            }
            return BadRequest("Invalid request parameters");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (id >0)
            {
                var product = await db.Products.FirstOrDefaultAsync(e => e.Id == id);
                return Json(product);
            }
            return BadRequest("Invalid request parameters");
        }

        // Get Тест ->
        // localhost:7196/product/delete?id=11
        [HttpPost]
        public async Task<IActionResult> Delete(int id)         // В POST запросе черзе Postman всегда приходит int id = 0 почемуто.
        {
            var product = await db.Products.FirstOrDefaultAsync(e => e.Id == id);

            if (product == null)
            {
                return NotFound("Product not found");
            }

            db.Products.Remove(product);
            db.SaveChangesAsync();

            return Json(product);
        }



    }
}
