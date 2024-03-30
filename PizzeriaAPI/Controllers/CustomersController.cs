using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pizzeria.Domain.Dto;
using Pizzeria.Domain.Dto.CustomerDto;
using Pizzeria.Domain.Mapper;
using Pizzeria.Domain.Services.CustomerService;

namespace PizzeriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController(ICustomerService customerService)
        : ControllerBase
    {
        [HttpGet]
        public IEnumerable<ResponseCustomerDto> GetAll()
        {
            return Mappers.MapCustomerToResponseDto(customerService.GetAll());
        }

        [HttpPost]
        public async Task Create([FromBody] RequestCustomerDto requestCustomerDto)
        {
            var customer = Mappers.MapRequestDtoToCustomer(requestCustomerDto);

            await customerService.CreateAsync(customer);
        }
    }
}
