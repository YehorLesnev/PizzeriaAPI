using Pizzeria.Domain.Models;
using System.Linq.Expressions;

namespace Pizzeria.Domain.Services.OrderService
{
    public interface IOrderService : IBaseService<Order>
    {
        IEnumerable<Order> GetAllByUserEmail(
            string userEmail,
            Expression<Func<Order, bool>>? filter = null,
            int? pageNumber = null,
            int? pageSize = null,
            bool asNoTracking = false);
    }
}