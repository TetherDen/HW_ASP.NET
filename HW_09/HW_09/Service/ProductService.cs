using HW_09.Models;
using System.Diagnostics.Metrics;

namespace HW_09.Service
{
    public class ProductService
    {
        private List<Product> _products = new List<Product>()
        {
            new Product { Id = _counter++, Name = "Prod1", Price = 100m, Description = "asd", Category = "Category1" },
            new Product { Id = _counter++, Name = "Prod2", Price = 200m, Description = "qwe", Category = "Category1"  },
            new Product { Id = _counter++, Name = "Prod3", Price = 300m, Description = "zxc", Category = "Category2"  },
            new Product { Id = _counter++, Name = "Prod4", Price = 400m, Description = "dfhgf", Category = "Category2"  },
            new Product { Id = _counter++, Name = "Prod5", Price = 500m, Description = "hfvbnbvn", Category = "Category2"  },
        };
        private static int _counter = 1;

        public List<Product> GetProducts () => _products;

        public Product GetProductById (int id) => _products.FirstOrDefault(e=>e.Id == id);

        public void EditProduct(Product EditedProd)
        {
            var product = _products.FirstOrDefault(e=>  e.Id == EditedProd.Id);
            if (product != null)
            {
				product.Price = EditedProd.Price;
                product.Name = EditedProd.Name;
				product.Description = EditedProd.Description;
				product.Category = EditedProd.Category;
			}
        }

		public List<string> GetCategories() => _products.Select(e => e.Category).Distinct().ToList();
	}


}
