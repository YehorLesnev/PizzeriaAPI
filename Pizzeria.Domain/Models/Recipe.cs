using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizzeria.Domain.Models;

public partial class Recipe
{
    public Guid RecipeId { get; set; }
    
    [Column(TypeName = "varchar")]
    [MaxLength(55)]
    public string RecipeName { get; set; } = null!;

    public TimeOnly CookingTime { get; set; }

    public virtual Item? Item { get; set; }

    public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
}
