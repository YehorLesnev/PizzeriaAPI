using Pizzeria.Domain.Models;
using Pizzeria.Domain.Repository.Interfaces;

namespace Pizzeria.Domain.Services.InventoryService
{
    public class InventoryService(IInventoryRepository inventoryRepository)
        : BaseService<Inventory>(inventoryRepository), IInventoryService
    {
    }
}
