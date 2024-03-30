using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pizzeria.Domain.Dto;
using Pizzeria.Domain.Dto.StaffDto;
using Pizzeria.Domain.Mapper;
using Pizzeria.Domain.Services.StaffServcice;

namespace PizzeriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController(IStaffService staffService)
        : ControllerBase
    {
        [HttpGet]
        public IEnumerable<ResponseStaffDto> GetAll()
        {
            return Mappers.MapStaffToResponseDto(staffService.GetAll());
        }

        [HttpPost]
        public async Task Create([FromBody] RequestStaffDto requestStaffDto)
        {
            var staff = Mappers.MapRequestDtoToStaff(requestStaffDto);

            await staffService.CreateAsync(staff);
        }
    }
}
