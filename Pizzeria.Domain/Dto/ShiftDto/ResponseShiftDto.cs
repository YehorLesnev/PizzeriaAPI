using Pizzeria.Domain.Dto.ShiftStaffDto;
using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto.ShiftDto
{
    public record ResponseShiftDto
    {
        [Required]
        public required Guid ShiftId { get; init; }
        
        [Required]
        public required DateOnly ShiftDate { get; init; }

        [Required]
        public required ICollection<ResponseShiftStaffDto> ShiftStaff { get; init; } = new List<ResponseShiftStaffDto>();
    }
}
