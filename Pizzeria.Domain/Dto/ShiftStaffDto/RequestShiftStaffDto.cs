using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto.ShiftStaffDto
{
    public class RequestShiftStaffDto
    {
        [Required]
        public required Guid StaffId { get; init; }
    }
}
