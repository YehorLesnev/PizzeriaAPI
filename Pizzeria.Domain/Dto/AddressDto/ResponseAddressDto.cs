using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzeria.Domain.Dto.AddressDto
{
    public class ResponseAddressDto
    {
        [Required]
        public required Guid AddressId { get; set; }

        [Required, MaxLength(200)]
        public required string Address1 { get; set; }

        [MaxLength(200)]
        public string? Address2 { get; set; }

        [Required, MaxLength(100)]
        public required string City { get; set; }

        [MaxLength(20)]
        public string Zipcode { get; set; } = null!;
    }
}
