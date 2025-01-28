using HW_19_TODO_LIST_RazorPages.Models;
using Microsoft.EntityFrameworkCore;

namespace HW_19_TODO_LIST_RazorPages.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<TaskItem> TaskItems { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }
    }
}
