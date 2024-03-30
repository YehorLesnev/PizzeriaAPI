using Microsoft.AspNetCore.Mvc;
using Pizzeria.Domain.Dto.StaffDto;
using Pizzeria.Domain.Mapper;
using Pizzeria.Domain.Services.StaffServcice;

namespace PizzeriaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StaffController(IStaffService staffService)
        : ControllerBase
    {
        [HttpGet]
        public IEnumerable<ResponseStaffDto> GetAll()
        {
            return Mappers.MapStaffToResponseDto(staffService.GetAll(asNoTracking: true));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseStaffDto>> Get(Guid id)
        {
            var staff = await staffService.GetAsync(a => a.StaffId.Equals(id), true);

            if(staff is null) return NotFound();

            return Ok(Mappers.MapStaffToResponseDto(staff));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseStaffDto>> Create([FromBody] RequestStaffDto requestStaffDto)
        {
            var staff = Mappers.MapRequestDtoToStaff(requestStaffDto);
            
            await staffService.CreateAsync(staff);

            return Ok(Mappers.MapStaffToResponseDto(staff));
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseStaffDto>> Update([FromRoute] Guid id, [FromBody] RequestStaffDto requestStaffDto)
        {
            var initialStaff = await staffService.GetAsync(o => o.StaffId.Equals(id), true);

            if(initialStaff is null) return NotFound();

            var updatedStaff = Mappers.MapRequestDtoToStaff(requestStaffDto);
            updatedStaff.StaffId = initialStaff.StaffId;

            await staffService.UpdateAsync(updatedStaff);
            
            return Ok(Mappers.MapStaffToResponseDto(updatedStaff));
        }

        [HttpDelete("{id:guid}")]
        public async Task Delete([FromRoute] Guid id)
        {
            var staff = await staffService.GetAsync(o => o.StaffId.Equals(id));

            if(staff is null) return;

            await staffService.DeleteAsync(staff);
        }
    }
}
