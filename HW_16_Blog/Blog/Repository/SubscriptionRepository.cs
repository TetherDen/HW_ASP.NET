using Blog.Data;
using Blog.Interfaces;
using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Repository
{
    public class SubscriptionRepository : ISubscription
    {
        private readonly ApplicationContext _context;

        public SubscriptionRepository(ApplicationContext context)
        {
            _context = context;
        }


        public async Task<bool> SubscribeAsync(string userId, string authorId)
        {
            if (userId == authorId)
                return false;

            // если подписка существует = возвращаем фалс
            Subscription? existingSubscription = await _context.Subscriptions.FirstOrDefaultAsync (x => x.UserId == userId && x.AuthorId == authorId );
            if(existingSubscription != null)
            {
                return false;
            }

            Subscription subscription = new Subscription
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                AuthorId = authorId,
                SubscribedAt = DateTime.Now,

            };

            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UnsubscribeAsync(string userId, string authorId)
        {
            Subscription? subscription = await _context.Subscriptions.FirstOrDefaultAsync(x => x.UserId == userId && x.AuthorId == authorId);
            if (subscription == null)
            {
                return false;
            }

            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> isSubscribedAsync(string userId, string authorId)
        {
            return await _context.Subscriptions.AnyAsync(u => u.UserId == userId && u.AuthorId == authorId);

        }

        public async Task<int> GetSubscribersCountAsync(string authorId)
        {
            return await _context.Subscriptions.CountAsync(s => s.AuthorId == authorId);
        }
    }
}
