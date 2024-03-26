using Pizzeria.Domain.Models;
using Pizzeria.Domain.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzeria.Domain.Repository.Implementations
{
    public class InventoryRepository(PizzeriaDbContext dbContext) 
        : BaseRepository<Inventory>(dbContext) , IInventoryRepository
    {
    }
}
