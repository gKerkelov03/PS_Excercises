using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataLayer.Database;
using DataLayer.Model;

namespace DataLayer.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DatabaseContext _context;
        protected readonly DbSet<T> _dbSet;
        private readonly string _entityName;
        private readonly DataLayer.Logger _logger;

        public Repository(DatabaseContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _entityName = typeof(T).Name;
            _logger = new DataLayer.Logger(context);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                await _logger.LogInfoAsync($"Retrieved {_entityName} with ID: {id}");
            }
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await _dbSet.ToListAsync();
            await _logger.LogInfoAsync($"Retrieved all {_entityName}s");
            return entities;
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            var entities = await _dbSet.Where(predicate).ToListAsync();
            await _logger.LogInfoAsync($"Searched {_entityName}s with predicate");
            return entities;
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _logger.LogInfoAsync($"Created new {_entityName}");
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _logger.LogInfoAsync($"Created {entities.Count()} {_entityName}s");
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _logger.LogInfoAsync($"Updated {_entityName}");
        }

        public async Task RemoveAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _logger.LogInfoAsync($"Deleted {_entityName}");
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            await _logger.LogInfoAsync($"Deleted {entities.Count()} {_entityName}s");
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            var exists = await _dbSet.AnyAsync(predicate);
            await _logger.LogInfoAsync($"Checked existence of {_entityName}");
            return exists;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
} 