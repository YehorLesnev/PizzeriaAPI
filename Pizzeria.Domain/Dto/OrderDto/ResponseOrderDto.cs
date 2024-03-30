using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzeria.Domain.Dto.OrderDto
{
    public class ResponseOrderDto
    {
        [Required]
        public required Guid OrderId { get; set; }

        [Required]
        public required DateTime Date { get; set; }

        [Required]
        public required Guid StaffId { get; set; }

        [Required]
        public required Guid? CustomerId { get; set; }

        [Required]
        public required ICollection<OrderItemDto.OrderItemDto> OrderItems { get; set; } 

        [DefaultValue(false)]
        public bool Delivery { get; set; } = false;

        public Guid? DeliveryAddressId { get; set; }
    }
}
