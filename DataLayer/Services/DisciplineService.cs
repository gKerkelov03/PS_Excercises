using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Model;
using DataLayer.Repositories;

namespace DataLayer.Services
{
    public class DisciplineService : IDisciplineService
    {
        private readonly IDisciplineRepository _disciplineRepository;

        public DisciplineService(IDisciplineRepository disciplineRepository)
        {
            _disciplineRepository = disciplineRepository;
        }

        public async Task<IEnumerable<Discipline>> GetAllDisciplinesAsync()
        {
            return await _disciplineRepository.GetAllAsync();
        }

        public async Task<Discipline> GetDisciplineByIdAsync(int id)
        {
            return await _disciplineRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Discipline>> GetDisciplinesByYearAsync(int year)
        {
            if (year < 1 || year > 4)
            {
                throw new ArgumentException("Year must be between 1 and 4", nameof(year));
            }
            return await _disciplineRepository.GetByYearAsync(year);
        }

        public async Task<IEnumerable<Discipline>> GetDisciplinesBySemesterAsync(int semester)
        {
            if (semester < 1 || semester > 2)
            {
                throw new ArgumentException("Semester must be either 1 or 2", nameof(semester));
            }
            return await _disciplineRepository.GetBySemesterAsync(semester);
        }

        public async Task<IEnumerable<Discipline>> GetDisciplinesByLecturerAsync(string lecturer)
        {
            if (string.IsNullOrWhiteSpace(lecturer))
            {
                throw new ArgumentException("Lecturer name cannot be empty", nameof(lecturer));
            }
            return await _disciplineRepository.GetByLecturerAsync(lecturer);
        }

        public async Task<Discipline> CreateDisciplineAsync(Discipline discipline)
        {
            if (!await ValidateDisciplineAsync(discipline))
            {
                throw new ArgumentException("Invalid discipline data", nameof(discipline));
            }
            return await _disciplineRepository.AddAsync(discipline);
        }

        public async Task UpdateDisciplineAsync(Discipline discipline)
        {
            if (!await ValidateDisciplineAsync(discipline))
            {
                throw new ArgumentException("Invalid discipline data", nameof(discipline));
            }
            if (!await _disciplineRepository.ExistsAsync(discipline.Id))
            {
                throw new ArgumentException("Discipline not found", nameof(discipline));
            }
            await _disciplineRepository.UpdateAsync(discipline);
        }

        public async Task DeleteDisciplineAsync(int id)
        {
            if (!await _disciplineRepository.ExistsAsync(id))
            {
                throw new ArgumentException("Discipline not found", nameof(id));
            }
            await _disciplineRepository.DeleteAsync(id);
        }

        public async Task<bool> ValidateDisciplineAsync(Discipline discipline)
        {
            if (discipline == null)
                return false;

            if (string.IsNullOrWhiteSpace(discipline.Name))
                return false;

            if (discipline.Year < 1 || discipline.Year > 4)
                return false;

            if (discipline.Semester < 1 || discipline.Semester > 2)
                return false;

            if (discipline.Credits <= 0)
                return false;

            if (string.IsNullOrWhiteSpace(discipline.Lecturer))
                return false;

            if (discipline.MaxStudents <= 0)
                return false;

            return true;
        }

        public async Task<Dictionary<int, int>> GetDisciplineStatisticsByYearAsync()
        {
            return await _disciplineRepository.GetDisciplinesPerYearAsync();
        }

        public async Task<Dictionary<int, int>> GetDisciplineStatisticsBySemesterAsync()
        {
            return await _disciplineRepository.GetDisciplinesPerSemesterAsync();
        }

        public async Task<int> GetTotalDisciplinesCountAsync()
        {
            return await _disciplineRepository.GetTotalDisciplinesAsync();
        }
    }
} 