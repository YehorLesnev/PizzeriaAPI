using Microsoft.AspNetCore.Mvc;
using Pizzeria.Domain.Dto;
using Pizzeria.Domain.Mapper;
using Pizzeria.Domain.Services.OrderService;

namespace PizzeriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IOrderService orderService)
        : ControllerBase
    {
        [HttpGet]
        public IEnumerable<OrderDto> GetAll()
        {
            return Mappers.MapOrderToDto(orderService.GetAll());
        }

        [HttpPost]
        public async Task Create([FromBody] OrderDto orderDto)
        {
            var order = Mappers.MapDtoToOrder(orderDto);
            
            await orderService.CreateAsync(order);
        }
    }
}
