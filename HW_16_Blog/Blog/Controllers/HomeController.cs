using Blog.Interfaces;
using Blog.Models;
using Blog.Models.Pages;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Blog.Controllers
{
    //Install - Package Microsoft.AspNetCore.Identity.EntityFrameworkCore
    //Install-Package Microsoft.EntityFrameworkCore.SqlServer 

    // Практика:
    //1. + Реализовать возможность смены пароля пользователя(каждый пользователь может менять собственный пароль, указав старый).
    //2. Дополнительно: реализовать возможность восстановления пароля.


    public class HomeController : Controller
    {
        private readonly ICategory _categories;
        private readonly IPublication _publications;
        private readonly IWebHostEnvironment _appEnvironment;

        private readonly UserManager<User> _userManager;
        private readonly ISubscription _subscription;

        public HomeController(ICategory categories, IPublication publications, IWebHostEnvironment appEnvironment, UserManager<User> userManager, ISubscription subscription )
        {
            _categories = categories;
            _publications = publications;
            _appEnvironment = appEnvironment;

            _userManager = userManager;
            _subscription = subscription;
        }

        // Практика, L2
        public async Task<IActionResult> Index(QueryOptions? options, string? categoryId)
        {
            var allCategories = await _categories.GetAllCategoriesAsync();
            var allPublications = await _publications.GetAllPublicationsByCategoryWithCategories(options, categoryId);
            var randomPublications = await _publications.GetRandomPublicationsAsync();

            return View(new IndexViewModel
            {
                Categories = allCategories.ToList(),
                Publications = allPublications,
                RandomPublications = randomPublications  // Практика , Добавил рандом публикации . Но там разметка неоч в частичном представлении которое добавил...
            });
        }


        [Route("publication")]
        public async Task<IActionResult> GetPublication(string? id, string? returnUrl)
        {
            var currentPublication = await _publications.GetPublicationWithCategoriesAsync(id);
            if(currentPublication != null)
            {
                await _publications.UpdateViewsAsync(currentPublication.Id.ToString());

                //hw  достаем юзерИд публикации для  вывода имени автора в представлении публикации.
                var user = await _userManager.FindByIdAsync(currentPublication.UserId);
                string publisherName = user.Name;


                return View(new GetPublicationViewModel
                {
                    Publication = currentPublication,
                    ReturnUrl = returnUrl,
                    AuthorName = publisherName,
                });
            }
            return NotFound();
        }


        //// HW subsbcribers
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSubscription(string authorId)
        {
            var userId = _userManager.GetUserId(User);

            if (userId == authorId)
            {
                return Json(new
                {
                    success = false,
                    message = "Cannot subscribe to yoursel."
                });
            }

            var isSubscribed = await _subscription.isSubscribedAsync(userId, authorId);

            bool result;
            if (!isSubscribed)
            {
                result = await _subscription.SubscribeAsync(userId, authorId);
            }
            else
            {
                result = await _subscription.UnsubscribeAsync(userId, authorId);
            }

            return Json(new
            {
                success = result,
                isSubscribed = !isSubscribed,
                subscribersCount = await _subscription.GetSubscribersCountAsync(authorId),
                message = result ? "Subscribed." : "Failed."
            });
        }





    }
}
