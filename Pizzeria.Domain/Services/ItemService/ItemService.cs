using Pizzeria.Domain.Models;
using Pizzeria.Domain.Repository.Interfaces;

namespace Pizzeria.Domain.Services.ItemService
{
    public class ItemService(IItemRepository itemRepository)
        : BaseService<Item>(itemRepository), IItemService
    {
    }
}
