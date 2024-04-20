using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto.AddressDto
{
    public record RequestAddressDto
    {
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
