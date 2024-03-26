using Pizzeria.Domain.Models;
using Pizzeria.Domain.Repository.Interfaces;

namespace Pizzeria.Domain.Repository.Implementations
{
    public class IngredientRepository(PizzeriaDbContext dbContext) 
        : BaseRepository<Ingredient>(dbContext), IIngredientRepository;
}
