using Pizzeria.Domain.Models;
using Pizzeria.Domain.Repository.Interfaces;

namespace Pizzeria.Domain.Repository.Implementations
{
    public class OrderRepository(PizzeriaDbContext dbContext)
        : BaseRepository<Order>(dbContext), IOrderRepository
    {
    }
}
