using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto.ShiftStaffDto
{
    public record RequestShiftStaffDto
    {
        [Required]
        public required Guid StaffId { get; init; }
    }
}
