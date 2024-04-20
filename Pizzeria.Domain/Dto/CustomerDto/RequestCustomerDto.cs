using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto.CustomerDto
{
    public record RequestCustomerDto
    {
        [MaxLength(55)]
        public string? FirstName { get; set; }

        [MaxLength(55)]
        public string? LastName { get; set; }

        [MaxLength(25)]
        public string? PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email")]
        public string? Email { get; set; }

        public string Password {get; set;} = null!;
    }
}
