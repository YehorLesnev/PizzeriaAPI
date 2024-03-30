using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizzeria.Domain.Models;

public partial class Customer
{
    public Guid CustomerId { get; set; } = Guid.NewGuid();

    [Column(TypeName = "varchar")]
    [MaxLength(55)]
    public string? FirstName { get; set; }

    [Column(TypeName = "varchar")]
    [MaxLength(55)]
    public string? LastName { get; set; }

    [Column(TypeName = "varchar")]
    [MaxLength(15)]
    public string? PhoneNumber { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
