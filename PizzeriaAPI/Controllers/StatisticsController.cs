using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pizzeria.Domain.Dto.StatisticsDto;
using Pizzeria.Domain.Identity.Roles;
using Pizzeria.Domain.Services.Statistics;

namespace PizzeriaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoleNames.Manager)]
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
        
        [HttpGet("NumberOfOrders/Days")]
        public ActionResult<IEnumerable<NumberOfOrdersInfo>> GetNumberOfOrdersByDay(
            [FromQuery] DateOnly dateStart,
            [FromQuery] DateOnly dateEnd)
        {
            return Ok(statisticsService.GetNumberOfOrdersByDay(dateStart, dateEnd));
        } 
        
        [HttpGet("NumberOfOrders/Month")]
        public ActionResult<IEnumerable<NumberOfOrdersInfo>> GetNumberOfOrdersByMonth(
            [FromQuery] DateOnly dateStart,
            [FromQuery] DateOnly dateEnd)
        {
            return Ok(statisticsService.GetNumberOfOrdersByMonth(dateStart, dateEnd));
        } 
        
        [HttpGet("NumberOfOrders/Years")]
        public ActionResult<IEnumerable<NumberOfOrdersInfo>> GetNumberOfOrdersByYear(
            [FromQuery] DateOnly dateStart,
            [FromQuery] DateOnly dateEnd)
        {
            return Ok(statisticsService.GetNumberOfOrdersByYear(dateStart, dateEnd));
        }

        [HttpGet("ProductOfMonth")]
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

        [HttpGet("OrderDeliveryInfo/Days")]
        public ActionResult<IEnumerable<OrderDeliveryInfo>> GetOrderDeliveryInfoByDay(
            [FromQuery] DateTime dateStart,
            [FromQuery] DateTime dateEnd)
        {
            return Ok(statisticsService.GetOrderDeliveryInfoByDay(dateStart, dateEnd));
        }

        [HttpGet("OrderDeliveryInfo/Months")]
        public ActionResult<IEnumerable<OrderDeliveryInfo>> GetOrderDeliveryInfoByMonth(
            [FromQuery] DateTime dateStart,
            [FromQuery] DateTime dateEnd)
        {
            return Ok(statisticsService.GetOrderDeliveryInfoByMonth(dateStart, dateEnd));
        }

        [HttpGet("OrderDeliveryInfo/Years")]
        public ActionResult<IEnumerable<OrderDeliveryInfo>> GetOrderDeliveryInfoByYear(
            [FromQuery] DateTime dateStart,
            [FromQuery] DateTime dateEnd)
        {
            return Ok(statisticsService.GetOrderDeliveryInfoByYear(dateStart, dateEnd));
        }

        [HttpGet("StaffOrdersInfo/Days")]
        public ActionResult<IEnumerable<OrderDeliveryInfo>> GetStaffOrdersInfoByDay([FromQuery] DateOnly date)
        {
            return Ok(statisticsService.GetStaffOrdersInfoByDay(date));
        }

        [HttpGet("StaffOrdersInfo/Months")]
        public ActionResult<IEnumerable<OrderDeliveryInfo>> GetStaffOrdersInfoByMonth([FromQuery] DateOnly date)
        {
            return Ok(statisticsService.GetStaffOrdersInfoByMonth(date));
        }

        [HttpGet("StaffOrdersInfo/Years")]
        public ActionResult<IEnumerable<OrderDeliveryInfo>> GetStaffOrdersInfoByYear([FromQuery] DateOnly date)
        {
            return Ok(statisticsService.GetStaffOrdersInfoByYear(date));
        }
    }
}