using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Model;

namespace DataLayer.Services
{
    public interface IDisciplineService
    {
        Task<IEnumerable<Discipline>> GetAllDisciplinesAsync();
        Task<Discipline> GetDisciplineByIdAsync(int id);
        Task<IEnumerable<Discipline>> GetDisciplinesByYearAsync(int year);
        Task<IEnumerable<Discipline>> GetDisciplinesBySemesterAsync(int semester);
        Task<IEnumerable<Discipline>> GetDisciplinesByLecturerAsync(string lecturer);
        Task<Discipline> CreateDisciplineAsync(Discipline discipline);
        Task UpdateDisciplineAsync(Discipline discipline);
        Task DeleteDisciplineAsync(int id);
        Task<bool> ValidateDisciplineAsync(Discipline discipline);
        Task<Dictionary<int, int>> GetDisciplineStatisticsByYearAsync();
        Task<Dictionary<int, int>> GetDisciplineStatisticsBySemesterAsync();
        Task<int> GetTotalDisciplinesCountAsync();
    }
} 