using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaAPI.Domain.Entities
{
    [Table("recipe")]
    public class Recipe
    {
        [Key, Column("row_id")]
        public required Guid Id { get; init; }

        [Required, Column("recipe_name", TypeName = "varchar")]
        [MaxLength(30)]
        public required string Name { get; set; }

        [Required, Column("ingredient_id")]
        public required Guid IngredientId { get; set; }

        [Required, Column("quantity")]
        public required int Quantity { get; set; }

        [ForeignKey("ItemId")]
        public Item Item { get; set; } = null!;
    }
}
