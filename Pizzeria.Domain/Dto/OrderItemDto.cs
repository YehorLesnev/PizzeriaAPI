using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto
{
    public class OrderItemDto
    {
        [Required]
        public required Guid OrderId { get; set; }

        [Required]
        public required Guid ItemId { get; set; }

        [Required]
        public required int Quantity { get; set; }
    }
}
