using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pizzeria.Domain.Dto.AddressDto;
using Pizzeria.Domain.Dto.StaffDto;
using Pizzeria.Domain.Mapper;
using Pizzeria.Domain.Services.AddressService;
using Pizzeria.Domain.Services.StaffServcice;
using PizzeriaAPI.Identity.Roles;

namespace PizzeriaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AddressesController(
        IAddressService addressService
        ) : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = $"{UserRoleNames.Admin}, {UserRoleNames.Manager}, {UserRoleNames.Cashier}")]
        public IEnumerable<ResponseAddressDto> GetAll(
            [FromQuery] int? pageNumber = null,
            [FromQuery] int? pageSize = null
        )
        {
            return Mappers.MapAddressToResponseDto(addressService.GetAll(
                pageNumber: pageNumber,
                pageSize: pageSize,
                asNoTracking: true));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = $"{UserRoleNames.Admin}, {UserRoleNames.Manager}, {UserRoleNames.Cashier}")]
        public async Task<ActionResult<ResponseAddressDto>> Get(Guid id)
        {
            var address = await addressService.GetAsync(a => a.AddressId.Equals(id), true);

            if(address is null) return NotFound();

            return Ok(Mappers.MapAddressToResponseDto(address));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseAddressDto>> Create([FromBody] RequestAddressDto requestAddressDto)
        {
            var address = Mappers.MapRequestDtoToAddress(requestAddressDto);
            address.AddressId = Guid.NewGuid();

            await addressService.CreateAsync(address);

            var createdAddress = await addressService.GetAsync(r => r.AddressId == address.AddressId);

            if(createdAddress is null)
                return BadRequest("Couldn't create address");

            return Created(nameof(Get), Mappers.MapAddressToResponseDto(createdAddress));
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = $"{UserRoleNames.Admin}, {UserRoleNames.Manager}, {UserRoleNames.Cashier}, {UserRoleNames.Customer}")]
        public async Task<ActionResult<ResponseAddressDto>> Update([FromRoute] Guid id, [FromBody] RequestAddressDto requestAddressDto)
        {
            var initialAddress = await addressService.GetAsync(o => o.AddressId.Equals(id), false);

            if(initialAddress is null) return NotFound();

            var updatedAddress = Mappers.MapRequestDtoToAddress(requestAddressDto);

            await addressService.UpdateAsync(updatedAddress);
            
            return Ok(Mappers.MapAddressToResponseDto(updatedAddress));
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = $"{UserRoleNames.Admin}, {UserRoleNames.Manager}, {UserRoleNames.Cashier}, {UserRoleNames.Customer}")]
        public async Task Delete([FromRoute] Guid id)
        {
            var order = await addressService.GetAsync(o => o.AddressId.Equals(id));

            if(order is null) return;

            await addressService.DeleteAsync(order);
        }
    }
}