using Pizzeria.Domain.Models;
using Pizzeria.Domain.Repository.Interfaces;

namespace Pizzeria.Domain.Repository.Implementations
{
    public class StaffRepository(PizzeriaDbContext dbContext)
        : BaseRepository<Staff>(dbContext), IStaffRepository
    {
    }
}
