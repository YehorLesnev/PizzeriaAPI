using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto.InventoryDto
{
    public class InventoryDto
    {
        [Required]
        public required int Quantity { get; set; }

        [Required]
        public required Guid IngredientId { get; set; }
    }
}
