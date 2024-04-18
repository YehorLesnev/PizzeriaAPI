using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto.StaffDto
{
    public class ResponseStaffDto
    {
        [Required]
        public Guid StaffId { get; set; }

        [Required, MaxLength(55)]
        public string FirstName { get; set; }

        [MaxLength(55)]
        public string LastName { get; set; } = null!;

        [Required, MaxLength(100)]
        public string Position { get; set; } = null!;

        [MaxLength(25)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public decimal HourlyRate { get; set; }
    }
}
