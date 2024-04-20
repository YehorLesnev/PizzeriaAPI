using Microsoft.EntityFrameworkCore;
using Pizzeria.Domain.Models;
using System.Linq.Expressions;
using System.Linq;

namespace Pizzeria.Domain.Repository.IngredientRepository
{
    public class IngredientRepository(PizzeriaDbContext dbContext) 
        : BaseRepository<Ingredient>(dbContext), IIngredientRepository
    {
        public override IEnumerable<Ingredient> GetAll(Expression<Func<Ingredient, bool>>? filter = null,
            int? pageNumber = null,
            int? pageSize = null,
            bool asNoTracking = false)
        {
            IQueryable<Ingredient> query = DbSet;

            if (filter is not null)
                query = query.Where(filter);

            if (pageNumber is null || pageSize is null)
                return asNoTracking ? query.AsNoTracking() : query;

            query = query.OrderBy(x => x.IngredientName);

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
