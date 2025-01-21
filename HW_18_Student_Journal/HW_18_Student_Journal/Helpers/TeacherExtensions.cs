using HW_18_Student_Journal.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HW_18_Student_Journal.Helpers
{
    // через этот екстеншен в _layout  првоеряю выводит ли меню преподотвалея текущему юзеру
    public static class TeacherExtensions
    {
        public static async Task<bool> IsTeacherAsync(this ClaimsPrincipal user, UserManager<User> userManager, TeachersEmails teachersEmails)
        {
            if (user == null || !user.Identity.IsAuthenticated)
                return false;

            var appUser = await userManager.GetUserAsync(user);
            return teachersEmails.TeacherEmailsList.Contains(appUser?.Email);
        }
    }
}
