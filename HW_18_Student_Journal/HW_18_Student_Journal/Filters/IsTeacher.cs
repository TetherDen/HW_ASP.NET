


using HW_18_Student_Journal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace HW_18_Student_Journal.Filters
{
    public class IsTeacherActionFilter : Attribute,IAsyncActionFilter 
    {
        private readonly UserManager<User> _userManager;
        private readonly TeachersEmails _teachersEmails;
        public IsTeacherActionFilter(UserManager<User> userManager, IOptions<TeachersEmails> teacherEmails)
        {
            _userManager = userManager;
            _teachersEmails = teacherEmails.Value;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var user = context.HttpContext.User;
            if (user == null || !await IsUserAllowedAsync(user))
            {
                //context.Result = new RedirectResult("/Home");
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                // TODO сделать кастомную страница для   access not allowed ?
                return;
            }

            await next();
        }

        private async Task<bool> IsUserAllowedAsync(ClaimsPrincipal user)
        {
            var appUser = await _userManager.GetUserAsync(user);
            return _teachersEmails.TeacherEmailsList.Contains(appUser.Email);
        }
    }
}
