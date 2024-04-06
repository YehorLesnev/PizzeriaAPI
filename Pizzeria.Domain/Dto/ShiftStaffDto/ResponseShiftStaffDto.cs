using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto.ShiftStaffDto
{
    public class ResponseShiftStaffDto
    {
        [Required]
        public required Guid ShiftId { get; init; }

        [Required]
        public required Guid StaffId { get; init; }
    }
}
