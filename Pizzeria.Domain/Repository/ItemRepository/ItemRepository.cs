using Pizzeria.Domain.Models;

namespace Pizzeria.Domain.Repository.ItemRepository
{
    public class ItemRepository(PizzeriaDbContext dbContext) 
        : BaseRepository<Item>(dbContext), IItemRepository
    {
    }
}