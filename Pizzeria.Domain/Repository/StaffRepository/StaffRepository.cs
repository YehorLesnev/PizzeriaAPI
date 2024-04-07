using Pizzeria.Domain.Models;

namespace Pizzeria.Domain.Repository.StaffRepository
{
    public class StaffRepository(PizzeriaDbContext dbContext)
        : BaseRepository<Staff>(dbContext), IStaffRepository
    {
    }
}
