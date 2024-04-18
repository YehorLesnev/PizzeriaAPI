using Pizzeria.Domain.Dto.StaffDto;
using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto.ShiftStaffDto
{
    public class ResponseShiftStaffDto
    {
        [Required]
        public ResponseStaffDto Staff { get; set; }
    }
}
