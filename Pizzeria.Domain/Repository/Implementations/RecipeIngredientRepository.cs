using Pizzeria.Domain.Models;
using Pizzeria.Domain.Repository.Interfaces;

namespace Pizzeria.Domain.Repository.Implementations
{
    public class RecipeIngredientRepository(PizzeriaDbContext dbContext)
        : BaseRepository<RecipeIngredient>(dbContext), IRecipeIngredientRepository
    {
    }
}
