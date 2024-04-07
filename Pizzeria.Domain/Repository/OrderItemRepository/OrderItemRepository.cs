using Pizzeria.Domain.Models;

namespace Pizzeria.Domain.Repository.OrderItemRepository
{
    public class OrderItemRepository(PizzeriaDbContext dbContext)
        : BaseRepository<OrderItem>(dbContext), IOrderItemRepository    
    {
    }
}
