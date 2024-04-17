using Bogus;
using Pizzeria.Domain.Models;
using Pizzeria.Domain.Services.CustomerService;
using Pizzeria.Domain.Services.IngredientService;
using Pizzeria.Domain.Services.ItemService;
using Pizzeria.Domain.Services.OrderService;
using Pizzeria.Domain.Services.RecipeService;
using Pizzeria.Domain.Services.ShiftService;
using Pizzeria.Domain.Services.StaffServcice;

namespace Pizzeria.Domain.Seeder
{
    public class Seeder(
    IIngredientService ingredientService,
    IRecipeService recipeService,
    IItemService itemService,
    ICustomerService customerService,
    IStaffService staffService,
    IShiftService shiftService,
    IOrderService orderService)
        : ISeeder
    {
        private readonly Random _random = new Random();

        private Address GetRandomAddressObject()
        {
            var random = new Random();
            var isSecondAddress = random.Next(0, 15);

            return new Faker<Address>()
                .RuleFor(x => x.Address1, f => f.Address.StreetAddress())
                .RuleFor(x => x.Address2, f => isSecondAddress == 0 ? f.Address.StreetAddress() : null)
                .RuleFor(x => x.City, f => f.Address.City())
                .RuleFor(x => x.Zipcode, f => f.Address.ZipCode());
        }

        private async Task<List<Customer>> GetCustomerListAsync()
        {
            return await Task.Run(() =>
            {
                var customerList = new List<Customer>();
                var random = new Random();

                var numOfCustomers = random.Next(Constants.SeederConstants.MinNumOfCustomers, Constants.SeederConstants.MaxNumOfCustomers);

                for (int i = 0; i < numOfCustomers; ++i)
                {
                    customerList.Add(new Faker<Customer>()
                        .RuleFor(x => x.FirstName, f => f.Name.FirstName())
                        .RuleFor(x => x.LastName, f => f.Name.LastName())
                        .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumberFormat()));
                }

                return customerList;
            });
        }

        private async Task<List<Staff>> GetStaffListAsync()
        {
            return await Task.Run(() =>
            {
                var staffList = new List<Staff>();
                var random = new Random();

                for (int i = 0; i < Constants.SeederConstants.NumOfStaff; ++i)
                {
                    var iPosition = random.Next(0, Constants.SeederConstants.StaffPositions.Count);

                    staffList.Add(new Faker<Staff>()
                        .RuleFor(x => x.FirstName, f => f.Name.FirstName())
                        .RuleFor(x => x.LastName, f => f.Name.LastName())
                        .RuleFor(x => x.Position, Constants.SeederConstants.StaffPositions[iPosition])
                        .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumberFormat())
                        .RuleFor(x => x.HourlyRate, Constants.SeederConstants.StaffPayRates[iPosition]));
                }

                return staffList;
            });
        }

        public async Task Seed()
        {
            var ingredients = new List<Ingredient>();
            var recipes = new List<Recipe>();
            var items = new List<Item>();

            var customers = GetCustomerListAsync();
            var staff = GetStaffListAsync();

            // Ingredients
            foreach (var ingrName in Constants.SeederConstants.IngredientNames)
            {
                var isKg = _random.Next(0, 2);

                ingredients.Add(new Faker<Ingredient>()
                    
                    .RuleFor(x => x.IngredientName, ingrName)
                    .RuleFor(x => x.IngredientWeightMeasure, isKg == 0 ? "kg" : "g")
                    .RuleFor(x => x.IngredientPrice, f => isKg == 0 ? f.Finance.Amount(1, 3) : f.Finance.Amount(0.01m, 0.1m))
                    .RuleFor(x => x.QuantityInStock, f => isKg == 0 ? f.Random.Number(1000, 100000) : f.Random.Number(500000, 10000000)));
            }

            await ingredientService.CreateAllAsync(ingredients);

            // Items

            // Pizza
            foreach (var item in Constants.SeederConstants.PizzaItemNames)
            {
                const string itemCategory = "Pizza";

                var recipeIngredientsCount = _random.Next(5, 12);
                var ingredientsCount = ingredients.Count;
                var itemIngredients = new List<Ingredient>(recipeIngredientsCount);

                for (int i = 0; i < recipeIngredientsCount; ++i)
                {
                    var ingredient = ingredients[_random.Next(0, ingredientsCount)];

                    if (itemIngredients.Any(x => x.IngredientId.Equals(ingredient.IngredientId))) continue;

                    itemIngredients.Add(ingredient);
                }

                for (int iSize = 0; iSize < Constants.SeederConstants.ItemSizeNames.Count; ++iSize)
                {
                    var recipeId = Guid.NewGuid();
                    var recipeIngredients = new List<RecipeIngredient>();

                    foreach (var ingredient in itemIngredients)
                    {
                        if (recipeIngredients.Any(x => x.IngredientId.Equals(ingredient.IngredientId))) continue;
                        if (recipeIngredients.Any(x => x.IngredientId.Equals(ingredient.IngredientId) && x.RecipeId.Equals(recipeId))) continue;

                        recipeIngredients.Add(new Faker<RecipeIngredient>()
                            .RuleFor(x => x.RecipeId, recipeId)
                            .RuleFor(x => x.IngredientId, ingredient.IngredientId)
                            .RuleFor(x => x.IngredientWeight, f =>
                            (float)(0.1 * (1 + iSize) * (ingredient.IngredientWeightMeasure == "kg" ? f.Random.Number(0, 1) + f.Random.Float() : f.Random.Number(10, 200)))));
                    }

                    // Calculate overall item price by ingredients
                    var ingredientsPrice = recipeIngredients
                        .Select(x =>
                        (decimal)x.IngredientWeight * ingredients.FirstOrDefault(i => i.IngredientId.Equals(x.IngredientId))!.IngredientPrice)
                        .Aggregate((a, b) => a + b);

                    recipes.Add(new Faker<Recipe>()
                        .RuleFor(x => x.RecipeId, recipeId)
                        .RuleFor(x => x.RecipeName, $"{itemCategory}_{item}_{Constants.SeederConstants.ItemSizeNames[iSize]}")
                        .RuleFor(x => x.CookingTime, f => new TimeOnly(0, f.Random.Number(5, 55), 0))
                        .RuleFor(x => x.RecipeIngredients, recipeIngredients));

                    items.Add(new Faker<Item>()
                        .RuleFor(x => x.ItemName, item)
                        .RuleFor(x => x.ItemCategory, itemCategory)
                        .RuleFor(x => x.ItemSize, Constants.SeederConstants.ItemSizeNames[iSize])
                        .RuleFor(x => x.ItemPrice, f => ingredientsPrice + f.Random.Number(5, 10) + f.Random.Decimal())
                        .RuleFor(x => x.RecipeId, recipeId)
                        .RuleFor(x => x.ImagePath, $"static/images/items/{itemCategory}/{item.Replace(" ", "_")}.{Constants.SeederConstants.ImageExtension}"));
                }
            }

            // Burger
            foreach (var item in Constants.SeederConstants.BurgerItemNames)
            {
                const string itemCategory = "Burger";

                var recipeIngredientsCount = _random.Next(5, 12);
                var ingredientsCount = ingredients.Count;
                var itemIngredients = new List<Ingredient>(recipeIngredientsCount);

                for (int i = 0; i < recipeIngredientsCount; ++i)
                {
                    var ingredient = ingredients[_random.Next(0, ingredientsCount)];

                    if (itemIngredients.Any(x => x.IngredientId.Equals(ingredient.IngredientId))) continue;

                    itemIngredients.Add(ingredient);
                }

                for (int iSize = 0; iSize < Constants.SeederConstants.ItemSizeNames.Count; ++iSize)
                {
                    var recipeId = Guid.NewGuid();
                    var recipeIngredients = new List<RecipeIngredient>();

                    foreach (var ingredient in itemIngredients)
                    {
                        if (recipeIngredients.Any(x => x.IngredientId.Equals(ingredient.IngredientId))) continue;
                        if (recipeIngredients.Any(x => x.IngredientId.Equals(ingredient.IngredientId) && x.RecipeId.Equals(recipeId))) continue;

                        recipeIngredients.Add(new Faker<RecipeIngredient>()
                            .RuleFor(x => x.RecipeId, recipeId)
                            .RuleFor(x => x.IngredientId, ingredient.IngredientId)
                            .RuleFor(x => x.IngredientWeight, f =>
                            (float)(0.1 * (1 + iSize) * (ingredient.IngredientWeightMeasure == "kg" ? f.Random.Number(0, 1) + f.Random.Float() : f.Random.Number(10, 200)))));
                    }

                    // Calculate overall item price by ingredients
                    var ingredientsPrice = recipeIngredients
                        .Select(x =>
                        (decimal)x.IngredientWeight * ingredients.FirstOrDefault(i => i.IngredientId.Equals(x.IngredientId))!.IngredientPrice)
                        .Aggregate((a, b) => a + b);

                    recipes.Add(new Faker<Recipe>()
                        .RuleFor(x => x.RecipeId, recipeId)
                        .RuleFor(x => x.RecipeName, $"{itemCategory}_{item}_{Constants.SeederConstants.ItemSizeNames[iSize]}")
                        .RuleFor(x => x.CookingTime, f => new TimeOnly(0, f.Random.Number(5, 55), 0))
                        .RuleFor(x => x.RecipeIngredients, recipeIngredients));

                    items.Add(new Faker<Item>()
                        .RuleFor(x => x.ItemName, item)
                        .RuleFor(x => x.ItemCategory, itemCategory)
                        .RuleFor(x => x.ItemSize, Constants.SeederConstants.ItemSizeNames[iSize])
                        .RuleFor(x => x.ItemPrice, f => ingredientsPrice + f.Random.Number(5, 10) + f.Random.Decimal())
                        .RuleFor(x => x.RecipeId, recipeId)
                        .RuleFor(x => x.ImagePath, $"static/images/items/{itemCategory}/{item.Replace(" ", "_")}.{Constants.SeederConstants.ImageExtension}"));
                }
            }

            // Fries
            foreach (var item in Constants.SeederConstants.FrenchFriesItemNames)
            {
                const string itemCategory = "Fries";

                var recipeIngredientsCount = _random.Next(5, 12);
                var ingredientsCount = ingredients.Count;
                var itemIngredients = new List<Ingredient>(recipeIngredientsCount);

                for (int i = 0; i < recipeIngredientsCount; ++i)
                {
                    var ingredient = ingredients[_random.Next(0, ingredientsCount)];

                    if (itemIngredients.Any(x => x.IngredientId.Equals(ingredient.IngredientId))) continue;

                    itemIngredients.Add(ingredient);
                }

                for (int iSize = 0; iSize < Constants.SeederConstants.ItemSizeNames.Count; ++iSize)
                {
                    var recipeId = Guid.NewGuid();
                    var recipeIngredients = new List<RecipeIngredient>();

                    foreach (var ingredient in itemIngredients)
                    {
                        if (recipeIngredients.Any(x => x.IngredientId.Equals(ingredient.IngredientId))) continue;
                        if (recipeIngredients.Any(x => x.IngredientId.Equals(ingredient.IngredientId) && x.RecipeId.Equals(recipeId))) continue;

                        recipeIngredients.Add(new Faker<RecipeIngredient>()
                            .RuleFor(x => x.RecipeId, recipeId)
                            .RuleFor(x => x.IngredientId, ingredient.IngredientId)
                            .RuleFor(x => x.IngredientWeight, f =>
                            (float)(0.1 * (1 + iSize) * (ingredient.IngredientWeightMeasure == "kg" ? f.Random.Number(0, 1) + f.Random.Float() : f.Random.Number(10, 200)))));
                    }

                    // Calculate overall item price by ingredients
                    var ingredientsPrice = recipeIngredients
                        .Select(x =>
                        (decimal)x.IngredientWeight * ingredients.FirstOrDefault(i => i.IngredientId.Equals(x.IngredientId))!.IngredientPrice)
                        .Aggregate((a, b) => a + b);

                    recipes.Add(new Faker<Recipe>()
                        .RuleFor(x => x.RecipeId, recipeId)
                        .RuleFor(x => x.RecipeName, $"{itemCategory}_{item}_{Constants.SeederConstants.ItemSizeNames[iSize]}")
                        .RuleFor(x => x.CookingTime, f => new TimeOnly(0, f.Random.Number(5, 55), 0))
                        .RuleFor(x => x.RecipeIngredients, recipeIngredients));

                    items.Add(new Faker<Item>()
                        .RuleFor(x => x.ItemName, item)
                        .RuleFor(x => x.ItemCategory, itemCategory)
                        .RuleFor(x => x.ItemSize, Constants.SeederConstants.ItemSizeNames[iSize])
                        .RuleFor(x => x.ItemPrice, f => ingredientsPrice + f.Random.Number(5, 10) + f.Random.Decimal())
                        .RuleFor(x => x.RecipeId, recipeId)
                        .RuleFor(x => x.ImagePath, $"static/images/items/{itemCategory}/{item.Replace(" ", "_")}.{Constants.SeederConstants.ImageExtension}"));
                }
            }

            // Hot Dog
            foreach (var item in Constants.SeederConstants.HotDogItemNames)
            {
                const string itemCategory = "HotDog";

                var recipeIngredientsCount = _random.Next(5, 12);
                var ingredientsCount = ingredients.Count;
                var itemIngredients = new List<Ingredient>(recipeIngredientsCount);

                for (int i = 0; i < recipeIngredientsCount; ++i)
                {
                    var ingredient = ingredients[_random.Next(0, ingredientsCount)];

                    if (itemIngredients.Any(x => x.IngredientId.Equals(ingredient.IngredientId))) continue;

                    itemIngredients.Add(ingredient);
                }

                for (int iSize = 0; iSize < Constants.SeederConstants.ItemSizeNames.Count; ++iSize)
                {
                    var recipeId = Guid.NewGuid();
                    var recipeIngredients = new List<RecipeIngredient>();

                    foreach (var ingredient in itemIngredients)
                    {
                        if (recipeIngredients.Any(x => x.IngredientId.Equals(ingredient.IngredientId))) continue;
                        if (recipeIngredients.Any(x => x.IngredientId.Equals(ingredient.IngredientId) && x.RecipeId.Equals(recipeId))) continue;

                        recipeIngredients.Add(new Faker<RecipeIngredient>()
                            .RuleFor(x => x.RecipeId, recipeId)
                            .RuleFor(x => x.IngredientId, ingredient.IngredientId)
                            .RuleFor(x => x.IngredientWeight, f =>
                            (float)(0.1 * (1 + iSize) * (ingredient.IngredientWeightMeasure == "kg" ? f.Random.Number(1) + f.Random.Float() : f.Random.Number(10, 200)))));
                    }

                    // Calculate overall item price by ingredients
                    var ingredientsPrice = recipeIngredients
                        .Select(x =>
                        (decimal)x.IngredientWeight * ingredients.FirstOrDefault(i => i.IngredientId.Equals(x.IngredientId))!.IngredientPrice)
                        .Aggregate((a, b) => a + b);

                    recipes.Add(new Faker<Recipe>()
                        .RuleFor(x => x.RecipeId, recipeId)
                        .RuleFor(x => x.RecipeName, $"{itemCategory}_{item}_{Constants.SeederConstants.ItemSizeNames[iSize]}")
                        .RuleFor(x => x.CookingTime, f => new TimeOnly(0, f.Random.Number(5, 55), 0))
                        .RuleFor(x => x.RecipeIngredients, recipeIngredients));

                    items.Add(new Faker<Item>()
                        .RuleFor(x => x.ItemName, item)
                        .RuleFor(x => x.ItemCategory, itemCategory)
                        .RuleFor(x => x.ItemSize, Constants.SeederConstants.ItemSizeNames[iSize])
                        .RuleFor(x => x.ItemPrice, f => ingredientsPrice + f.Random.Number(5, 10) + f.Random.Decimal())
                        .RuleFor(x => x.RecipeId, recipeId)
                        .RuleFor(x => x.ImagePath, $"static/images/items/{itemCategory}/{item.Replace(" ", "_")}.{Constants.SeederConstants.ImageExtension}"));
                }
            }

            await recipeService.CreateAllAsync(recipes);
            await itemService.CreateAllAsync(items);

            await staffService.CreateAllAsync(staff.Result);
            await customerService.CreateAllAsync(customers.Result);

            // Shifts
            var shifts = new List<Shift>();

            // Orders
            var orders = new List<Order>();

            var startDate = Constants.SeederConstants.OrdersStartDate;
            var startWorkingDayTime = Domain.Constants.Constants.ShiftStartTime;
            var endWorkingDayTime = Domain.Constants.Constants.ShiftEndTime;
            var random = new Random();

            var itemsCount = items.Count;

            // Create shifts
            for (var date = DateOnly.FromDateTime(startDate); date <= DateOnly.FromDateTime(DateTime.Now); date = date.AddDays(1))
            {
                var shiftId = Guid.NewGuid();
                var shiftStaffCount = random.Next(4, 9);
                var shiftStaff = new List<ShiftStaff>();

                for (int i = 0; i < shiftStaffCount; ++i)
                {
                    shiftStaff.Add(new Faker<ShiftStaff>()
                        .RuleFor(x => x.ShiftId, shiftId)
                        .RuleFor(x => x.StaffId, f =>
                             f.PickRandom(staff.Result.Where(s => !shiftStaff.Select(sh => sh.StaffId).Contains(s.StaffId))).StaffId));
                }

                shifts.Add(new Faker<Shift>()
                    .RuleFor(x => x.ShiftId, shiftId)
                    .RuleFor(x => x.ShiftDate, date)
                    .RuleFor(x => x.ShiftStaff, shiftStaff));
            }

            // Create orders
            for (var date = startDate; date < DateTime.Now; date = date.AddMinutes(random.Next(1, 5)))
            {
                if (TimeOnly.FromDateTime(date) > endWorkingDayTime)
                {
                    date = new DateTime(DateOnly.FromDateTime(date).AddDays(1), startWorkingDayTime);
                    continue;
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

                    orderItems.Add(new Faker<OrderItem>()
                        .RuleFor(x => x.OrderId, orderId)
                        .RuleFor(x => x.ItemId, itemId)
                        .RuleFor(x => x.Quantity, f => f.Random.Number(1, 7)));
                }

                orders.Add(new Faker<Order>()
                    .RuleFor(x => x.OrderId, orderId)
                    .RuleFor(x => x.Date, date)
                    .RuleFor(x => x.StaffId, f =>
                        f.PickRandom(shifts.FirstOrDefault(s => s.ShiftDate.Equals(DateOnly.FromDateTime(date))).ShiftStaff).StaffId)
                    .RuleFor(x => x.CustomerId, f => f.PickRandom(customers.Result).Id)
                    .RuleFor(x => x.Delivery, isDelivery == 0)
                    .RuleFor(x => x.DeliveryAddress, deliveryAddress)
                    .RuleFor(x => x.DeliveryAddressId, deliveryAddress?.AddressId)
                    .RuleFor(x => x.OrderItems, orderItems));
            }

            await orderService.CreateAllAsync(orders);

            await shiftService.CreateAllAsync(shifts);

            // Clean up
            ingredients = null;
            recipes = null;
            items = null;
            customers = null;
            staff = null;
        }
    }
}