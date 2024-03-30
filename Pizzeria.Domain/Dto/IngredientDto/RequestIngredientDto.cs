using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto.IngredientDto
{
    public class RequestIngredientDto
    {
        [Required, MaxLength(100)]
        public required string IngredientName { get; set; }

        [Required, MaxLength(50)]
        public required string IngredientWeightMeasure { get; set; }

        [Required]
        public required decimal IngredientPrice { get; set; }

        [Required]
        public required long QuantityInStock { get; set; }
    }
}
