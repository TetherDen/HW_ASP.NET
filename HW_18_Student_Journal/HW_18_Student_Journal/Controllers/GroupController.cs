using HW_18_Student_Journal.Filters;
using HW_18_Student_Journal.Interface;
using HW_18_Student_Journal.Models;
using HW_18_Student_Journal.ViewModel;
using HW_18_Student_Journal.ViewModel.HW_18_Student_Journal.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HW_18_Student_Journal.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    [ServiceFilter(typeof(IsTeacherActionFilter))]
    public class GroupController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly TeachersEmails _teachersEmails;
        private readonly IGroup _groupRepository;
        private readonly ISubject _subject;
        public GroupController(IGroup groupRepository, UserManager<User> userManager, IOptions<TeachersEmails> teacherEmails, ISubject subject)
        {
            _userManager = userManager;
            _teachersEmails = teacherEmails.Value;
            _groupRepository = groupRepository;
            _subject = subject;
        }


        public async Task<IActionResult> Index()
        {
            var groups = await _groupRepository.GetAllGroupsAsync();
            return View(groups);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var group = await _groupRepository.GetGroupByIdAsync(id);
            if (group == null)
            {
                TempData["ErrorMessage"] = "Group not found.";
                return RedirectToAction("Index");
            }

            await _groupRepository.DeleteGroupAsync(id);

            TempData["SuccessMessage"] = $"Group: '{group.Name}' successfully deleted.";
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> ManageStudents(int groupId)
        {
            var group = await _groupRepository.GetGroupByIdAsync(groupId);
            if (group == null)
            {
                TempData["ErrorMessage"] = "Group not found.";
                return RedirectToAction("Index");
            }

            var availableStudents = await _groupRepository.GetStudentsWithoutGroupsAsync(groupId);
            var viewModel = new ManageStudentsViewModel
            {
                GroupId = group.Id,
                GroupName = group.Name,
                StudentsInGroup = group.GroupMembers.Select(gm => gm.Student).ToList(),
                AvailableStudents = availableStudents
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudents(int groupId, List<string> studentIds)
        {
            if (studentIds == null || !studentIds.Any())
            {
                TempData["ErrorMessage"] = "Please select at least one student.";
                return RedirectToAction("ManageStudents", new { groupId });
            }

            foreach (var studentId in studentIds)
            {
                var groupMember = new GroupMember
                {
                    GroupId = groupId,
                    StudentId = studentId
                };
                await _groupRepository.AddGroupMemberAsync(groupMember);
            }

            TempData["SuccessMessage"] = "Students added successfully.";
            return RedirectToAction("ManageStudents", new { groupId });
        }


        public async Task<IActionResult> RemoveStudent(int groupId, string studentId)
        {
            var groupMember = await _groupRepository.GetGroupMemberLinkAsync(groupId, studentId);
            if (groupMember == null)
            {
                TempData["ErrorMessage"] = "Student not found in group.";
                return RedirectToAction("ManageStudents", new { groupId });
            }

            await _groupRepository.RemoveGroupMemberAsync(groupMember);

            TempData["SuccessMessage"] = "The student has been successfully removed from the group.";
            return RedirectToAction("ManageStudents", new { groupId });
        }


        public async Task<IActionResult> Details(int id)
        {
            var group = await _groupRepository.GetGroupByIdAsync(id);
            if (group == null)
            {
                TempData["ErrorMessage"] = "Group not found.";
                return RedirectToAction("Index");
            }

            var viewModel = new GroupDetailsViewModel
            {
                GroupId = group.Id,
                GroupName = group.Name,
                Students = group.GroupMembers.Select(gm => gm.Student).ToList(),
                Subjects = group.Subjects.ToList(),
            };


            return View(viewModel);
        }


        //========================================================
        //Subjects
        public async Task<IActionResult> ManageSubjects(int groupId)
        {
            var group = await _groupRepository.GetGroupByIdAsync(groupId);
            if (group == null)
            {
                return NotFound();
            }

            var allSubjects = await _subject.GetAllAsync();
            var model = new ManageSubjectsViewModel
            {
                GroupId = groupId,
                GroupName = group.Name,
                AvailableSubjects = allSubjects,
                AssignedSubjects = group.Subjects
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddSubjectToGroup(int groupId, int subjectId)
        {
            await _groupRepository.AddSubjectToGroupAsync(groupId, subjectId);
            TempData["SuccessMessage"] = "Subject added successfully!";
            return RedirectToAction(nameof(ManageSubjects), new { groupId });
        }
        [HttpPost]
        public async Task<IActionResult> RemoveSubjectFromGroup(int groupId, int subjectId)
        {
            await _groupRepository.RemoveSubjectFromGroupAsync(groupId, subjectId);
            TempData["SuccessMessage"] = "Subject removed successfully!";
            return RedirectToAction(nameof(ManageSubjects), new { groupId });
        }

    }
}
