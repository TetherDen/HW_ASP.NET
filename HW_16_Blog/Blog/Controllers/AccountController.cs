using Blog.Interfaces;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Route("login")]
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel {  ReturnUrl = returnUrl });
        }


        [Route("login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if(result.Succeeded)
                {
                    // проверяем принадлежит ли URL приложению
                    // PS в LoginVIewModel есть поле ReturnUrl - Представим допустим у когото в закладках леит ссылка на наш сайт, там какоето место на сайте, 
                    // Пользователь стучится по адрессу но он не авторизован, его перекидывает на авторизацию и после авторизации за счет того что в етом поле хранится ссылка мы можем редирект сделать на тот адресс куда он изначально хотел...
                    if(!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аунтефикационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }



        [Route("register")]
        [HttpGet]
        public async Task<IActionResult> Register([FromServices] IMembership membership, string? code)
        {
            if(!User.Identity.IsAuthenticated || code != null)
            {
                if(await membership.ExistsMembershipByCodeAsync(code))
                {
                    if(await membership.EnableCodeMembershipByCodeAsync(code))
                    {
                        return View(new RegisterUserViewModel { Code = code });    
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }


        [Route("register")]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register([FromServices] IMembership membership, RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userCheck = await _userManager.FindByEmailAsync(model.Email);
                if (userCheck != null)
                {
                    ModelState.AddModelError("Email", "Такой email адрес уже есть!");
                    return View(model);
                }
                User user = new User { Email = model.Email, UserName = model.Email, Name = model.Name, PhoneNumber = model.Phone };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //Установки роли. Сама роль находится в таблице AspNetRoles
                    //если таблица пустая, получим ошибку. Обязательно заполняем роли!
                    await _userManager.AddToRoleAsync(user, "Editor");
                    //используем приглашение
                    await membership.DisableMembershipCodeAsync(model.Code);
                    //установка куки
                    await _signInManager.SignInAsync(user, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }


        // Практика смена пароля

        [Authorize] 
        [Route("change-password")]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }


        [Authorize]
        [Route("change-password")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    await _signInManager.RefreshSignInAsync(user);
                    TempData["SuccessMessage"] = "Пароль успешно изменён.";
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }


    }
}
