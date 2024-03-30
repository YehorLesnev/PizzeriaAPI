using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto
{
    public class OrderItemDto
    {
        [Required]
        public required int Quantity { get; set; }

        [Required]
        public required ItemDto Item { get; set; }
    }
}
