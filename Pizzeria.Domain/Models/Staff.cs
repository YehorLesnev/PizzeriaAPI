using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizzeria.Domain.Models;

public partial class Staff
{
    public Guid StaffId { get; set; } = Guid.NewGuid();

    [Column(TypeName = "varchar")]
    [MaxLength(55)]
    public string FirstName { get; set; } = null!;

    [Column(TypeName = "varchar")]
    [MaxLength(55)]
    public string LastName { get; set; } = null!;

    [Column(TypeName = "varchar")]
    [MaxLength(100)]
    public string Position { get; set; } = null!;

    [Column(TypeName = "varchar")]
    [MaxLength(25)]
    public string PhoneNumber { get; set; } = null!;
    
    public decimal HourlyRate { get; set; }
    
    public virtual ICollection<Order> Orders { get; init; } = new List<Order>();

    public virtual ICollection<ShiftStaff> ShiftStaff { get; init; } = new List<ShiftStaff>();
}
