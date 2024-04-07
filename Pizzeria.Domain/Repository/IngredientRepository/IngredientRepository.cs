using Pizzeria.Domain.Models;

namespace Pizzeria.Domain.Repository.IngredientRepository
{
    public class IngredientRepository(PizzeriaDbContext dbContext) 
        : BaseRepository<Ingredient>(dbContext), IIngredientRepository;
}
