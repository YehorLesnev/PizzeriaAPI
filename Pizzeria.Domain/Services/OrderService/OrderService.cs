using Pizzeria.Domain.Models;
using Pizzeria.Domain.Repository.OrderRepository;
using System.Linq.Expressions;

namespace Pizzeria.Domain.Services.OrderService
{
    public class OrderService(IOrderRepository orderRepository) : BaseService<Order>(orderRepository), IOrderService
    {
        public IEnumerable<Order> GetAllByUserEmail(
            string userEmail,
            Expression<Func<Order, bool>>? filter = null,
            int? pageNumber = null,
            int? pageSize = null,
            bool asNoTracking = false)
        {
            return orderRepository.GetAllByUserEmail(userEmail, filter, pageNumber, pageSize, asNoTracking);
        }
    }
}
