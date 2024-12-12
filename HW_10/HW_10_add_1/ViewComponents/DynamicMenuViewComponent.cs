using HW_10_add_1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HW_10_add_1.ViewComponents
{

    public class DynamicMenuViewComponent : ViewComponent
    {
        private List<MenuItem> MenuItems = new()
        {
            new MenuItem { Title = "Home", Url = "/", Role = "User" },
            new MenuItem { Title = "Profile", Url = "/Profile", Role = "User" },
            new MenuItem { Title = "Manage Users", Url = "/Admin/Users", Role = "Admin" },
            new MenuItem {Title = "Manage Permissions", Url = "/Admin/Permissions", Role = "Admin"}

        };


        public IViewComponentResult Invoke(string userRole)
        {
            var userMenuItems = MenuItems.Where(e => e.Role == userRole).ToList();  

            return View(userMenuItems);
        }

    }

}
