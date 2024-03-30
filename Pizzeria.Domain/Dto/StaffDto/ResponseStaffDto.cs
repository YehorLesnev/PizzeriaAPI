using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto.StaffDto
{
    public class ResponseStaffDto
    {
        [Required]
        public required Guid StaffId { get; set; }

        [Required, MaxLength(55)]
        public required string FirstName { get; set; }

        [MaxLength(55)]
        public string LastName { get; set; } = null!;

        [Required, MaxLength(100)]
        public required string Position { get; set; } = null!;

        [Required]
        public required decimal HourlyRate { get; set; }
    }
}
