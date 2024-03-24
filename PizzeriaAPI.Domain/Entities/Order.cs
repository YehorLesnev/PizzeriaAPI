using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaAPI.Domain.Entities
{
    [Table("orders")]
    public class Order
    {
        [Key, Column("row_id")]
        public Guid RowId { get; init; }

        [Required, Column("order_id")]
        public required Guid OrderId { get; set; }

        [Required, Column("created_at", TypeName = "DateTime")]
        public required DateTime CreatedAt { get; set; }

        [Required, Column("item_id")]
        public required Guid ItemId { get; set; }

        [Required, Column("quantity")]
        public required int Quantity { get; set; }

        [Required, Column("customer_id")]
        public required Guid CustomerId { get; set;}

        [Required, Column("delivery")]
        [DefaultValue(false)]
        public required bool Delivery { get; set; }

        [Column("delivery_address_id")]
        public Guid DeliveryAddressId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; } = null!;
    }
}
