using Pizzeria.Domain.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Pizzeria.Domain.Repository.AddressRepository
{
    public class AddressRepository(PizzeriaDbContext dbContext) 
        : BaseRepository<Address>(dbContext), IAddressRepository
    {
        public override IEnumerable<Address> GetAll(Expression<Func<Address, bool>>? filter = null,
            int? pageNumber = null,
            int? pageSize = null,
            bool asNoTracking = false)
        {
            IQueryable<Address> query = DbSet;

            if (filter is not null)
                query = query.Where(filter);

            if (pageNumber is null || pageSize is null)
                return asNoTracking ? query.AsNoTracking() : query;

            query = query.OrderBy(x => x.City);

            return asNoTracking ? query
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value)
                    .AsNoTracking()
                : query
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
        }
    }
}
