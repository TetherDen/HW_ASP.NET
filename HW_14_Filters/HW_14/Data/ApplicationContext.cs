using HW_14.Models;
using Microsoft.EntityFrameworkCore;

namespace HW_14.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Currency> Currencies { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
