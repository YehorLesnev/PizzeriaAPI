using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Pizzeria.Domain.Dto.OrderItemDto;

namespace Pizzeria.Domain.Dto.OrderDto
{
    public record ResponseOrderDto
    {
        [Required]
        public required Guid OrderId { get; set; }

        [Required]
        public required DateTime Date { get; set; }

        [Required]
        public required Guid StaffId { get; set; }

        public Guid? CustomerId { get; set; }

        [Required]
        public required ICollection<ResponseOrderItemDto> OrderItems { get; set; }

        [Required]
        public required decimal Total { get; set; }

        [DefaultValue(false)]
        public bool Delivery { get; set; } = false;

        public Guid? DeliveryAddressId { get; set; }
    }
}
