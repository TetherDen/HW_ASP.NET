using Microsoft.AspNetCore.Identity;

namespace HW_17_Mini_Blog.Models
{
    public class Publication
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime? CreatedAt { get; set; }


        public string? AuthorId { get; set; }
        public IdentityUser? Author { get; set; }

    }
}
