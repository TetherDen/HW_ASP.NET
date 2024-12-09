using System.ComponentModel.DataAnnotations;

namespace HW_09.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
		[Required]
		public decimal Price { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public string Category { get; set; }

    }
}
