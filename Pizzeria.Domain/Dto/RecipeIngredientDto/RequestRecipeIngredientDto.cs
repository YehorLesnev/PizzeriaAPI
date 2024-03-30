using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto.RecipeIngredientDto
{
    public class RequestRecipeIngredientDto
    {
        [Required]
        public required Guid IngredientId { get; set; }

        [Required]
        public required float IngredientWeight { get; set; }
    }
}
