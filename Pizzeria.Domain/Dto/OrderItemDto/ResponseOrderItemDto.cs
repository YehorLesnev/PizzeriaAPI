using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto.OrderItemDto
{
    public class ResponseOrderItemDto
    {
        [Required]
        public required Guid OrderId { get; set; }

        [Required]
        public required Guid ItemId { get; set; }

        [Required]
        public required int Quantity { get; set; }
    }
}
