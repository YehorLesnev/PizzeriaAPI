using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pizzeria.Domain.Dto;
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
        public IEnumerable<StaffDto> GetAll()
        {
            return Mappers.MapStaffToDto(staffService.GetAll());
        }

        [HttpPost]
        public async Task Create([FromBody] StaffDto staffDto)
        {
            var staff = Mappers.MapDtoToStaff(staffDto);

            await staffService.CreateAsync(staff);
        }
    }
}
