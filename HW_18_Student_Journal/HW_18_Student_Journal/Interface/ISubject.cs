using HW_18_Student_Journal.Models;

namespace HW_18_Student_Journal.Interface
{
    public interface ISubject
    {
        Task<IEnumerable<Subject>> GetAllAsync();
        Task<Subject> GetByIdAsync(int id);
        Task AddAsync(Subject subject);
        Task UpdateAsync(Subject subject);
        Task DeleteAsync(int id);
    }
}
