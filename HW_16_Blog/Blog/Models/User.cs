using Microsoft.AspNetCore.Identity;

namespace Blog.Models
{
    public class User : IdentityUser
    {
        public int PublicationsCount { get; set; }
        public string? Name { get; set; }


        // ============
        // hw  
        // подписки
        public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
        // Подписчики
        public ICollection<Subscription> Subscribers { get; set; } = new List<Subscription>();
        // Публикации юзера
        public ICollection<Publication>? Publications { get; set; }
    }
}
