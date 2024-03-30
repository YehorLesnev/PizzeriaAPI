using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto
{
    public class RecipeDto
    {
        [Required, MaxLength(55)]
        public required string RecipeName { get; set; }

        [Required]
        public required TimeOnly CookingTime { get; set; }

        [Required]
        public required ICollection<RecipeIngredientDto> RecipeIngredients { get; set; }
    }
}
