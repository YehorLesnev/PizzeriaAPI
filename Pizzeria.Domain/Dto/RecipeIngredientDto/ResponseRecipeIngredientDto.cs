using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto.RecipeIngredientDto
{
    public class ResponseRecipeIngredientDto
    {
        [Required]
        public required Guid RecipeId { get; set; }

        [Required]
        public required Guid IngredientId { get; set; }

        [Required]
        public required int IngredientWeight { get; set; }
    }
}
