﻿using Microsoft.Extensions.DependencyInjection;
using Pizzeria.Domain.Repository.Interfaces;
using System.Reflection;
using Pizzeria.Domain.Repository.Implementations;
using Pizzeria.Domain.Services.OrderService;
using Pizzeria.Domain.Services.AddressService;

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
            serviceCollection.AddScoped<IInventoryRepository, InventoryRepository>();
            serviceCollection.AddScoped<IItemRepository, ItemRepository>();
            serviceCollection.AddScoped<IOrderItemRepository, OrderItemRepository>();
            serviceCollection.AddScoped<IOrderRepository, OrderRepository>();
            serviceCollection.AddScoped<IRecipeRepository, RecipeRepository>();
            serviceCollection.AddScoped<IRecipeIngredientRepository, RecipeIngredientRepository>();
            serviceCollection.AddScoped<IStaffRepository, StaffRepository>();

            return serviceCollection;
        }

        public static IServiceCollection? RegisterServices(this IServiceCollection? serviceCollection)
        {
            if(serviceCollection is null) return serviceCollection;

            serviceCollection.AddScoped<IOrderService,  OrderService>();
            serviceCollection.AddScoped<IAddressService,  AddressService>();

            return serviceCollection;
        }
    }
}
