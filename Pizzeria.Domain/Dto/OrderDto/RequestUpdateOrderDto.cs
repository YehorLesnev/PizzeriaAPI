﻿using Pizzeria.Domain.Dto.OrderItemDto;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto.OrderDto
{
    public record RequestUpdateOrderDto
    {
        [Required]
        public required DateTime Date { get; set; }

        [Required]
        public required Guid StaffId { get; set; }

        public Guid? CustomerId { get; set; }

        [Required]
        public required ICollection<RequestOrderItemDto> OrderItems { get; set; } 

        [DefaultValue(false)]
        public bool Delivery { get; set; } = false;

        public Guid? DeliveryAddressId { get; set; }
    }
}
