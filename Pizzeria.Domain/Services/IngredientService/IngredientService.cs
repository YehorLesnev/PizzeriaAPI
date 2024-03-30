using Pizzeria.Domain.Models;
using Pizzeria.Domain.Repository.Interfaces;

namespace Pizzeria.Domain.Services.IngredientService
{
    public class IngredientService(IIngredientRepository ingredientRepository)
        : BaseService<Ingredient>(ingredientRepository), IIngredientService
    {
    }
}
