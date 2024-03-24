using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzeriaAPI.Domain.Entities
{
    [Table("customers")]
    public class Customer
    {
        [Key, Column("customer_id")]
        public Guid Id { get; init; }

        [Column("first_name", TypeName = "varchar")]
        [MaxLength(55)]
        public string? FirstName { get; set; }

        [Column("last_name", TypeName = "varchar")]
        [MaxLength(55)]
        public string? LastName { get; set; }

        public ICollection<Order> Orders { get; set; } = null!;
    }
}
