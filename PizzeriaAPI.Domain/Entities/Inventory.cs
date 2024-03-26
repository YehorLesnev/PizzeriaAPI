using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaAPI.Domain.Entities
{
    [Table("inventory")]
    public class Inventory
    {
        [Key, Column("inventory_id")]
        public Guid Id { get; set; }

        [Column("ingredient_id")]
        public Guid IngredientId { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [ForeignKey("RecipeId")]
        public Recipe Recipe { get; set; } = null!;
    }
}
