using System.Linq.Expressions;

namespace Pizzeria.Domain.Services
{
    public interface IBaseService<T> where T : class
    {
        Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, bool asNoTracking = false);

        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, bool asNoTracking = false);

        Task CreateAsync(T entity);

        Task CreateAllAsync(IEnumerable<T> entities);

        void Update(T entity);

        void Delete(T entity);
    }
}
