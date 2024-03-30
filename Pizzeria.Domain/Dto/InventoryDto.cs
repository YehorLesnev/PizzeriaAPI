﻿using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto
{
    public class InventoryDto
    {
        [Required]
        public required int Quantity { get; set; }

        [Required]
        public required IngredientDto Ingredient { get; set; }
    }
}
