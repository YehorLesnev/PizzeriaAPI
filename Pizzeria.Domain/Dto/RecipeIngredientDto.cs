using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto
{
    public class RecipeIngredientDto
    {
        [Required]
        public required int IngredientWeight { get; set; }

        [Required]
        public required IngredientDto Ingredient { get; set; } 
    }
}
