using Pizzeria.Domain.Models;
using Pizzeria.Domain.Repository.Interfaces;

namespace Pizzeria.Domain.Repository.Implementations
{
    public class RecipeRepository(PizzeriaDbContext dbContext) 
        : BaseRepository<Recipe>(dbContext), IRecipeRepository
    {
    }
}
