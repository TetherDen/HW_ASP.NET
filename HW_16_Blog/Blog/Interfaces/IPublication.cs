using Blog.Models;
using Blog.Models.Pages;

namespace Blog.Interfaces
{
    public interface IPublication
    {
        Task<IEnumerable<Publication>> GetAllPublicationsAsync();
        Task<IEnumerable<Publication>> GetAllPublicationsWithCategoriesAsync(QueryOptions options);
        Task<PagedList<Publication>> GetAllPublicationsByCategoryWithCategories(QueryOptions options, string id); // 20-07 добавил изменили
        Task<Publication> GetPublicationAsync(string id);
        Task<Publication> GetPublicationWithCategoriesAsync(string id);

        Task UpdateViewsAsync(string id);
        Task AddPublicationAsync(Publication publication);  
        Task UpdatePublicationAsync(Publication publication);
        Task DeletePublicationAsync(Publication publication);

        // Практика 2
        Task<List<Publication>> GetRandomPublicationsAsync();

    }
}
