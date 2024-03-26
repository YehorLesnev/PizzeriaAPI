using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Models;

public partial class Customer
{
    public Guid CustomerId { get; set; }

    [MaxLength(55)]
    public string? FirstName { get; set; }

    [MaxLength(55)]
    public string? LastName { get; set; }

    [MaxLength(15)]
    public string? PhoneNumber { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
