using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto.ShiftDto
{
    public class ResponseShiftDto
    {
        [Required]
        public required Guid ShiftId { get; init; }
        
        [Required]
        public required DateOnly ShiftDate { get; init; }

        [Required]
        public required ICollection<ShiftStaffDto.ResponseShiftStaffDto> ShiftStaff { get; init; } = new List<ShiftStaffDto.ResponseShiftStaffDto>();
    }
}
