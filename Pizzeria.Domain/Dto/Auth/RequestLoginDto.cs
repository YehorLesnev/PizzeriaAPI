using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto.Auth
{
    public class RequestLoginDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "InvalidEmail")]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
