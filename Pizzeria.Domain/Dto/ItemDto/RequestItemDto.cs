using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto.ItemDto
{
    public class RequestItemDto
    {
        [Required, MaxLength(100)]
        public required string ItemName { get; set; } = null!;

        [Required, MaxLength(100)]
        public required string ItemCategory { get; set; } = null!;

        [Required, MaxLength(55)]
        public required string ItemSize { get; set; } = null!;

        [Required]
        public required decimal ItemPrice { get; set; }

        [Required]
        public required IFormFile Image {get; set;}

        [Required]
        public required Guid RecipeId { get; set; }
    }
}
