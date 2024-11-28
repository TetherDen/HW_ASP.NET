namespace HW_06.Models
{
	public static class DbInitializer
	{
		public static void Init(ApplicationDbContext context)
		{

			if (!context.Products.Any())
			{
				var products = new[]
				{
					new Product { Name = "Product1", Price = 100 },
					new Product { Name = "Product2", Price = 200 },
					new Product { Name = "Product3", Price = 300 },
					new Product { Name = "Product4", Price = 400 },
					new Product { Name = "Product5", Price = 500 },
					new Product { Name = "Product6", Price = 600 },
					new Product { Name = "Product7", Price = 700 },
					new Product { Name = "Product8", Price = 800 },

				};

				context.Products.AddRange(products);
				context.SaveChanges();

			}

		}
	}
}
