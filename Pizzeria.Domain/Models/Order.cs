namespace Pizzeria.Domain.Models;

public partial class Order
{
    public Guid OrderId { get; set; }

    public DateTime Date { get; set; }
    
    public Guid? StaffId { get; set; }

    public Guid? CustomerId { get; set; }

    public bool Delivery { get; set; }

    public Guid? DeliveryAddressId { get; set; }

    public decimal Total { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Address? DeliveryAddress { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Staff? Staff { get; set; }
}
