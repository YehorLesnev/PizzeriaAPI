using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzeriaAPI.Domain.Entities
{
    [Table("items")]
    public class Item
    {
        [Key, Column("item_id")]
        public required Guid Id { get; init; }

        [Required, Column("recipe_name", TypeName = "varchar")]
        [MaxLength(30)]
        public required string RecipeName { get; set; }

        [Required, Column("item_name")]
        [MaxLength(55)]
        public required string Name { get; set; }

        [Required, Column("item_category")]
        [MaxLength(55)]
        public required string Category {get; set; }

        [Required, Column("item_size")]
        [MaxLength(55)]
        public required string Size {get; set; }

        [Required, Column("item_price", TypeName = "Decimal")]
        public required decimal Price { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; } = null!;
    }
}
