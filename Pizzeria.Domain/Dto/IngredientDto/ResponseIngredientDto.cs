using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto.IngredientDto
{
    public record ResponseIngredientDto
    {
        [Required]
        public required Guid IngredientId { get; set; }

        [Required, MaxLength(100)]
        public required string IngredientName { get; set; }

        [Required, MaxLength(50)]
        public required string IngredientWeightMeasure { get; set; }

        [Required]
        public required decimal IngredientPrice { get; set; }

        [Required]
        public required float QuantityInStock { get; set; }
    }
}
