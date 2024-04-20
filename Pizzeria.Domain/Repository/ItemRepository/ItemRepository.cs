using Microsoft.EntityFrameworkCore;
using Pizzeria.Domain.Models;
using System.Linq.Expressions;
using System.Linq;

namespace Pizzeria.Domain.Repository.ItemRepository
{
    public class ItemRepository(PizzeriaDbContext dbContext) 
        : BaseRepository<Item>(dbContext), IItemRepository
    {
        public override IEnumerable<Item> GetAll(Expression<Func<Item, bool>>? filter = null,
            int? pageNumber = null,
            int? pageSize = null,
            bool asNoTracking = false)
        {
            IQueryable<Item> query = DbSet;

            if (filter is not null)
                query = query.Where(filter);

            if (pageNumber is null || pageSize is null)
                return asNoTracking ? query.AsNoTracking() : query;

            query = query.OrderBy(x => x.ItemName);

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