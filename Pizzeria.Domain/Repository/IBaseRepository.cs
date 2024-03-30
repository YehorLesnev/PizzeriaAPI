using System.Linq.Expressions;

namespace Pizzeria.Domain.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, bool asNoTracking = false);

        Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, bool asNoTracking = false);

        Task CreateAsync(T entity);

        Task CreateAllAsync(IEnumerable<T> entities);

        void Update(T entity);

        void Delete(T entity);

        Task SaveAsync();
    }
}
