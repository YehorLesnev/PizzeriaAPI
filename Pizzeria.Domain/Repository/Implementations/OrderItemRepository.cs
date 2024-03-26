using Pizzeria.Domain.Models;
using Pizzeria.Domain.Repository.Interfaces;

namespace Pizzeria.Domain.Repository.Implementations
{
    public class OrderItemRepository(PizzeriaDbContext dbContext)
        : BaseRepository<OrderItem>(dbContext), IOrderItemRepository    
    {
    }
}
