using Pizzeria.Domain.Models;
using Pizzeria.Domain.Repository.OrderRepository;

namespace Pizzeria.Domain.Services.OrderService
{
    public class OrderService(IOrderRepository orderRepository) : BaseService<Order>(orderRepository), IOrderService
    {

    }
}
