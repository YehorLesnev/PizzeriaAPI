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

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, bool asNoTracking = false)
        {
            return repository.GetAll(filter, asNoTracking);
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

        public void Update(T entity)
        {
            repository.Update(entity);

            repository.SaveAsync();
        }

        public void Delete(T entity)
        {
            repository.Delete(entity);

            repository.SaveAsync();
        }
    }
}
