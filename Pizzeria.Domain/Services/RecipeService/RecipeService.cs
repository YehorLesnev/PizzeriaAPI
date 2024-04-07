using Pizzeria.Domain.Models;
using Pizzeria.Domain.Repository.RecipeRepository;

namespace Pizzeria.Domain.Services.RecipeService
{
    public class RecipeService(IRecipeRepository recipeRepository)
        : BaseService<Recipe>(recipeRepository), IRecipeService
    {
    }
}
