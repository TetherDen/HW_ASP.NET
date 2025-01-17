using HW_17_Mini_Blog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HW_17_Mini_Blog.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Publication> Publications { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
