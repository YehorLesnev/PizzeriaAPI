using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto.RecipeDto
{
    public class ResponseRecipeDto
    {
        [Required]
        public required Guid RecipeId { get; set; }

        [Required, MaxLength(55)]
        public required string RecipeName { get; set; }

        [Required]
        public required TimeOnly CookingTime { get; set; }

        [Required]
        public required ICollection<RecipeIngredientDto.RecipeIngredientDto> RecipeIngredients { get; set; }
    }
}
