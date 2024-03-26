using System;
using System.Collections.Generic;

namespace Pizzeria.Domain.Models;

public partial class OrderItem
{
    public Guid OrderId { get; set; }

    public Guid ItemId { get; set; }

    public int Quantity { get; set; }

    public virtual Item Item { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
