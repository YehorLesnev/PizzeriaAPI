using Pizzeria.Domain.Models;
using Pizzeria.Domain.Repository.Interfaces;

namespace Pizzeria.Domain.Repository.Implementations
{
    public class ItemRepository(PizzeriaDbContext dbContext) 
        : BaseRepository<Item>(dbContext), IItemRepository
    {
    }
}
