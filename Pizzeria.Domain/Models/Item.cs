﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizzeria.Domain.Models;

public partial class Item
{
    public Guid ItemId { get; set; }

    [Column(TypeName = "varchar")]
    [MaxLength(100)]
    public string ItemName { get; set; } = null!;

    [Column(TypeName = "varchar")]
    [MaxLength(100)]
    public string ItemCategory { get; set; } = null!;

    [Column(TypeName = "varchar")]
    [MaxLength(55)]
    public string ItemSize { get; set; } = null!;

    public decimal ItemPrice { get; set; }

    [MaxLength(200)]
    public string ImagePath { get; set; } = null!;

    public Guid RecipeId { get; set; }

    public virtual Recipe ItemNavigation { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
