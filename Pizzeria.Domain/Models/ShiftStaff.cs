using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Models
{
    public class ShiftStaff
    {
        public Guid ShiftId { get; set; }

        public Guid StaffId { get; init; }

        public virtual Shift Shift { get;  init; } = null!;

        public virtual Staff Staff { get;  init; } = null!;
    }
}
