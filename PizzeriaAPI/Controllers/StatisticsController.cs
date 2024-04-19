using Microsoft.AspNetCore.Mvc;
using Pizzeria.Domain.Dto.StatisticsDto;
using Pizzeria.Domain.Repository.StatisticsRepository;
using Pizzeria.Domain.Services.Statistics;

namespace PizzeriaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StatisticsController(IStatisticsService statisticsService) : ControllerBase
    {
        [HttpGet("GetStaffPayroll")]
        public ActionResult<IEnumerable<StaffPayrollResult>> CalculateStaffPayroll(
            [FromQuery] DateTime dateStart,
            [FromQuery] DateTime dateEnd)
        {
            return Ok(statisticsService.CalculateStaffPayroll(dateStart, dateEnd));
        }

        [HttpGet("Sales")]
        public ActionResult<IEnumerable<TotalSalesDay>> GetDatePeriodSales(
            [FromQuery] DateTime dateStart,
            [FromQuery] DateTime dateEnd)
        {
            return Ok(statisticsService.GetTotalSalesRevenueByDay(dateStart, dateEnd));
        }
    }
}
