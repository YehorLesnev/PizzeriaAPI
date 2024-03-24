using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaAPI.Domain.Entities
{
    [Table("address")]
    public class Address
    {
        [Key, Column("address_id")]
        public Guid Id { get; init; }

        [Required, Column("delivery_address1", TypeName = "varchar")]
        [MaxLength(200)]
        public required string DeliveryAddress1 { get; set; }

        [Column("delivery_address2", TypeName = "varchar")]
        [MaxLength(200)]
        public string? DeliveryAddress2 { get; set; }

        [Required, Column("delivery_city", TypeName = "varchar")]
        [MaxLength(55)]
        public required string DeliveryCity { get; set; }
        
        [Required, Column("delivery_zipcode", TypeName = "varchar")]
        [MaxLength(25)]
        public required string DeliveryZipcode { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; } = null!;
    }
}
