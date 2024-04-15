using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizzeria.Domain.Models;

public partial class Ingredient
{
    public Guid IngredientId { get; set; }

    [Column(TypeName = "varchar")]
    [MaxLength(100)]
    public string IngredientName { get; set; } = null!;

    [Column(TypeName = "varchar")]
    [MaxLength(50)]
    public string IngredientWeightMeasure { get; set; } = null!;

    [Required]
    public required decimal IngredientPrice { get; set; }

    [Required]
    public required float QuantityInStock { get; set; }

    public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
}
