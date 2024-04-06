using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Models
{
    public class Shift
    {
        public Guid ShiftId { get; set; } = Guid.NewGuid();
        
        public DateOnly ShiftDate { get; init; }

        public virtual ICollection<ShiftStaff> ShiftStaff { get; init; } = new List<ShiftStaff>();
    }
}
