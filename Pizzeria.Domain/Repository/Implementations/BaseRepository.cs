using Microsoft.EntityFrameworkCore;
using Pizzeria.Domain.Models;
using Pizzeria.Domain.Repository.Interfaces;
using System.Linq.Expressions;

namespace Pizzeria.Domain.Repository.Implementations
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly PizzeriaDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        protected BaseRepository(PizzeriaDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _dbSet;

            if(filter is not null) 
                query = query.Where(filter);

            return query;
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _dbSet;

            
            if(filter is not null) 
                query = query.Where(filter);

            return await query.FirstOrDefaultAsync();
        }

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task CreateAllAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
