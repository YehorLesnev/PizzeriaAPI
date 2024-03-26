using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Models;

public partial class Address
{
    public Guid AddressId { get; set; }

    [MaxLength(200)]
    public string Address1 { get; set; } = null!;

    [MaxLength(200)]
    public string? Address2 { get; set; }

    [MaxLength(100)]
    public string City { get; set; } = null!;

    [MaxLength(20)]
    public string Zipcode { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
