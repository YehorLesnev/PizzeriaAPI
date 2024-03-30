using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto.OrderItemDto
{
    public class RequestOrderItemDto
    {
        [Required]
        public required Guid ItemId { get; set; }

        [Required]
        public required int Quantity { get; set; }
    }
}
