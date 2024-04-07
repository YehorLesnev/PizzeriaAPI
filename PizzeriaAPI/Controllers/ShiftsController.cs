using Microsoft.AspNetCore.Mvc;
using Pizzeria.Domain.Dto.ShiftDto;
using Pizzeria.Domain.Mapper;
using Pizzeria.Domain.Services.ShiftService;

namespace PizzeriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftsController(
        IShiftService shiftService) : ControllerBase
    {
        [HttpGet]
        public IEnumerable<ResponseShiftDto> GetAll()
        {
            return Mappers.MapShiftToResponseDto(shiftService.GetAll(asNoTracking: true));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseShiftDto>> Get(Guid id)
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
            
            await shiftService.CreateAsync(shift);

            return Ok(Mappers.MapShiftToResponseDto(shift));
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseShiftDto>> Update([FromRoute] Guid id, [FromBody] RequestShiftDto requestShiftDto)
        {
            var initialShift = await shiftService.GetAsync(o => o.ShiftId.Equals(id), true);

            if(initialShift is null) return NotFound();

            var updatedShift = Mappers.MapRequestDtoToShift(requestShiftDto);
            updatedShift.ShiftId = initialShift.ShiftId;

            await shiftService.UpdateAsync(updatedShift);
            
            return Ok(Mappers.MapShiftToResponseDto(updatedShift));
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
