using Pizzeria.Domain.Dto.StatisticsDto;

namespace Pizzeria.Domain.Repository.StatisticsRepository
{
    public interface IStatisticsRepository
    {
        IEnumerable<StaffPayrollResult> CalculateStaffPayroll(DateTime startDate, DateTime endDate);

        IEnumerable<TotalSales> GetTotalSalesRevenueByDay(DateTime startDate, DateTime endDate);

        IEnumerable<TotalSales> GetSalesDistributionByCategoryAndDay(string itemCategory, DateTime startDate, DateTime endDate);

        IEnumerable<AverageOrderTotalValue> GetAverageOrderValueByDay(DateTime startDate, DateTime endDate);
        IEnumerable<AverageOrderTotalValue> GetAverageOrderValueByMonth(DateTime startDate, DateTime endDate);
        IEnumerable<AverageOrderTotalValue> GetAverageOrderValueByYear(DateTime startDate, DateTime endDate);

        ProductOfMonth? GetProductOfMonth(int year, int month);
    }
}
