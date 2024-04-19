using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto.Auth
{
    public class ResponseLoginDto
    {
        [Required]
        public string Email { get; set; }

        [Required] 
        public string Token { get; set; }

        [Required]
        public DateTime Expires { get; set; }

        [Required]
        public IEnumerable<string> Roles { get; set; }
    }
}
