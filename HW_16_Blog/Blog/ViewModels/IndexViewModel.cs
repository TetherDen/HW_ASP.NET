using Blog.Models;

namespace Blog.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Publication> Publications { get; set; }
        public List<Category> Categories { get; set; }

        public IEnumerable<Publication> RandomPublications { get; set; }
    }
}
