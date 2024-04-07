using Pizzeria.Domain.Models;

namespace Pizzeria.Domain.Repository.OrderRepository
{
    public class OrderRepository(PizzeriaDbContext dbContext)
        : BaseRepository<Order>(dbContext), IOrderRepository
    {
    }
}
