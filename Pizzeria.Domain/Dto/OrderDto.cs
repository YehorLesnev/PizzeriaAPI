using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto
{
    public class OrderDto
    {
        [Required]
        public required DateTime Date { get; set; }

        [Required]
        public required CustomerDto Customer { get; set; }
        
        [Required]
        public required StaffDto Staff { get; set; }

        [Required]
        public required ICollection<OrderItemDto> OrderItems { get; set; } 

        [DefaultValue(false)]
        public bool Delivery { get; set; } = false;

        public AddressDto? DeliveryAddress { get; set; }
    }
}
