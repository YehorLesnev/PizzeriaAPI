using System.ComponentModel.DataAnnotations;
using Pizzeria.Domain.Dto.RecipeIngredientDto;

namespace Pizzeria.Domain.Dto.RecipeDto
{
    public record RequestRecipeDto
    {
        [Required, MaxLength(55)]
        public required string RecipeName { get; set; }

        [Required]
        public required TimeOnly CookingTime { get; set; }

        [Required]
        public required ICollection<RequestRecipeIngredientDto> RecipeIngredients { get; set; }
    }
}
