using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaAPI.Domain.Entities
{
    [Table("ingredients")]
    public class Ingredient
    {
        [Key, Column("ingredient_id")]
        public required Guid Id { get; init; }

        [Required, Column("ingredient_name", TypeName = "varchar")]
        [MaxLength(200)]
        public required string Name { get; set; }

        [Required, Column("ingredient_weight")]
        public required int Weight { get; set; }

        [Required, Column("ingredient_measure", TypeName = "varchar")]
        [MaxLength(30)]
        public required string Measure { get; set;}

        [Required, Column("ingredient_price", TypeName = "Decimal")]
        public required decimal Price { get; set; }

        [ForeignKey("RecipeId")]
        public Recipe Recipe { get; set; } = null!;
    }
}
