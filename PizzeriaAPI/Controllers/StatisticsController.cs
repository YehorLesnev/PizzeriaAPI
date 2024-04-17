using Microsoft.AspNetCore.Mvc;
using Pizzeria.Domain.Dto.StatisticsDto;
using Pizzeria.Domain.Repository.StatisticsRepository;

namespace PizzeriaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StatisticsController(IStatisticsRepository statisticsRepository) : ControllerBase
    {
        [HttpGet("GetStaffPayroll")]
        public ActionResult<IEnumerable<StaffPayrollResult>> CalculateStaffPayroll(
        [FromQuery] DateTime dateStart,
            [FromQuery] DateTime dateEnd)
        {
            return Ok(statisticsRepository.CalculateStaffPayroll(dateStart, dateEnd));
        }
    }
}
