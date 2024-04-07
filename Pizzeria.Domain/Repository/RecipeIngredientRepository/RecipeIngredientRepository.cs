using Pizzeria.Domain.Models;

namespace Pizzeria.Domain.Repository.RecipeIngredientRepository
{
    public class RecipeIngredientRepository(PizzeriaDbContext dbContext)
        : BaseRepository<RecipeIngredient>(dbContext), IRecipeIngredientRepository
    {
    }
}
