using Pizzeria.Domain.Dto.StatisticsDto;

namespace Pizzeria.Domain.Services.Statistics
{
    public interface IStatisticsService
    {
        IEnumerable<StaffPayrollResult> CalculateStaffPayroll(DateTime startDate, DateTime endDate);

        IEnumerable<TotalSales> GetTotalSalesRevenueByDay(DateTime startDate, DateTime endDate);
        IEnumerable<TotalSales> GetTotalSalesRevenueByMonth(DateTime startDate, DateTime endDate);
        IEnumerable<TotalSales> GetTotalSalesRevenueByYear(DateTime startDate, DateTime endDate);

        IEnumerable<AverageOrderTotalValue> GetAverageOrderValueByDay(DateTime startDate, DateTime endDate);
        IEnumerable<AverageOrderTotalValue> GetAverageOrderValueByMonth(DateTime startDate, DateTime endDate);
        IEnumerable<AverageOrderTotalValue> GetAverageOrderValueByYear(DateTime startDate, DateTime endDate);
    }
}
