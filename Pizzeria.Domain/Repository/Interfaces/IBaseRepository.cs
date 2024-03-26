using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pizzeria.Domain.Repository.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null);

        Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null);

        Task CreateAsync(T entity);

        Task CreateAllAsync(IEnumerable<T> entities);

        void Update(T entity);

        void Delete(T entity);

        Task SaveAsync();
    }
}
