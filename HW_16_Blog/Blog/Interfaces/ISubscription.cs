namespace Blog.Interfaces
{
    public interface ISubscription
    {
        Task<bool> SubscribeAsync(string userId, string authorId);
        Task<bool> UnsubscribeAsync(string userId, string authorId);
        Task<bool> isSubscribedAsync(string userId, string authorId);
        Task<int> GetSubscribersCountAsync(string authorId);


    }
}
