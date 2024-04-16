using Pizzeria.Domain.Dto.IngredientDto;
using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto.RecipeIngredientDto
{
    public class ResponseRecipeIngredientDto
    {
        [Required]
        public RecipeIngredientInfoDto Ingredient { get; set; }

        [Required]
        public required float IngredientWeight { get; set; }
    }
}
