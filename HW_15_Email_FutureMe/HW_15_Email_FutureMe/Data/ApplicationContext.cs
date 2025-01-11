using HW_15_Email_FutureMe.Models;
using Microsoft.EntityFrameworkCore;

namespace HW_15_Email_FutureMe.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<FutureLetter> FutureLetters { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
