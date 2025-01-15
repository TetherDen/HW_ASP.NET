namespace Blog.Models
{
    public class Subscription
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }      //  юзер который подписан
        public User User { get; set; }          // навигационное свойство юзер
        public string AuthorId { get; set; }    // автор на которого подписан юзер
        public User Author {  get; set; }        // навигационное свойство автор
        public DateTime SubscribedAt { get; set; }
    }
}
