using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Models
{
    public class Shift
    {
        public Guid ShiftId { get; set; }
        
        public DateOnly ShiftDate { get; init; }

        public TimeOnly ShiftStartTime {get; init; }
        public TimeOnly ShiftEndTime {get; init; }

        public virtual ICollection<ShiftStaff> ShiftStaff { get; init; } = new List<ShiftStaff>();
    }
}
