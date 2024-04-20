using Microsoft.EntityFrameworkCore;
using Pizzeria.Domain.Models;
using System.Linq.Expressions;

namespace Pizzeria.Domain.Repository.ShiftRepository
{
    public class ShiftRepository(PizzeriaDbContext dbContext)
        : BaseRepository<Shift>(dbContext), IShiftRepository
    {
        public override IEnumerable<Shift> GetAll(
            Expression<Func<Shift, bool>>? filter = null,
            int? pageNumber = null,
            int? pageSize = null,
            bool asNoTracking = false)
        {
            IQueryable<Shift> query = DbSet;

            if (filter is not null)
                query = query.Where(filter);

            query = query.OrderByDescending(x => x.ShiftDate);

            if (pageNumber is null || pageSize is null)
                return asNoTracking ? query.Include("ShiftStaff").Include("ShiftStaff.Staff").AsNoTracking()
                    : query.Include("ShiftStaff").Include("ShiftStaff.Staff");

            return asNoTracking ? query.Include("ShiftStaff").Include("ShiftStaff.Staff")
                .Skip((pageNumber.Value - 1) * pageSize.Value)
                .Take(pageSize.Value)
                .AsNoTracking()
                : query.Include("ShiftStaff").Include("ShiftStaff.Staff")
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
        }

        public override async Task<Shift?> GetAsync(Expression<Func<Shift, bool>>? filter = null, bool asNoTracking = false)
        {
            IQueryable<Shift> query = DbSet;

            if (filter is not null)
                query = query.Where(filter);

            return asNoTracking ? await query.Include("ShiftStaff").Include("ShiftStaff.Staff").AsNoTracking().FirstOrDefaultAsync()
                : await query.Include("ShiftStaff").Include("ShiftStaff.Staff").FirstOrDefaultAsync();
        }
    }
}
