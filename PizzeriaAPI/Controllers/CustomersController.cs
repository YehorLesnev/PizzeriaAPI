using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pizzeria.Domain.Dto.CustomerDto;
using Pizzeria.Domain.Dto.RecipeDto;
using Pizzeria.Domain.Mapper;
using Pizzeria.Domain.Services.CustomerService;
using Pizzeria.Domain.Services.RecipeService;
using PizzeriaAPI.Identity.Roles;

namespace PizzeriaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomersController(ICustomerService customerService)
        : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = $"{UserRoleNames.Admin}, {UserRoleNames.Manager}")]
        public IEnumerable<ResponseCustomerDto> GetAll(
            [FromQuery] int? pageNumber = null,
            [FromQuery] int? pageSize = null
        )
        {
            return Mappers.MapCustomerToResponseDto(customerService.GetAll(
                pageNumber: pageNumber,
                pageSize: pageSize,
                asNoTracking: true));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = $"{UserRoleNames.Admin}, {UserRoleNames.Manager}")]
        public async Task<ActionResult<ResponseCustomerDto>> Get(Guid id)
        {
            var customer = await customerService.GetAsync(a => a.CustomerId.Equals(id), true);

            if(customer is null) return NotFound();

            return Ok(Mappers.MapCustomerToResponseDto(customer));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseCustomerDto>> Create([FromBody] RequestCustomerDto requestCustomerDto)
        {
            var customer = Mappers.MapRequestDtoToCustomer(requestCustomerDto);
            
            await customerService.CreateAsync(customer);

            return Ok(Mappers.MapCustomerToResponseDto(customer));
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = $"{UserRoleNames.Admin}, {UserRoleNames.Manager}, {UserRoleNames.Customer}")]
        public async Task<ActionResult<ResponseCustomerDto>> Update([FromRoute] Guid id, [FromBody] RequestCustomerDto requestCustomerDto)
        {
            var initialCustomer = await customerService.GetAsync(o => o.CustomerId.Equals(id), true);

            if(initialCustomer is null) return NotFound();

            var updatedCustomer = Mappers.MapRequestDtoToCustomer(requestCustomerDto);
            updatedCustomer.CustomerId = initialCustomer.CustomerId;

            await customerService.UpdateAsync(updatedCustomer);
            
            return Ok(Mappers.MapCustomerToResponseDto(updatedCustomer));
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = $"{UserRoleNames.Admin}, {UserRoleNames.Manager}, {UserRoleNames.Customer}")]
        public async Task Delete([FromRoute] Guid id)
        {
            var customer = await customerService.GetAsync(o => o.CustomerId.Equals(id));

            if(customer is null) return;

            await customerService.DeleteAsync(customer);
        }
    }
}
