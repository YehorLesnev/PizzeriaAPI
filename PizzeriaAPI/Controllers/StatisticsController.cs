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

        [HttpGet("Sales/Days")]
        public ActionResult<IEnumerable<TotalSales>> GetDatePeriodSalesByDays(
            [FromQuery] DateTime dateStart,
            [FromQuery] DateTime dateEnd,
            [FromQuery] string? itemCategory = null)
        {
            return Ok(statisticsService.GetTotalSalesRevenueByDay(dateStart, dateEnd, itemCategory));
        }

        [HttpGet("Sales/Months")]
        public ActionResult<IEnumerable<TotalSales>> GetDatePeriodSalesByMonth(
            [FromQuery] DateTime dateStart,
            [FromQuery] DateTime dateEnd,
            [FromQuery] string? itemCategory = null)
        {
            return Ok(statisticsService.GetTotalSalesRevenueByMonth(dateStart, dateEnd, itemCategory));
        }

        [HttpGet("Sales/Years")]
        public ActionResult<IEnumerable<TotalSales>> GetDatePeriodSalesByYear(
            [FromQuery] DateTime dateStart,
            [FromQuery] DateTime dateEnd,
            [FromQuery] string? itemCategory = null)
        {
            return Ok(statisticsService.GetTotalSalesRevenueByYear(dateStart, dateEnd, itemCategory));
        }

        [HttpGet("GetProductOfMonth")]
        public ActionResult<ProductOfMonth> GetProductOfMonth(
            [FromQuery] int year,
            [FromQuery] int month)
        {
            var result = statisticsService.GetProductOfMonth(year, month);

            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet("AverageOrderTotal/Days")]
        public ActionResult<IEnumerable<AverageOrderTotalValue>> GetAverageOrderValueByDay(
            [FromQuery] DateTime dateStart,
            [FromQuery] DateTime dateEnd)
        {
            return Ok(statisticsService.GetAverageOrderValueByDay(dateStart, dateEnd));
        }

        [HttpGet("AverageOrderTotal/Months")]
        public ActionResult<IEnumerable<AverageOrderTotalValue>> GetAverageOrderValueByMonth(
            [FromQuery] DateTime dateStart,
            [FromQuery] DateTime dateEnd)
        {
            return Ok(statisticsService.GetAverageOrderValueByMonth(dateStart, dateEnd));
        }

        [HttpGet("AverageOrderTotal/Years")]
        public ActionResult<IEnumerable<AverageOrderTotalValue>> GetAverageOrderValueByYear(
            [FromQuery] DateTime dateStart,
            [FromQuery] DateTime dateEnd)
        {
            return Ok(statisticsService.GetAverageOrderValueByYear(dateStart, dateEnd));
        }
    }
}
