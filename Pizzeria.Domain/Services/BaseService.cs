using System.Linq.Expressions;
using Pizzeria.Domain.Repository;

namespace Pizzeria.Domain.Services
{
    public abstract class BaseService<T>(IBaseRepository<T> repository)
        where T : class
    {
        public async Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, bool asNoTracking = false)
        {
            return await repository.GetAsync(filter, asNoTracking);
        }

        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null,
            int? pageNumber = null,
            int? pageSize = null,
            bool asNoTracking = false)
        {
            return repository.GetAll(filter, pageNumber, pageSize, asNoTracking);
        }

        public Task CreateAsync(T entity)
        {
            repository.CreateAsync(entity);

            return repository.SaveAsync();
        }

        public Task CreateAllAsync(IEnumerable<T> entities)
        {
            repository.CreateAllAsync(entities);

            return repository.SaveAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            repository.Update(entity);

            await repository.SaveAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            repository.Delete(entity);

            await repository.SaveAsync();
        }
    }
}
