using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pizzeria.Domain.Dto.OrderDto;
using Pizzeria.Domain.Dto.RecipeDto;
using Pizzeria.Domain.Dto.ShiftDto;
using Pizzeria.Domain.Identity.Roles;
using Pizzeria.Domain.Mapper;
using Pizzeria.Domain.Services.OrderService;
using Pizzeria.Domain.Services.RecipeService;
using Pizzeria.Domain.Services.ShiftService;

namespace PizzeriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = $"{UserRoleNames.Admin}, {UserRoleNames.Manager}")]
    public class ShiftsController(
        IShiftService shiftService) : ControllerBase
    {
        [HttpGet]
        public IEnumerable<ResponseShiftDto> GetAll(
            [FromQuery] int? pageNumber = null,
            [FromQuery] int? pageSize = null
        )
        {
            return Mappers.MapShiftToResponseDto(shiftService.GetAll(
                pageNumber: pageNumber,
                pageSize: pageSize,
                asNoTracking: true));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseShiftDto>> GetAsync(Guid id)
        {
            var shift = await shiftService.GetAsync(a => a.ShiftId.Equals(id), true);

            if(shift is null) return NotFound();

            return Ok(Mappers.MapShiftToResponseDto(shift));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseShiftDto>> Create([FromBody] RequestShiftDto requestShiftDto)
        {
            var shift = Mappers.MapRequestDtoToShift(requestShiftDto);
            shift.ShiftId = Guid.NewGuid();
            await shiftService.CreateAsync(shift);

            var createdShift = await shiftService.GetAsync(r => r.ShiftId == shift.ShiftId);

            if(createdShift is null)
                return BadRequest("Couldn't create shift");

            return Created(nameof(GetAsync), Mappers.MapShiftToResponseDto(createdShift));
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseShiftDto>> Update([FromRoute] Guid id, [FromBody] RequestShiftDto requestShiftDto)
        {
            var initialShift = await shiftService.GetAsync(o => o.ShiftId.Equals(id), false);

            if(initialShift is null) return NotFound();

            var updatedShift = Mappers.MapRequestDtoToShift(requestShiftDto);

            initialShift.ShiftStaff.Clear();

            foreach(var staff in updatedShift.ShiftStaff)
            {
                staff.ShiftId = id;
                initialShift.ShiftStaff.Add(staff);
            }

            await shiftService.UpdateAsync(initialShift);
            
            return await GetAsync(id);;
        }

        [HttpDelete("{id:guid}")]
        public async Task Delete([FromRoute] Guid id)
        {
            var shift = await shiftService.GetAsync(o => o.ShiftId.Equals(id));

            if(shift is null) return;

            await shiftService.DeleteAsync(shift);
        }
    }
}
