﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizzeria.Domain.Models;

public partial class Customer : IdentityUser<Guid>
{
    [Column(TypeName = "varchar")]
    [MaxLength(55)]
    public string? FirstName { get; set; }

    [Column(TypeName = "varchar")]
    [MaxLength(55)]
    public string? LastName { get; set; }

    [Column(TypeName = "varchar")]
    [MaxLength(25)]
    public string? PhoneNumber { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
