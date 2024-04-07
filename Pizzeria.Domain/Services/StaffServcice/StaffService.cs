using Pizzeria.Domain.Models;
using Pizzeria.Domain.Repository.StaffRepository;

namespace Pizzeria.Domain.Services.StaffServcice
{
    public class StaffService(IStaffRepository staffRepository) 
        : BaseService<Staff>(staffRepository), IStaffService
    {
    }
}
