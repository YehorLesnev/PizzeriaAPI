using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto.StaffDto
{
    public record RequestStaffDto
    {
        [Required, MaxLength(55)]
        public required string FirstName { get; set; }

        [MaxLength(55)]
        public string LastName { get; set; } = null!;

        [MaxLength(25)]
        public required string PhoneNumber { get; set; }

        [Required, MaxLength(100)]
        public required string Position { get; set; } = null!;

        [Required]
        public required decimal HourlyRate { get; set; }
    }
}
