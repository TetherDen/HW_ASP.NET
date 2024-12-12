using HW_10.Models;
using Microsoft.EntityFrameworkCore;

namespace HW_10.Data
{
    // Install-Package Microsoft.EntityFrameworkCore.SqlServer
    public class ApplicationContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
