using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pizzeria.Domain.Dto;
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
        public IEnumerable<CustomerDto> GetAll()
        {
            return Mappers.MapCustomerToDto(customerService.GetAll());
        }

        [HttpPost]
        public async Task Create([FromBody] CustomerDto customerDto)
        {
            var customer = Mappers.MapDtoToCustomer(customerDto);
            await customerService.CreateAsync(customer);
        }
    }
}
