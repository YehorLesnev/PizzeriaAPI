﻿using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto.CustomerDto
{
    public class ResponseCustomerDto
    {
        [Required]
        public required Guid CustomerId { get; set; }

        [MaxLength(55)]
        public string? FirstName { get; set; }

        [MaxLength(55)]
        public string? LastName { get; set; }

        [MaxLength(15)]
        public string? PhoneNumber { get; set; }
    }
}