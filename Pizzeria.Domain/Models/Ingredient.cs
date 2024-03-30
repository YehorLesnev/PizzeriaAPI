using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Models;

public partial class Ingredient
{
    public Guid IngredientId { get; set; }

    [MaxLength(100)]
    public string IngredientName { get; set; } = null!;

    [MaxLength(50)]
    public string IngredientWeightMeasure { get; set; } = null!;

    public decimal IngredientPrice { get; set; }

    public virtual Inventory IngredientNavigation { get; set; } = null!;

    public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
}
