using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pizzeria.Domain.Dto.CustomerDto;
using Pizzeria.Domain.Dto.RecipeDto;
using Pizzeria.Domain.Identity.Roles;
using Pizzeria.Domain.Mapper;
using Pizzeria.Domain.Services.CustomerService;
using Pizzeria.Domain.Services.RecipeService;

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
            var customer = await customerService.GetAsync(a => a.Id.Equals(id), true);

            if(customer is null) return NotFound();

            return Ok(Mappers.MapCustomerToResponseDto(customer));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseCustomerDto>> CreateAsync([FromBody] RequestCustomerDto requestCustomerDto)
        {
            var customer = Mappers.MapRequestDtoToCustomer(requestCustomerDto);

            if(await customerService.GetAsync(c => c.Email == requestCustomerDto.Email) is not null)
                return BadRequest("Customer with specified email is already exists");

            try
            {
                await customerService.CreateAsync(customer, requestCustomerDto.Password);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest("The password must contain capital letters, numbers and special symbol");
            }

            var createdCustomer = await customerService.GetAsync(r => r.Email == customer.Email);

            if(createdCustomer is null)
                return BadRequest("Couldn't create customer");

            return Created(nameof(Get), Mappers.MapCustomerToResponseDto(createdCustomer));
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = $"{UserRoleNames.Admin}, {UserRoleNames.Manager}, {UserRoleNames.Customer}")]
        public async Task<ActionResult<ResponseCustomerDto>> Update([FromRoute] Guid id, [FromBody] RequestUpdateCustomerDto requestCustomerDto)
        {
            var initialCustomer = await customerService.GetAsync(o => o.Id.Equals(id), false);

            if(initialCustomer is null) return NotFound();

            initialCustomer.PhoneNumber = requestCustomerDto.PhoneNumber;
            initialCustomer.FirstName = requestCustomerDto.FirstName;
            initialCustomer.LastName = requestCustomerDto.LastName;

            await customerService.UpdateAsync(initialCustomer);
            
            return Ok(Mappers.MapCustomerToResponseDto(initialCustomer));
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = $"{UserRoleNames.Admin}, {UserRoleNames.Manager}, {UserRoleNames.Customer}")]
        public async Task Delete([FromRoute] Guid id)
        {
            var customer = await customerService.GetAsync(o => o.Id.Equals(id));

            if(customer is null) return;

            await customerService.DeleteAsync(customer);
        }
    }
}
