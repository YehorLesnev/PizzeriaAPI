using Pizzeria.Domain.Models;
using Pizzeria.Domain.Services.IngredientService;
using Pizzeria.Domain.Services.ItemService;
using Pizzeria.Domain.Services.OrderService;
using Pizzeria.Domain.Services.RecipeService;

namespace Pizzeria.Domain.Seeder
{
    public class Seeder(
    IIngredientService ingredientService,
    IRecipeService recipeService,
    IItemService itemService,
    IOrderService orderService) 
        : ISeeder
    {
        private readonly Random _random = new Random();

        public async Task SeedItems()
        {
            var ingredients = new List<Ingredient>();
            var recipes = new List<Recipe>();
            var items = new List<Item>();

            foreach (var ingrName in Constants.Constants.IngredientNames)
            {
                var isKg = _random.Next(0, 2);

                ingredients.Add(new Ingredient
                {
                    IngredientId = Guid.NewGuid(),
                    IngredientName = ingrName,
                    IngredientWeightMeasure = isKg == 0 ? "kg" : "g",
                    IngredientPrice = isKg == 0 ? (decimal)_random.Next(1, 10) + (decimal)_random.NextDouble() : (decimal) (_random.NextDouble() * 2.0),
                    QuantityInStock = isKg == 0 ? _random.Next(10, 50) : _random.Next(500, 5000),
                });
            }

            await ingredientService.CreateAllAsync(ingredients);

           foreach (var item in Constants.Constants.PizzaItemNames)
            {
                var recipeIngredientsCount = _random.Next(5, 12);
                var ingredientsCount = ingredients.Count;
                var itemIngredients = new List<Ingredient>(recipeIngredientsCount);
                var price = (decimal)_random.Next(10, 35) + (decimal)_random.NextDouble();
                var time = new TimeOnly(0, _random.Next(5, 55), 0);

                for(int i = 0; i < ingredientsCount; ++i)
                {
                    var ingredient = ingredients[_random.Next(0, ingredientsCount - 1)];

                    if(itemIngredients.Any(x => x.IngredientId.Equals(ingredient.IngredientId))) continue;

                    itemIngredients.Add(ingredient);
                }

                for (int iSize = 0; iSize < Constants.Constants.ItemSizeNames.Count; ++iSize)
                {
                    var recipeId = Guid.NewGuid();
                    var recipeIngredients = new List<RecipeIngredient>();
                    
                    foreach(var ingredient in itemIngredients)
                    {                       
                        if(recipeIngredients.Any(x => x.IngredientId.Equals(ingredient.IngredientId))) continue;
                        if(recipeIngredients.Any(x => x.IngredientId.Equals(ingredient.IngredientId) && x.RecipeId.Equals(recipeId))) continue;

                        recipeIngredients.Add(new RecipeIngredient
                        {
                            RecipeId = recipeId,
                            IngredientId = ingredient.IngredientId,
                            IngredientWeight = (float)0.5 * (1 + iSize) * (ingredient.IngredientWeightMeasure == "kg" ? (float)_random.Next(0, 1) + (float)_random.NextDouble() : (float)_random.Next(10, 300)),
                        });
                    }

                    recipes.Add(new Recipe
                    {
                        RecipeId = recipeId,
                        RecipeName = $"Pizza_{item}_{Constants.Constants.ItemSizeNames[iSize]}",
                        CookingTime = time,
                        RecipeIngredients = recipeIngredients
                    });

                    items.Add(new Item
                    {
                        ItemId = Guid.NewGuid(),
                        ItemName = item,
                        ItemCategory = "Pizza",
                        ItemSize = Constants.Constants.ItemSizeNames[iSize],
                        ItemPrice = price * (decimal) 0.5 * (1 + iSize),
                        RecipeId = recipeId
                    });
                }
            }

            foreach (var item in Constants.Constants.BurgerItemNames)
            {
                var recipeIngredientsCount = _random.Next(5, 12);
                var ingredientsCount = ingredients.Count;
                var itemIngredients = new List<Ingredient>(recipeIngredientsCount);
                var price = (decimal)_random.Next(10, 35) + (decimal)_random.NextDouble();
                var time = new TimeOnly(0, _random.Next(5, 55), 0);

                for(int i = 0; i < ingredientsCount; ++i)
                {
                    var ingredient = ingredients[_random.Next(0, ingredientsCount - 1)];

                    if(itemIngredients.Any(x => x.IngredientId.Equals(ingredient.IngredientId))) continue;

                    itemIngredients.Add(ingredient);
                }

                for (int iSize = 0; iSize < Constants.Constants.ItemSizeNames.Count; ++iSize)
                {
                    var recipeId = Guid.NewGuid();
                    var recipeIngredients = new List<RecipeIngredient>();
                    
                    foreach(var ingredient in itemIngredients)
                    {                       
                        if(recipeIngredients.Any(x => x.IngredientId.Equals(ingredient.IngredientId))) continue;
                        if(recipeIngredients.Any(x => x.IngredientId.Equals(ingredient.IngredientId) && x.RecipeId.Equals(recipeId))) continue;

                        recipeIngredients.Add(new RecipeIngredient
                        {
                            RecipeId = recipeId,
                            IngredientId = ingredient.IngredientId,
                            IngredientWeight = (float)0.5 * (1 + iSize) * (ingredient.IngredientWeightMeasure == "kg" ? (float)_random.Next(0, 1) + (float)_random.NextDouble() : (float)_random.Next(10, 300)),
                        });
                    }

                    recipes.Add(new Recipe
                    {
                        RecipeId = recipeId,
                        RecipeName = $"Burger_{item}_{Constants.Constants.ItemSizeNames[iSize]}",
                        CookingTime = time,
                        RecipeIngredients = recipeIngredients
                    });

                    items.Add(new Item
                    {
                        ItemId = Guid.NewGuid(),
                        ItemName = item,
                        ItemCategory = "Burger",
                        ItemSize = Constants.Constants.ItemSizeNames[iSize],
                        ItemPrice = price * (decimal) 0.5 * (1 + iSize),
                        RecipeId = recipeId
                    });
                }
            }

            foreach (var item in Constants.Constants.FrenchFriesItemNames)
            {
                var recipeIngredientsCount = _random.Next(5, 12);
                var ingredientsCount = ingredients.Count;
                var itemIngredients = new List<Ingredient>(recipeIngredientsCount);
                var price = (decimal)_random.Next(10, 35) + (decimal)_random.NextDouble();
                var time = new TimeOnly(0, _random.Next(5, 55), 0);

                for(int i = 0; i < ingredientsCount; ++i)
                {
                    var ingredient = ingredients[_random.Next(0, ingredientsCount - 1)];

                    if(itemIngredients.Any(x => x.IngredientId.Equals(ingredient.IngredientId))) continue;

                    itemIngredients.Add(ingredient);
                }

                for (int iSize = 0; iSize < Constants.Constants.ItemSizeNames.Count; ++iSize)
                {
                    var recipeId = Guid.NewGuid();
                    var recipeIngredients = new List<RecipeIngredient>();
                    
                    foreach(var ingredient in itemIngredients)
                    {                       
                        if(recipeIngredients.Any(x => x.IngredientId.Equals(ingredient.IngredientId))) continue;
                        if(recipeIngredients.Any(x => x.IngredientId.Equals(ingredient.IngredientId) && x.RecipeId.Equals(recipeId))) continue;

                        recipeIngredients.Add(new RecipeIngredient
                        {
                            RecipeId = recipeId,
                            IngredientId = ingredient.IngredientId,
                            IngredientWeight = (float)0.5 * (1 + iSize) * (ingredient.IngredientWeightMeasure == "kg" ? (float)_random.Next(0, 1) + (float)_random.NextDouble() : (float)_random.Next(10, 300)),
                        });
                    }

                    recipes.Add(new Recipe
                    {
                        RecipeId = recipeId,
                        RecipeName = $"Fries_{item}_{Constants.Constants.ItemSizeNames[iSize]}",
                        CookingTime = time,
                        RecipeIngredients = recipeIngredients
                    });

                    items.Add(new Item
                    {
                        ItemId = Guid.NewGuid(),
                        ItemName = item,
                        ItemCategory = "Fries",
                        ItemSize = Constants.Constants.ItemSizeNames[iSize],
                        ItemPrice = price * (decimal) 0.5 * (1 + iSize),
                        RecipeId = recipeId
                    });
                }
            }

            foreach (var item in Constants.Constants.HotDogItemNames)
            {
                var recipeIngredientsCount = _random.Next(5, 12);
                var ingredientsCount = ingredients.Count;
                var itemIngredients = new List<Ingredient>(recipeIngredientsCount);
                var price = (decimal)_random.Next(10, 35) + (decimal)_random.NextDouble();
                var time = new TimeOnly(0, _random.Next(5, 55), 0);

                for(int i = 0; i < ingredientsCount; ++i)
                {
                    var ingredient = ingredients[_random.Next(0, ingredientsCount - 1)];

                    if(itemIngredients.Any(x => x.IngredientId.Equals(ingredient.IngredientId))) continue;

                    itemIngredients.Add(ingredient);
                }

                for (int iSize = 0; iSize < Constants.Constants.ItemSizeNames.Count; ++iSize)
                {
                    var recipeId = Guid.NewGuid();
                    var recipeIngredients = new List<RecipeIngredient>();
                    
                    foreach(var ingredient in itemIngredients)
                    {                       
                        if(recipeIngredients.Any(x => x.IngredientId.Equals(ingredient.IngredientId))) continue;
                        if(recipeIngredients.Any(x => x.IngredientId.Equals(ingredient.IngredientId) && x.RecipeId.Equals(recipeId))) continue;

                        recipeIngredients.Add(new RecipeIngredient
                        {
                            RecipeId = recipeId,
                            IngredientId = ingredient.IngredientId,
                            IngredientWeight = (float)0.5 * (1 + iSize) * (ingredient.IngredientWeightMeasure == "kg" ? (float)_random.Next(0, 1) + (float)_random.NextDouble() : (float)_random.Next(10, 300)),
                        });
                    }

                    recipes.Add(new Recipe
                    {
                        RecipeId = recipeId,
                        RecipeName = $"HotDog_{item}_{Constants.Constants.ItemSizeNames[iSize]}",
                        CookingTime = time,
                        RecipeIngredients = recipeIngredients
                    });

                    items.Add(new Item
                    {
                        ItemId = Guid.NewGuid(),
                        ItemName = item,
                        ItemCategory = "HotDog",
                        ItemSize = Constants.Constants.ItemSizeNames[iSize],
                        ItemPrice = price * (decimal) 0.5 * (1 + iSize),
                        RecipeId = recipeId
                    });
                }
            }

            await recipeService.CreateAllAsync(recipes);
            await itemService.CreateAllAsync(items);
        }
    }
}