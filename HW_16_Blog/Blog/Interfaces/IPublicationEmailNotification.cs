namespace Blog.Interfaces
{
    public interface IPublicationEmailNotification
    {
        Task NotifySubscribersAsync(string authorId, string title, string description);
    }
}
