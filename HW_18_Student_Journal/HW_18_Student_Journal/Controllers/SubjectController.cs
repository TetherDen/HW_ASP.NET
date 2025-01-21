using HW_18_Student_Journal.Filters;
using HW_18_Student_Journal.Interface;
using HW_18_Student_Journal.Models;
using HW_18_Student_Journal.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HW_18_Student_Journal.Controllers
{


    [Authorize]
    [AutoValidateAntiforgeryToken]
    [ServiceFilter(typeof(IsTeacherActionFilter))]
    public class SubjectController : Controller
    {
        private readonly ISubject _subject;
        private readonly IGroup _group;

        public SubjectController(ISubject subject, IGroup group)
        {
            _subject = subject;
            _group = group;
        }

        public async Task<IActionResult> Index()
        {
            var subjects = await _subject.GetAllAsync();
            return View(subjects);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Subject subject)
        {
            if (ModelState.IsValid)
            {
                await _subject.AddAsync(subject);
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var subject = await _subject.GetByIdAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            return View(subject);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Subject subject)
        {
            if (ModelState.IsValid)
            {
                await _subject.UpdateAsync(subject);
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _subject.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }


        //================================






    }
}
