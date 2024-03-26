﻿using System;
using System.Collections.Generic;

namespace Pizzeria.Domain.Models;

public partial class RecipeIngredient
{
    public Guid RecipeId { get; set; }

    public Guid IngredientId { get; set; }

    public int IngredientWeight { get; set; }

    public virtual Ingredient Ingredient { get; set; } = null!;

    public virtual Recipe Recipe { get; set; } = null!;
}
