using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto.CustomerDto
{
    public record RequestUpdateCustomerDto
    {
        [MaxLength(55)]
        public string? FirstName { get; set; }

        [MaxLength(55)]
        public string? LastName { get; set; }

        [MaxLength(25)]
        public string? PhoneNumber { get; set; }
    }
}
