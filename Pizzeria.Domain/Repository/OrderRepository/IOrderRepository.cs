using Pizzeria.Domain.Models;
using System.Linq.Expressions;

namespace Pizzeria.Domain.Repository.OrderRepository
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        public IEnumerable<Order> GetAllByUserEmail(
            string userEmail,
            Expression<Func<Order, bool>>? filter = null,
            int? pageNumber = null,
            int? pageSize = null,
            bool asNoTracking = false);

        IEnumerable<Order> GetAllWithFullInfo(
            Expression<Func<Order, bool>>? filter = null,
            int? pageNumber = null,
            int? pageSize = null,
            bool asNoTracking = false);
    }
}
