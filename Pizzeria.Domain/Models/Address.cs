using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizzeria.Domain.Models;

public partial class Address
{
    public Guid AddressId { get; set; }

    [Column(TypeName = "varchar")]
    [MaxLength(200)]
    public string Address1 { get; set; } = null!;

    [Column(TypeName = "varchar")]
    [MaxLength(200)]
    public string? Address2 { get; set; }

    [Column(TypeName = "varchar")]
    [MaxLength(100)]
    public string City { get; set; } = null!;

    [Column(TypeName = "varchar")]
    [MaxLength(20)]
    public string Zipcode { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
