using HW_17_Mini_Blog.Data;
using HW_17_Mini_Blog.Interfaces;
using HW_17_Mini_Blog.Models;
using HW_17_Mini_Blog.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HW_17_Mini_Blog.Controllers
{
    public class PublicationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPublication _publications;
        public PublicationController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IPublication publications)
        {
            _context = context;
            _userManager = userManager;
            _publications = publications;
        }

        public async Task<IActionResult> Index()
        {
            var publications = await  _publications.GetAllPublicationsAsync();
            return View(publications);          
        }


        [HttpGet]
        [Authorize]
        public IActionResult CreatePublication()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> CreatePublication(ViewModelCreatePublication model)
        {
            if (ModelState.IsValid)
            {
                var publication = new Publication()
                {
                    Title = model.Title,
                    Description = model.Description,
                    Content = model.Content,
                    CreatedAt = DateTime.UtcNow,
                    AuthorId = _userManager.GetUserId(User),
                };
                await _publications.AddPublicationAsync(publication);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetPublication(Guid id)
        {
            var publication = await _publications.GetPublicationByIdAsync(id);
            if (publication == null)
            {
                return NotFound();
            }
            return View(publication);
        }

    }
}
