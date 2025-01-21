using HW_18_Student_Journal.Data;
using HW_18_Student_Journal.Interface;
using HW_18_Student_Journal.Models;
using Microsoft.EntityFrameworkCore;

namespace HW_18_Student_Journal.Repository
{
    public class SubjectRepository : ISubject
    {
        private readonly ApplicationContext _context;
        public SubjectRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            return await _context.Subjects.ToListAsync();
        }

        public async Task<Subject> GetByIdAsync(int id)
        {
            return await _context.Subjects.FindAsync(id);
        }

        public async Task AddAsync(Subject subject)
        {
            await _context.Subjects.AddAsync(subject);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Subject subject)
        {
            _context.Subjects.Update(subject);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject != null)
            {
                _context.Subjects.Remove(subject);
                await _context.SaveChangesAsync();
            }
        }

    }
}
