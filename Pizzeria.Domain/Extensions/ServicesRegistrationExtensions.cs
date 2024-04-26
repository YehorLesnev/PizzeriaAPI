using Microsoft.Extensions.DependencyInjection;
using Pizzeria.Domain.Repository.AddressRepository;
using Pizzeria.Domain.Repository.CustomerRepository;
using Pizzeria.Domain.Repository.IngredientRepository;
using Pizzeria.Domain.Repository.ItemRepository;
using Pizzeria.Domain.Repository.OrderItemRepository;
using Pizzeria.Domain.Repository.OrderRepository;
using Pizzeria.Domain.Repository.RecipeIngredientRepository;
using Pizzeria.Domain.Repository.RecipeRepository;
using Pizzeria.Domain.Repository.ShiftRepository;
using Pizzeria.Domain.Repository.StaffRepository;
using Pizzeria.Domain.Services.OrderService;
using Pizzeria.Domain.Services.AddressService;
using Pizzeria.Domain.Services.CustomerService;
using Pizzeria.Domain.Services.IngredientService;
using Pizzeria.Domain.Services.ItemService;
using Pizzeria.Domain.Services.StaffServcice;
using Pizzeria.Domain.Services.RecipeService;
using Pizzeria.Domain.Services.ShiftService;
using Pizzeria.Domain.Repository.StatisticsRepository;
using Pizzeria.Domain.Services.Statistics;
using Pizzeria.Domain.Services.LogService;
using Pizzeria.Domain.Repository.LogRepository;

namespace Pizzeria.Domain.Extensions
{
    public static class ServicesRegistrationExtensions
    {
        public static IServiceCollection? RegisterRepositories(this IServiceCollection? serviceCollection)
        {
            if(serviceCollection is  null) return serviceCollection;

            serviceCollection.AddScoped<IAddressRepository, AddressRepository>();
            serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();
            serviceCollection.AddScoped<IIngredientRepository, IngredientRepository>();
            serviceCollection.AddScoped<IItemRepository, ItemRepository>();
            serviceCollection.AddScoped<IOrderItemRepository, OrderItemRepository>();
            serviceCollection.AddScoped<IOrderRepository, OrderRepository>();
            serviceCollection.AddScoped<IRecipeRepository, RecipeRepository>();
            serviceCollection.AddScoped<IRecipeIngredientRepository, RecipeIngredientRepository>();
            serviceCollection.AddScoped<IStaffRepository, StaffRepository>();
            serviceCollection.AddScoped<IShiftRepository, ShiftRepository>();
            serviceCollection.AddScoped<IStatisticsRepository, StatisticsRepository>();
            serviceCollection.AddScoped<ILogRepository, LogRepository>();

            return serviceCollection;
        }

        public static IServiceCollection? RegisterServices(this IServiceCollection? serviceCollection)
        {
            if(serviceCollection is null) return serviceCollection;

            serviceCollection.AddScoped<IOrderService,  OrderService>();
            serviceCollection.AddScoped<IAddressService,  AddressService>();
            serviceCollection.AddScoped<ICustomerService, CustomerService>();
            serviceCollection.AddScoped<IIngredientService, IngredientService>();
            serviceCollection.AddScoped<IItemService, ItemService>();
            serviceCollection.AddScoped<IStaffService, StaffService>();
            serviceCollection.AddScoped<IRecipeService, RecipeService>();
            serviceCollection.AddScoped<IShiftService, ShiftService>();
            serviceCollection.AddScoped<IStatisticsService, StatisticsService>();
            serviceCollection.AddScoped<ILogService, LogService>();

            return serviceCollection;
        }
    }
}
