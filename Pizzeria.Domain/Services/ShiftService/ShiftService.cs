using Pizzeria.Domain.Models;
using Pizzeria.Domain.Repository.ShiftRepository;

namespace Pizzeria.Domain.Services.ShiftService
{
    public class ShiftService(IShiftRepository repository)
        : BaseService<Shift>(repository), IShiftService
    {
    }
}
