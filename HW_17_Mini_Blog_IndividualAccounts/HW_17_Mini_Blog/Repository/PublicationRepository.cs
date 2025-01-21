using HW_17_Mini_Blog.Data;
using HW_17_Mini_Blog.Interfaces;
using HW_17_Mini_Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace HW_17_Mini_Blog.Repository
{
    public class PublicationRepository : IPublication
    {
        private readonly ApplicationDbContext _context;
        public PublicationRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Publication>> GetAllPublicationsAsync()
        {
            return await _context.Publications.ToListAsync();
        }

        public async Task AddPublicationAsync(Publication publication)
        {
            await _context.Publications.AddAsync(publication);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Publication>> GetPublicationsByUserAsync(string userId)
        {
            return await _context.Publications.Where(p => p.AuthorId == userId).ToListAsync();
        }

        public async Task<Publication> GetPublicationByIdAsync(Guid id)
        {
            return await _context.Publications.FirstOrDefaultAsync(p => p.Id == id);
        }


    }
}
