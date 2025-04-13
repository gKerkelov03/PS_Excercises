using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Database;
using DataLayer.Model;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class DisciplineRepository : IDisciplineRepository
    {
        private readonly DatabaseContext _context;

        public DisciplineRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Discipline>> GetAllAsync()
        {
            return await _context.Disciplines.ToListAsync();
        }

        public async Task<Discipline> GetByIdAsync(int id)
        {
            return await _context.Disciplines.FindAsync(id);
        }

        public async Task<IEnumerable<Discipline>> GetByYearAsync(int year)
        {
            return await _context.Disciplines
                .Where(d => d.Year == year)
                .ToListAsync();
        }

        public async Task<IEnumerable<Discipline>> GetBySemesterAsync(int semester)
        {
            return await _context.Disciplines
                .Where(d => d.Semester == semester)
                .ToListAsync();
        }

        public async Task<IEnumerable<Discipline>> GetByLecturerAsync(string lecturer)
        {
            return await _context.Disciplines
                .Where(d => d.Lecturer == lecturer)
                .ToListAsync();
        }

        public async Task<Discipline> AddAsync(Discipline discipline)
        {
            _context.Disciplines.Add(discipline);
            await _context.SaveChangesAsync();
            return discipline;
        }

        public async Task UpdateAsync(Discipline discipline)
        {
            _context.Entry(discipline).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var discipline = await _context.Disciplines.FindAsync(id);
            if (discipline != null)
            {
                _context.Disciplines.Remove(discipline);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Disciplines.AnyAsync(d => d.Id == id);
        }

        public async Task<int> GetTotalDisciplinesAsync()
        {
            return await _context.Disciplines.CountAsync();
        }

        public async Task<Dictionary<int, int>> GetDisciplinesPerYearAsync()
        {
            return await _context.Disciplines
                .GroupBy(d => d.Year)
                .ToDictionaryAsync(g => g.Key, g => g.Count());
        }

        public async Task<Dictionary<int, int>> GetDisciplinesPerSemesterAsync()
        {
            return await _context.Disciplines
                .GroupBy(d => d.Semester)
                .ToDictionaryAsync(g => g.Key, g => g.Count());
        }
    }
} 