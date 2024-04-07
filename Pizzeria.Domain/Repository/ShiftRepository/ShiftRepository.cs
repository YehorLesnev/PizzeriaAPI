using Microsoft.EntityFrameworkCore;
using Pizzeria.Domain.Models;
using System.Linq.Expressions;

namespace Pizzeria.Domain.Repository.ShiftRepository
{
    public class ShiftRepository(PizzeriaDbContext dbContext) 
        : BaseRepository<Shift>(dbContext), IShiftRepository
    {
        public override IEnumerable<Shift> GetAll(Expression<Func<Shift, bool>>? filter = null, bool asNoTracking = false)
        {
            IQueryable<Shift> query = DbSet;

            if(filter is not null) 
                query = query.Where(filter);

            return asNoTracking ? query.Include("ShiftStaff").AsNoTracking() : query.Include("ShiftStaff");
        }
    }
}
