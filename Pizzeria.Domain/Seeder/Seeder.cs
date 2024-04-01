using Pizzeria.Domain.Models;
using Pizzeria.Domain.Services.CustomerService;
using Pizzeria.Domain.Services.IngredientService;
using Pizzeria.Domain.Services.ItemService;
using Pizzeria.Domain.Services.OrderService;
using Pizzeria.Domain.Services.RecipeService;
using Pizzeria.Domain.Services.StaffServcice;

namespace Pizzeria.Domain.Seeder
{
    public class Seeder(
    IIngredientService ingredientService,
    IRecipeService recipeService,
    IItemService itemService,
    ICustomerService customerService,
    IStaffService staffService,
    IOrderService orderService)
        : ISeeder
    {
        private readonly Random _random = new Random();

        private string GetRandPhoneNum()
        {
            var random = new Random();
            var phoneNum = "";

            for (int i = 0; i < 10; ++i)
            {
                phoneNum += random.Next(0, 10);
            }

            return phoneNum;
        }

        private string GetRandomAddress()
        {
            var alphabet = "ABCDEFGHIJKLMNOPQRSTUVQXYZ";
            var random = new Random();
            var maxWordLength = random.Next(5, 15);
            var maxWords = random.Next(1, 4);
            var result = "";

            for (int i = 0; i < maxWords; ++i)
            {
                for (int j = 0; j < maxWordLength; ++j)
                {
                    result += alphabet[random.Next(0, alphabet.Length)];
                }

                result += " ";
            }

            return result;
        }

        private string GetRandomCity()
        {
            var alphabet = "ABCDEFGHIJKLMNOPQRSTUVQXYZ";
            var random = new Random();
            var maxWordLength = random.Next(4, 10);
            var maxWords = random.Next(1, 3);
            var result = "";

            for (int i = 0; i < maxWords; ++i)
            {
                for (int j = 0; j < maxWordLength; ++j)
                {
                    result += alphabet[random.Next(0, alphabet.Length)];
                }

                result += " ";
            }

            return result;
        }

        private string GetRandomZipcode()
        {
            var random = new Random();
            var result = "";

            for (int i = 0; i < 10; ++i)
            {
                result += random.Next(0, 10);
            }

            return result;
        }

        private Address GetRandomAddressObject()
        {
            var random = new Random();
            var isSecondAddress = random.Next(0, 15);

            return new Address
            {
                AddressId = Guid.NewGuid(),
                Address1 = GetRandomAddress(),
                Address2 = isSecondAddress == 0 ? GetRandomAddress() : null,
                City = GetRandomCity(),
                Zipcode = GetRandomZipcode(),
            };
        }

        private async Task<List<Customer>> GetCustomerListAsync()
        {
            return await Task.Run(() =>
            {
                var customerList = new List<Customer>();
                var random = new Random();

                var numOfCustomers = random.Next(Constants.Constants.MinNumOfCustomers, Constants.Constants.MaxNumOfCustomers);

                for (int i = 0; i < numOfCustomers; ++i)
                {
                    var isLastName = random.Next(0, 5);
                    var isPhoneNum = random.Next(0, 10);
                    var peopleNamesCount = Constants.Constants.PeopleNames.Count;
                    var peopleLastNamesCount = Constants.Constants.PeopleLastNames.Count;

                    customerList.Add(new Customer
                    {
                        CustomerId = Guid.NewGuid(),
                        FirstName = Constants.Constants.PeopleNames[random.Next(0, peopleNamesCount)],
                        LastName = isLastName == 0 ? Constants.Constants.PeopleLastNames[random.Next(0, peopleLastNamesCount)] : null,
                        PhoneNumber = isPhoneNum == 0 ? GetRandPhoneNum() : null
                    });
                }

                return customerList;
            });
        }

        private async Task<List<Staff>> GetStaffListAsync()
        {
            return await Task.Run(() =>
            {
                var customerList = new List<Staff>();
                var random = new Random();

                for (int i = 0; i < Constants.Constants.NumOfStaff; ++i)
                {
                    var peopleNamesCount = Constants.Constants.PeopleNames.Count;
                    var peopleLastNamesCount = Constants.Constants.PeopleLastNames.Count;
                    var iPosition = random.Next(0, Constants.Constants.StaffPositions.Count);

                    customerList.Add(new Staff
                    {
                        StaffId = Guid.NewGuid(),
                        FirstName = Constants.Constants.PeopleNames[random.Next(0, peopleNamesCount)],
                        LastName = Constants.Constants.PeopleLastNames[random.Next(0, peopleLastNamesCount)],
                        Position = Constants.Constants.StaffPositions[iPosition],
                        HourlyRate = Constants.Constants.StaffPayRates[iPosition]
                    });
                }

                return customerList;
            });
        }

        public async Task Seed()
        {
            var ingredients = new List<Ingredient>();
            var recipes = new List<Recipe>();
            var items = new List<Item>();

            var customers = GetCustomerListAsync();
            var staff = GetStaffListAsync();

            foreach (var ingrName in Constants.Constants.IngredientNames)
            {
                var isKg = _random.Next(0, 2);

                ingredients.Add(new Ingredient
                {
                    IngredientId = Guid.NewGuid(),
                    IngredientName = ingrName,
                    IngredientWeightMeasure = isKg == 0 ? "kg" : "g",
                    IngredientPrice = isKg == 0 ? (decimal)_random.Next(1, 3) + (decimal)_random.NextDouble() : (decimal)(_random.NextDouble() * 0.01),
                    QuantityInStock = isKg == 0 ? _random.Next(10, 50) : _random.Next(500, 5000),
                });
            }

            await ingredientService.CreateAllAsync(ingredients);

            foreach (var item in Constants.Constants.PizzaItemNames)
            {
                var recipeIngredientsCount = _random.Next(5, 12);
                var ingredientsCount = ingredients.Count;
                var itemIngredients = new List<Ingredient>(recipeIngredientsCount);
                var priceExtra = (decimal)_random.Next(5, 10) + (decimal)(_random.Next(0, 2) == 0 ? 0.49 : 0.99);
                var time = new TimeOnly(0, _random.Next(5, 55), 0);

                for (int i = 0; i < ingredientsCount; ++i)
                {
                    var ingredient = ingredients[_random.Next(0, ingredientsCount)];

                    if (itemIngredients.Any(x => x.IngredientId.Equals(ingredient.IngredientId))) continue;

                    itemIngredients.Add(ingredient);
                }

                for (int iSize = 0; iSize < Constants.Constants.ItemSizeNames.Count; ++iSize)
                {
                    var recipeId = Guid.NewGuid();
                    var recipeIngredients = new List<RecipeIngredient>();

                    foreach (var ingredient in itemIngredients)
                    {
                        if (recipeIngredients.Any(x => x.IngredientId.Equals(ingredient.IngredientId))) continue;
                        if (recipeIngredients.Any(x => x.IngredientId.Equals(ingredient.IngredientId) && x.RecipeId.Equals(recipeId))) continue;

                        recipeIngredients.Add(new RecipeIngredient
                        {
                            RecipeId = recipeId,
                            IngredientId = ingredient.IngredientId,
                            IngredientWeight = (float)0.1 * (1 + iSize) * (ingredient.IngredientWeightMeasure == "kg" ? (float)_random.Next(0, 1) + (float)_random.NextDouble() : (float)_random.Next(10, 300)),
                        });
                    }

                    // Calculate overall item price by ingredients
                    var ingredientsPrice = recipeIngredients
                        .Select(x =>
                        (decimal)x.IngredientWeight * ingredients.FirstOrDefault(i => i.IngredientId.Equals(x.IngredientId))!.IngredientPrice)
                        .Aggregate((a, b) => a + b);

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
                        ItemPrice = ingredientsPrice + priceExtra,
                        RecipeId = recipeId
                    });
                }
            }

            foreach (var item in Constants.Constants.BurgerItemNames)
            {
                var recipeIngredientsCount = _random.Next(5, 12);
                var ingredientsCount = ingredients.Count;
                var itemIngredients = new List<Ingredient>(recipeIngredientsCount);
                var priceExtra = (decimal)_random.Next(5, 10) + (decimal)(_random.Next(0, 2) == 0 ? 0.49 : 0.99);
                var time = new TimeOnly(0, _random.Next(5, 55), 0);

                for (int i = 0; i < ingredientsCount; ++i)
                {
                    var ingredient = ingredients[_random.Next(0, ingredientsCount)];

                    if (itemIngredients.Any(x => x.IngredientId.Equals(ingredient.IngredientId))) continue;

                    itemIngredients.Add(ingredient);
                }

                for (int iSize = 0; iSize < Constants.Constants.ItemSizeNames.Count; ++iSize)
                {
                    var recipeId = Guid.NewGuid();
                    var recipeIngredients = new List<RecipeIngredient>();

                    foreach (var ingredient in itemIngredients)
                    {
                        if (recipeIngredients.Any(x => x.IngredientId.Equals(ingredient.IngredientId))) continue;
                        if (recipeIngredients.Any(x => x.IngredientId.Equals(ingredient.IngredientId) && x.RecipeId.Equals(recipeId))) continue;

                        recipeIngredients.Add(new RecipeIngredient
                        {
                            RecipeId = recipeId,
                            IngredientId = ingredient.IngredientId,
                            IngredientWeight = (float)0.1 * (1 + iSize) * (ingredient.IngredientWeightMeasure == "kg" ? (float)_random.Next(0, 1) + (float)_random.NextDouble() : (float)_random.Next(10, 300)),
                        });
                    }

                    // Calculate overall item price by ingredients
                    var ingredientsPrice = recipeIngredients
                        .Select(x =>
                        (decimal)x.IngredientWeight * ingredients.FirstOrDefault(i => i.IngredientId.Equals(x.IngredientId))!.IngredientPrice)
                        .Aggregate((a, b) => a + b);

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
                        ItemPrice = ingredientsPrice + priceExtra,
                        RecipeId = recipeId
                    });
                }
            }

            foreach (var item in Constants.Constants.FrenchFriesItemNames)
            {
                var recipeIngredientsCount = _random.Next(5, 12);
                var ingredientsCount = ingredients.Count;
                var itemIngredients = new List<Ingredient>(recipeIngredientsCount);
                var priceExtra = (decimal)_random.Next(5, 10) + (decimal)(_random.Next(0, 2) == 0 ? 0.49 : 0.99);
                var time = new TimeOnly(0, _random.Next(5, 55), 0);

                for (int i = 0; i < ingredientsCount; ++i)
                {
                    var ingredient = ingredients[_random.Next(0, ingredientsCount)];

                    if (itemIngredients.Any(x => x.IngredientId.Equals(ingredient.IngredientId))) continue;

                    itemIngredients.Add(ingredient);
                }

                for (int iSize = 0; iSize < Constants.Constants.ItemSizeNames.Count; ++iSize)
                {
                    var recipeId = Guid.NewGuid();
                    var recipeIngredients = new List<RecipeIngredient>();

                    foreach (var ingredient in itemIngredients)
                    {
                        if (recipeIngredients.Any(x => x.IngredientId.Equals(ingredient.IngredientId))) continue;
                        if (recipeIngredients.Any(x => x.IngredientId.Equals(ingredient.IngredientId) && x.RecipeId.Equals(recipeId))) continue;

                        recipeIngredients.Add(new RecipeIngredient
                        {
                            RecipeId = recipeId,
                            IngredientId = ingredient.IngredientId,
                            IngredientWeight = (float)0.1 * (1 + iSize) * (ingredient.IngredientWeightMeasure == "kg" ? (float)_random.Next(0, 1) + (float)_random.NextDouble() : (float)_random.Next(10, 300)),
                        });
                    }

                    // Calculate overall item price by ingredients
                    var ingredientsPrice = recipeIngredients
                        .Select(x =>
                        (decimal)x.IngredientWeight * ingredients.FirstOrDefault(i => i.IngredientId.Equals(x.IngredientId))!.IngredientPrice)
                        .Aggregate((a, b) => a + b);

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
                        ItemPrice = ingredientsPrice + priceExtra,
                        RecipeId = recipeId
                    });
                }
            }

            foreach (var item in Constants.Constants.HotDogItemNames)
            {
                var recipeIngredientsCount = _random.Next(5, 12);
                var ingredientsCount = ingredients.Count;
                var itemIngredients = new List<Ingredient>(recipeIngredientsCount);
                var priceExtra = (decimal)_random.Next(5, 10) + (decimal)(_random.Next(0, 2) == 0 ? 0.49 : 0.99);
                var time = new TimeOnly(0, _random.Next(5, 55), 0);

                for (int i = 0; i < ingredientsCount; ++i)
                {
                    var ingredient = ingredients[_random.Next(0, ingredientsCount)];

                    if (itemIngredients.Any(x => x.IngredientId.Equals(ingredient.IngredientId))) continue;

                    itemIngredients.Add(ingredient);
                }

                for (int iSize = 0; iSize < Constants.Constants.ItemSizeNames.Count; ++iSize)
                {
                    var recipeId = Guid.NewGuid();
                    var recipeIngredients = new List<RecipeIngredient>();

                    foreach (var ingredient in itemIngredients)
                    {
                        if (recipeIngredients.Any(x => x.IngredientId.Equals(ingredient.IngredientId))) continue;
                        if (recipeIngredients.Any(x => x.IngredientId.Equals(ingredient.IngredientId) && x.RecipeId.Equals(recipeId))) continue;

                        recipeIngredients.Add(new RecipeIngredient
                        {
                            RecipeId = recipeId,
                            IngredientId = ingredient.IngredientId,
                            IngredientWeight = (float)0.1 * (1 + iSize) * (ingredient.IngredientWeightMeasure == "kg" ? (float)_random.Next(0, 1) + (float)_random.NextDouble() : (float)_random.Next(10, 300)),
                        });
                    }

                    // Calculate overall item price by ingredients
                    var ingredientsPrice = recipeIngredients
                        .Select(x =>
                        (decimal)x.IngredientWeight * ingredients.FirstOrDefault(i => i.IngredientId.Equals(x.IngredientId))!.IngredientPrice)
                        .Aggregate((a, b) => a + b);

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
                        ItemPrice = ingredientsPrice + priceExtra,
                        RecipeId = recipeId
                    });
                }
            }

            await recipeService.CreateAllAsync(recipes);
            await itemService.CreateAllAsync(items);

            await staffService.CreateAllAsync(staff.Result);
            await customerService.CreateAllAsync(customers.Result);

            // Orders
            var orders = new List<Order>();

            var startDate = Constants.Constants.OrdersStartDate;
            var startWorkingDayTime = new TimeOnly(8, 0, 0);
            var endWorkingDayTime = new TimeOnly(22, 0, 0);
            var random = new Random();

            var staffCount = staff.Result.Count;
            var customersCount = customers.Result.Count;
            var itemsCount = items.Count;

            for (var date = startDate; date < DateTime.Now; date = date.AddMinutes(random.Next(1, 5)))
            {
                if (TimeOnly.FromDateTime(date) > endWorkingDayTime)
                {
                    date = new DateTime(DateOnly.FromDateTime(date).AddDays(1), startWorkingDayTime);
                }

                var orderId = Guid.NewGuid();

                var isDelivery = random.Next(0, 10);
                var deliveryAddress = isDelivery == 0 ? GetRandomAddressObject() : null;

                var orderItems = new List<OrderItem>();
                var orderItemsCount = random.Next(1, 7);

                for (int i = 0; i < orderItemsCount; ++i)
                {
                    var itemId = items[random.Next(0, itemsCount)].ItemId;

                    if (orderItems.Any(x => x.OrderId.Equals(orderId) && x.ItemId.Equals(itemId)))
                    {
                        --i;
                        continue;
                    }

                    orderItems.Add(new OrderItem
                    {
                        OrderId = orderId,
                        ItemId = itemId,
                        Quantity = random.Next(1, 7),
                    });
                }

                orders.Add(new Order
                {
                    OrderId = orderId,
                    Date = date,
                    StaffId = staff.Result[random.Next(0, staffCount)].StaffId,
                    CustomerId = customers.Result[random.Next(0, customersCount)].CustomerId,
                    Delivery = isDelivery == 0,
                    DeliveryAddress = deliveryAddress,
                    DeliveryAddressId = deliveryAddress?.AddressId,
                    OrderItems = orderItems
                });
            }

            await orderService.CreateAllAsync(orders);

            ingredients = null;
            recipes = null;
            items = null;
            customers = null;
            staff = null;
        }
    }
}