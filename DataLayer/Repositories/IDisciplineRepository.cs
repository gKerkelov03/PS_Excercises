using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Model;

namespace DataLayer.Repositories
{
    public interface IDisciplineRepository
    {
        Task<IEnumerable<Discipline>> GetAllAsync();
        Task<Discipline> GetByIdAsync(int id);
        Task<IEnumerable<Discipline>> GetByYearAsync(int year);
        Task<IEnumerable<Discipline>> GetBySemesterAsync(int semester);
        Task<IEnumerable<Discipline>> GetByLecturerAsync(string lecturer);
        Task<Discipline> AddAsync(Discipline discipline);
        Task UpdateAsync(Discipline discipline);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<int> GetTotalDisciplinesAsync();
        Task<Dictionary<int, int>> GetDisciplinesPerYearAsync();
        Task<Dictionary<int, int>> GetDisciplinesPerSemesterAsync();
    }
} 