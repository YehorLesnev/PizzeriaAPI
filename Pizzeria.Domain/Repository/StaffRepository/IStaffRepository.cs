using Pizzeria.Domain.Models;
using System.Linq.Expressions;

namespace Pizzeria.Domain.Repository.StaffRepository
{
    public interface IStaffRepository : IBaseRepository<Staff>
    {
        IEnumerable<Staff> GetAllWithOrders(
            Expression<Func<Staff, bool>>? filter = null,
            int? pageNumber = null,
            int? pageSize = null,
            bool asNoTracking = false);

        Task<Staff?> GetWithOrdersAsync(Expression<Func<Staff, bool>>? filter = null, bool asNoTracking = false);
    }
}
