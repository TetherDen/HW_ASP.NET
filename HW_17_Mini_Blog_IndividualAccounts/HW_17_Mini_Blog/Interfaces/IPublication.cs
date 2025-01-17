using HW_17_Mini_Blog.Models;

namespace HW_17_Mini_Blog.Interfaces
{
    public interface IPublication
    {
        Task<IEnumerable<Publication>> GetAllPublicationsAsync();
        Task AddPublicationAsync(Publication publication);
        Task<IEnumerable<Publication>> GetPublicationsByUserAsync(string userId);
        Task<Publication> GetPublicationByIdAsync(Guid id);
    }
}
