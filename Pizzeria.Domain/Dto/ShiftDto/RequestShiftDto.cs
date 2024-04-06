using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto.ShiftDto
{
    public class RequestShiftDto
    {   
        [Required]
        public required DateOnly ShiftDate { get; init; }

        [Required]
        public required ICollection<ShiftStaffDto.RequestShiftStaffDto> ShiftStaff { get; init; } = new List<ShiftStaffDto.RequestShiftStaffDto>();
    }
}
