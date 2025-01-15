using Blog.Models;

namespace Blog.ViewModels
{
    public class GetPublicationViewModel
    {
        public Publication Publication { get; set; }
        public string? ReturnUrl { get; set; }
        public string? AuthorName { get; set; } // hw+ , для вывода в представлении getPublication имени автора
    }
}
