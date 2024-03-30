using System;
using System.Collections.Generic;
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

    public decimal HourlyRate { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
