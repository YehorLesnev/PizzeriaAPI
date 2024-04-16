using Pizzeria.Domain.Dto.AddressDto;
using Pizzeria.Domain.Dto.CustomerDto;
using Pizzeria.Domain.Dto.IngredientDto;
using Pizzeria.Domain.Dto.ItemDto;
using Pizzeria.Domain.Dto.OrderDto;
using Pizzeria.Domain.Dto.OrderItemDto;
using Pizzeria.Domain.Dto.RecipeDto;
using Pizzeria.Domain.Dto.RecipeIngredientDto;
using Pizzeria.Domain.Dto.ShiftDto;
using Pizzeria.Domain.Dto.ShiftStaffDto;
using Pizzeria.Domain.Dto.StaffDto;
using Pizzeria.Domain.Models;
using Riok.Mapperly.Abstractions;

namespace Pizzeria.Domain.Mapper
{
    [Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
    public static partial class Mappers
    {
        public static partial ResponseAddressDto MapAddressToResponseDto(Address address);
        public static partial ResponseCustomerDto MapCustomerToResponseDto(Customer customer);
        public static partial ResponseIngredientDto MapIngredientToResponseDto(Ingredient ingredient);
        public static partial ResponseItemDto MapItemToResponseDto(Item item);
        public static partial ResponseOrderDto MapOrderToResponseDto(Order order);
        public static partial ResponseOrderItemDto MapOrderItemToResponseDto(OrderItem orderItem);
        public static partial ResponseRecipeDto MapRecipeToResponseDto(Recipe recipe);

        [MapProperty(nameof(RecipeIngredient.Ingredient), nameof(ResponseRecipeIngredientDto.Ingredient))]
        public static partial ResponseRecipeIngredientDto MapRecipeIngredientToResponseDto(RecipeIngredient recipeIngredient);
        public static partial ResponseStaffDto MapStaffToResponseDto(Staff staff);
        public static partial ResponseShiftDto MapShiftToResponseDto(Shift shift);
        public static partial ResponseShiftStaffDto MapShiftStaffToResponseDto(ShiftStaff shiftStaff);

        public static partial IEnumerable<ResponseAddressDto> MapAddressToResponseDto(IEnumerable<Address> addresses);
        public static partial IEnumerable<ResponseCustomerDto> MapCustomerToResponseDto(IEnumerable<Customer> customers);
        public static partial IEnumerable<ResponseIngredientDto> MapIngredientToResponseDto(IEnumerable<Ingredient> ingredients);
        public static partial IEnumerable<ResponseItemDto> MapItemToResponseDto(IEnumerable<Item> items);
        public static partial IEnumerable<ResponseOrderDto> MapOrderToResponseDto(IEnumerable<Order> orders);
        public static partial IEnumerable<ResponseOrderItemDto> MapOrderItemToResponseDto(IEnumerable<OrderItem> orderItems);
        public static partial IEnumerable<ResponseRecipeDto> MapRecipeToResponseDto(IEnumerable<Recipe> recipes);
        public static partial IEnumerable<ResponseRecipeIngredientDto> MapRecipeIngredientToResponseDto(IEnumerable<RecipeIngredient> recipeIngredients);
        public static partial IEnumerable<ResponseStaffDto> MapStaffToResponseDto(IEnumerable<Staff> staff);
        public static partial IEnumerable<ResponseShiftDto> MapShiftToResponseDto(IEnumerable<Shift> shifts);
        public static partial IEnumerable<ResponseShiftStaffDto> MapShiftStaffToResponseDto(IEnumerable<ShiftStaff> shiftStaff);

        public static partial Address MapRequestDtoToAddress(RequestAddressDto requestAddress);
        public static partial Customer MapRequestDtoToCustomer(RequestCustomerDto requestCustomer);
        public static partial Ingredient MapRequestDtoToIngredient(RequestIngredientDto requestIngredient);
        public static partial Item MapRequestDtoToItem(RequestItemDto requestItem);
        public static partial Order MapRequestDtoToOrder(RequestOrderDto requestOrder);
        public static partial OrderItem MapDtoToOrderItem(RequestOrderItemDto orderItem);
        public static partial Recipe MapRequestDtoToRecipe(RequestRecipeDto requestRecipe);
        public static partial RecipeIngredient MapRequestDtoToRecipeIngredient(RequestRecipeIngredientDto responseRecipeIngredient);
        public static partial Staff MapRequestDtoToStaff(RequestStaffDto requestStaff);
        public static partial ShiftStaff MapRequestDtoToShiftStaff(RequestShiftStaffDto requestShiftStaff);
        public static partial Shift MapRequestDtoToShift(RequestShiftDto requestShift);
    }
}
