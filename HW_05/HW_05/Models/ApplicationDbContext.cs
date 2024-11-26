using Microsoft.EntityFrameworkCore;

namespace HW_05.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<CustomerService> Services { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Token)
                .IsUnique();


            modelBuilder.Entity<CustomerService>()
                .HasOne(s => s.User)
                .WithMany(u => u.CustomerServices)
                .HasForeignKey(s => s.UserToken)
                .HasPrincipalKey(u => u.Token);




            base.OnModelCreating(modelBuilder);
        }

    }

}
