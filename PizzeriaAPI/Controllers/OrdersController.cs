using Microsoft.AspNetCore.Mvc;
using Pizzeria.Domain.Dto.OrderDto;
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
        public IEnumerable<ResponseOrderDto> GetAll()
        {
            return Mappers.MapOrderToResponseDto(orderService.GetAll());
        }

        [HttpPost]
        public async Task Create([FromBody] RequestOrderDto requestOrderDto)
        {
            var order = Mappers.MapRequestDtoToOrder(requestOrderDto);
            
            await orderService.CreateAsync(order);
        }
    }
}
