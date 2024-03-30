using Pizzeria.Domain.Dto;
using Pizzeria.Domain.Models;
using Riok.Mapperly.Abstractions;

namespace Pizzeria.Domain.Mapper
{
    [Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
    public static partial class Mappers
    {
        public static partial AddressDto MapAddressToDto(Address address);
        public static partial CustomerDto MapCustomerToDto(Customer customer);
        public static partial IngredientDto MapIngredientToDto(Ingredient ingredient);
        public static partial InventoryDto MapInventoryToDto(Inventory inventory);
        public static partial ItemDto MapItemToDto(Item item);
        public static partial OrderDto MapOrderToDto(Order order);
        public static partial OrderItemDto MapOrderItemToDto(OrderItem orderItem);
        public static partial RecipeDto MapRecipeToDto(Recipe recipe);
        public static partial RecipeIngredientDto MapRecipeIngredientToDto(RecipeIngredient recipeIngredient);
        public static partial StaffDto MapStaffToDto(Staff staff);

        public static partial IEnumerable<AddressDto> MapAddressToDto(IEnumerable<Address> addresses);
        public static partial IEnumerable<CustomerDto> MapCustomerToDto(IEnumerable<Customer> customers);
        public static partial IEnumerable<IngredientDto> MapIngredientToDto(IEnumerable<Ingredient> ingredients);
        public static partial IEnumerable<InventoryDto> MapInventoryToDto(IEnumerable<Inventory> inventories);
        public static partial IEnumerable<ItemDto> MapItemToDto(IEnumerable<Item> items);
        public static partial IEnumerable<OrderDto> MapOrderToDto(IEnumerable<Order> orders);
        public static partial IEnumerable<OrderItemDto> MapOrderItemToDto(IEnumerable<OrderItem> orderItems);
        public static partial IEnumerable<RecipeDto> MapRecipeToDto(IEnumerable<Recipe> recipes);
        public static partial IEnumerable<RecipeIngredientDto> MapRecipeIngredientToDto(IEnumerable<RecipeIngredient> recipeIngredients);
        public static partial IEnumerable<StaffDto> MapStaffToDto(IEnumerable<Staff> staff);

        public static partial Address MapDtoToAddress(AddressDto address);
        public static partial Customer MapDtoToCustomer(CustomerDto customer);
        public static partial Ingredient MapDtoToIngredient(IngredientDto ingredient);
        public static partial Inventory MapDtoToInventory(InventoryDto inventory);
        public static partial Item MapDtoToItem(ItemDto item);
        public static partial Order MapDtoToOrder(OrderDto order);
        public static partial OrderItem MapDtoToOrderItem(OrderItemDto orderItem);
        public static partial Recipe MapDtoToRecipe(RecipeDto recipe);
        public static partial RecipeIngredient MapDtoToRecipeIngredient(RecipeIngredientDto recipeIngredient);
        public static partial Staff MapDtoToStaff(StaffDto staff);
    }
}
