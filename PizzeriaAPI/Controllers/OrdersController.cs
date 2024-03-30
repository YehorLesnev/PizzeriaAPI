using Microsoft.AspNetCore.Mvc;
using Pizzeria.Domain.Dto.OrderDto;
using Pizzeria.Domain.Mapper;
using Pizzeria.Domain.Services.OrderService;

namespace PizzeriaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrdersController(IOrderService orderService)
        : ControllerBase
    {
        [HttpGet]
        public IEnumerable<ResponseOrderDto> GetAll()
        {
            return Mappers.MapOrderToResponseDto(orderService.GetAll(asNoTracking: true));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseOrderDto>> Get(Guid id)
        {
            var order = await orderService.GetAsync(a => a.OrderId.Equals(id), true);

            if(order is null) return NotFound();

            return Ok(Mappers.MapOrderToResponseDto(order));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseOrderDto>> Create([FromBody] RequestOrderDto requestOrderDto)
        {
            var order = Mappers.MapRequestDtoToOrder(requestOrderDto);
            
            await orderService.CreateAsync(order);

            return Ok(Mappers.MapOrderToResponseDto(order));
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseOrderDto>> Update([FromRoute] Guid id, [FromBody] RequestOrderDto requestOrderDto)
        {
            var initialOrder = await orderService.GetAsync(o => o.OrderId.Equals(id), true);

            if(initialOrder is null) return NotFound();

            var updatedOrder = Mappers.MapRequestDtoToOrder(requestOrderDto);
            updatedOrder.OrderId = initialOrder.OrderId;

            await orderService.UpdateAsync(updatedOrder);
            
            return Ok(Mappers.MapOrderToResponseDto(updatedOrder));
        }

        [HttpDelete("{id:guid}")]
        public async Task Delete([FromRoute] Guid id)
        {
            var order = await orderService.GetAsync(o => o.OrderId.Equals(id));

            if(order is null) return;

            await orderService.DeleteAsync(order);
        }
    }
}
