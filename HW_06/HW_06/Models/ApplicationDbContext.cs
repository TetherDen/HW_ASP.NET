using Microsoft.EntityFrameworkCore;

namespace HW_06.Models
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<Product> Products { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			base.OnModelCreating(modelBuilder);
		}

	}
}
