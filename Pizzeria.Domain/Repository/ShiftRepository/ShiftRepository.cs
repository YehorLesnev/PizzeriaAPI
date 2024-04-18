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

            if (pageNumber is null || pageSize is null)
                return asNoTracking ? query.Include("ShiftStaff").AsNoTracking()
                    : query.Include("ShiftStaff");

            return asNoTracking ? query.Include("ShiftStaff")
                .Skip((pageNumber.Value - 1) * pageSize.Value)
                .Take(pageSize.Value)
                .AsNoTracking()
                : query.Include("ShiftStaff")
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
        }

        public override async Task<Shift?> GetAsync(Expression<Func<Shift, bool>>? filter = null, bool asNoTracking = false)
        {
            IQueryable<Shift> query = DbSet;

            if (filter is not null)
                query = query.Where(filter);

            return asNoTracking ? await query.Include("ShiftStaff").AsNoTracking().FirstOrDefaultAsync()
                : await query.Include("ShiftStaff").FirstOrDefaultAsync();
        }
    }
}
