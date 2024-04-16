using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto.RecipeIngredientDto
{
    public class RecipeIngredientInfoDto
    {
        [Required]
        public required Guid IngredientId { get; set; }

        [Required, MaxLength(100)]
        public required string IngredientName { get; set; }

        [Required, MaxLength(50)]
        public required string IngredientWeightMeasure { get; set; }
    }
}
