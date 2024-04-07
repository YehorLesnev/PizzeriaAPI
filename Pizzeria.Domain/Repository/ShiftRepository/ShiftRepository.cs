using Pizzeria.Domain.Models;

namespace Pizzeria.Domain.Repository.ShiftRepository
{
    public class ShiftRepository(PizzeriaDbContext dbContext) 
        : BaseRepository<Shift>(dbContext), IShiftRepository
    {
    }
}
