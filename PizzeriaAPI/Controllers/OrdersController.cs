using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pizzeria.Domain.Dto.IngredientDto;
using Pizzeria.Domain.Dto.OrderDto;
using Pizzeria.Domain.Dto.RecipeDto;
using Pizzeria.Domain.Mapper;
using Pizzeria.Domain.Services.IngredientService;
using Pizzeria.Domain.Services.OrderService;
using Pizzeria.Domain.Services.RecipeService;
using PizzeriaAPI.Identity.Roles;

namespace PizzeriaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrdersController(IOrderService orderService)
        : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = $"{UserRoleNames.Admin}, {UserRoleNames.Manager}, {UserRoleNames.Cashier}")]
        public IEnumerable<ResponseOrderDto> GetAll(
            [FromQuery] int? pageNumber = null,
            [FromQuery] int? pageSize = null
        )
        {
            return Mappers.MapOrderToResponseDto(orderService.GetAll(
                pageNumber: pageNumber,
                pageSize: pageSize,
                asNoTracking: true));
        }

        [HttpGet("user/{userEmail}")]
        [Authorize(Roles = $"{UserRoleNames.Admin}, {UserRoleNames.Manager}, {UserRoleNames.Cashier}")]
        public IEnumerable<ResponseOrderDto> GetAll(
            [FromRoute] string userEmail,
            [FromQuery] int? pageNumber = null,
            [FromQuery] int? pageSize = null
        )
        {
            return Mappers.MapOrderToResponseDto(orderService.GetAllByUserEmail(
                userEmail,
                pageNumber: pageNumber,
                pageSize: pageSize,
                asNoTracking: true));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = $"{UserRoleNames.Admin}, {UserRoleNames.Manager}, {UserRoleNames.Cashier}")]
        public async Task<ActionResult<ResponseOrderDto>> GetAsync(Guid id)
        {
            var order = await orderService.GetAsync(a => a.OrderId.Equals(id), true);

            if(order is null) return NotFound();

            return Ok(Mappers.MapOrderToResponseDto(order));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseOrderDto>> Create([FromBody] RequestOrderDto requestOrderDto)
        {
            var order = Mappers.MapRequestDtoToOrder(requestOrderDto);
            order.OrderId = Guid.NewGuid();
            await orderService.CreateAsync(order);

            var createdOrder = await orderService.GetAsync(r => r.OrderId == order.OrderId);

            if(createdOrder is null)
                return BadRequest("Couldn't create order");

            return Created(nameof(GetAsync), Mappers.MapOrderToResponseDto(createdOrder));
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = $"{UserRoleNames.Admin}, {UserRoleNames.Manager}")]
        public async Task<ActionResult<ResponseOrderDto>> Update([FromRoute] Guid id, [FromBody] RequestOrderDto requestOrderDto)
        {
            var initialOrder = await orderService.GetAsync(o => o.OrderId.Equals(id), false);

            if(initialOrder is null) return NotFound();

            initialOrder.OrderItems.Clear();

            var updatedOrder = Mappers.MapRequestDtoToOrder(requestOrderDto);

            foreach(var orderItem in updatedOrder.OrderItems)
            {
                orderItem.OrderId = id;
                initialOrder.OrderItems.Add(orderItem);
            }

            await orderService.UpdateAsync(initialOrder);

            return await GetAsync(id);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = $"{UserRoleNames.Admin}, {UserRoleNames.Manager}")]
        public async Task Delete([FromRoute] Guid id)
        {
            var order = await orderService.GetAsync(o => o.OrderId.Equals(id));

            if(order is null) return;

            await orderService.DeleteAsync(order);
        }
    }
}
