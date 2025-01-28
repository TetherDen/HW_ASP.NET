using HW_19_TODO_LIST_RazorPages.Data;
using HW_19_TODO_LIST_RazorPages.Models;
using HW_19_TODO_LIST_RazorPages.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HW_19_TODO_LIST_RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationContext _context;

        public List<TaskItem> Tasks { get; set; } = new();

        [BindProperty]
        public TaskItemViewModel TaskViewModel { get; set; }

        [BindProperty]
        public int TaskId { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Tasks = await _context.TaskItems.ToListAsync();
        }


        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                var newTask = new TaskItem
                {
                    Title = TaskViewModel.Title,
                    Description = TaskViewModel.Description,
                    IsCompleted = false,
                };

                await _context.TaskItems.AddAsync(newTask);
                await _context.SaveChangesAsync();

                return RedirectToPage();
            }

            Tasks = await _context.TaskItems.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var task = await  _context.TaskItems.FindAsync(id);
            if (task != null)
            {
                _context.TaskItems.Remove(task);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostToggleCompleted(int id)
        {
            var task = await _context.TaskItems.FindAsync(id);
            if (task != null)
            {
                task.IsCompleted = !task.IsCompleted;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateTask()
        {
            var task = await _context.TaskItems.FindAsync(TaskId);
            if (task != null)
            {
                // если тайтл/дескрипшен пустой то оставляем старую запись...
                task.Title = string.IsNullOrEmpty(TaskViewModel.Title) ? task.Title : TaskViewModel.Title;
                task.Description = string.IsNullOrEmpty(TaskViewModel.Description) ? task.Description : TaskViewModel.Description;

                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }



    }
}
