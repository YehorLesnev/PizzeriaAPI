namespace Pizzeria.Domain.Models;

public partial class Inventory
{
    public Guid IngredientId { get; set; }

    public int Quantity { get; set; }

    public virtual Ingredient? Ingredient { get; set; }
}
