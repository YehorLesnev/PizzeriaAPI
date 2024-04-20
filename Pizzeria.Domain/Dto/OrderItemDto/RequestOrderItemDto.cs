using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto.OrderItemDto
{
    public record RequestOrderItemDto
    {
        [Required]
        public required Guid ItemId { get; set; }

        [Required]
        public required int Quantity { get; set; }
    }
}
