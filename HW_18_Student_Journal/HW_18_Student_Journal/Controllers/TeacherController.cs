using HW_18_Student_Journal.Filters;
using HW_18_Student_Journal.Interface;
using HW_18_Student_Journal.Models;
using HW_18_Student_Journal.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HW_18_Student_Journal.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(IsTeacherActionFilter))]
    [AutoValidateAntiforgeryToken]
    public class TeacherController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly TeachersEmails _teachersEmails;
        private readonly IGroup _groupRepository;

        public TeacherController(UserManager<User> userManager, IOptions<TeachersEmails> teacherEmails, IGroup groupRepository)
        {
            _userManager = userManager;
            _teachersEmails = teacherEmails.Value;
            _groupRepository = groupRepository;
        }


        //  В итоге все действия переехали в контроллер Group, и CreateGroup надо будет вынести туда тоже...
        public async Task<IActionResult> CreateGroup()
        {
            return View(new CreateGroupViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup(CreateGroupViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _groupRepository.IsGroupExistsAsync(model.Name))
                {
                    ModelState.AddModelError("Name", "A group with this name already exists");
                    return View(model);
                }

                var group = new Group
                {
                    Name = model.Name,
                };
                await _groupRepository.AddGroupAsync(group);
                return RedirectToAction("ManageStudents", "Group", new { groupId = group.Id });
            }
            return View(model);
        }




    }
}
