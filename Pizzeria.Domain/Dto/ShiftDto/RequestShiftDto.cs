﻿using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto.ShiftDto
{
    public record RequestShiftDto
    {   
        [Required]
        public required DateOnly ShiftDate { get; init; }

        [Required]
        public required ICollection<ShiftStaffDto.RequestShiftStaffDto> ShiftStaff { get; init; } = new List<ShiftStaffDto.RequestShiftStaffDto>();
    }
}
