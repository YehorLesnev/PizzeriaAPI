using Pizzeria.Domain.Dto.StatisticsDto;

namespace Pizzeria.Domain.Services.Statistics
{
    public interface IStatisticsService
    {
        IEnumerable<StaffPayrollResult> CalculateStaffPayroll(DateTime startDate, DateTime endDate);

        IEnumerable<TotalSales> GetTotalSalesRevenueByDay(DateTime startDate, DateTime endDate, string? itemCategory = null);
        IEnumerable<TotalSales> GetTotalSalesRevenueByMonth(DateTime startDate, DateTime endDate, string? itemCategory = null);
        IEnumerable<TotalSales> GetTotalSalesRevenueByYear(DateTime startDate, DateTime endDate, string? itemCategory = null);

        IEnumerable<AverageOrderTotalValue> GetAverageOrderValueByDay(DateTime startDate, DateTime endDate);
        IEnumerable<AverageOrderTotalValue> GetAverageOrderValueByMonth(DateTime startDate, DateTime endDate);
        IEnumerable<AverageOrderTotalValue> GetAverageOrderValueByYear(DateTime startDate, DateTime endDate);

        ProductOfMonth? GetProductOfMonth(int year, int month);

        IEnumerable<OrderDeliveryInfo> GetOrderDeliveryInfoByDay(DateTime startDate, DateTime endDate);
        IEnumerable<OrderDeliveryInfo> GetOrderDeliveryInfoByMonth(DateTime startDate, DateTime endDate);
        IEnumerable<OrderDeliveryInfo> GetOrderDeliveryInfoByYear(DateTime startDate, DateTime endDate);

        IEnumerable<StaffOrdersInfo> GetStaffOrdersInfoByDay(DateOnly date);
        IEnumerable<StaffOrdersInfo> GetStaffOrdersInfoByMonth(DateOnly date);
        IEnumerable<StaffOrdersInfo> GetStaffOrdersInfoByYear(DateOnly date);
    }
}
