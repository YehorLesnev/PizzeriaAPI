using Microsoft.EntityFrameworkCore;
using Pizzeria.Domain.Models;
using Pizzeria.Domain.Repository.Interfaces;
using System.Linq.Expressions;

namespace Pizzeria.Domain.Repository.Implementations
{
    public class RecipeRepository(PizzeriaDbContext dbContext) 
        : BaseRepository<Recipe>(dbContext), IRecipeRepository
    {
        public override IEnumerable<Recipe> GetAll(Expression<Func<Recipe, bool>>? filter = null, bool asNoTracking = false)
        {
            IQueryable<Recipe> query = DbSet;

            if(filter is not null) 
                query = query.Where(filter);

            return asNoTracking ? query.Include("RecipeIngredients").AsNoTracking() : query.Include("RecipeIngredients");
        }
    }
}
