using Pizzeria.Domain.Dto.StatisticsDto;
using Pizzeria.Domain.Repository.StatisticsRepository;

namespace Pizzeria.Domain.Services.Statistics
{
    public class StatisticsService(IStatisticsRepository statisticsRepository) : IStatisticsService
    {
        public IEnumerable<StaffPayrollResult> CalculateStaffPayroll(DateTime startDate, DateTime endDate) =>
            statisticsRepository.CalculateStaffPayroll(startDate, endDate);

        public IEnumerable<TotalSales> GetTotalSalesRevenueByDay(
            DateTime startDate,
            DateTime endDate,
            string? itemCategory = null) =>
            itemCategory is null ? statisticsRepository.GetTotalSalesRevenueByDay(startDate, endDate)
            : statisticsRepository.GetSalesDistributionByCategoryAndDay(itemCategory, startDate, endDate);


        public IEnumerable<TotalSales> GetTotalSalesRevenueByMonth(DateTime startDate, DateTime endDate, string? itemCategory = null)
        {
            var start = new DateTime(startDate.Year, startDate.Month, 1);
            var end = new DateTime(endDate.Year, endDate.Month, DateTime.DaysInMonth(endDate.Year, endDate.Month));

            var salesDays = itemCategory is null ?
                statisticsRepository.GetTotalSalesRevenueByDay(start, end)
                : statisticsRepository.GetSalesDistributionByCategoryAndDay(itemCategory, start, end);

            return salesDays
                .GroupBy(s => new { s.SalesDate.Year, s.SalesDate.Month })
                .Select(g => new TotalSales
                {
                    SalesDate = new DateTime(g.Key.Year, g.Key.Month, DateTime.DaysInMonth(g.Key.Year, g.Key.Month)),
                    TotalRevenue = g.Sum(s => s.TotalRevenue)
                });
        }

        public IEnumerable<TotalSales> GetTotalSalesRevenueByYear(DateTime startDate, DateTime endDate, string? itemCategory = null)
        {
            var start = new DateTime(startDate.Year, 1, 1);
            var end = new DateTime(endDate.Year, 12, DateTime.DaysInMonth(endDate.Year, 12));

            var salesDays = itemCategory is null ?
                statisticsRepository.GetTotalSalesRevenueByDay(start, end)
                : statisticsRepository.GetSalesDistributionByCategoryAndDay(itemCategory, start, end);

            return salesDays
                .GroupBy(s => s.SalesDate.Year)
                .Select(g => new TotalSales
                {
                    SalesDate = new DateTime(g.Key, 12, DateTime.DaysInMonth(g.Key, 12)),
                    TotalRevenue = g.Sum(s => s.TotalRevenue)
                });
        }

        public IEnumerable<AverageOrderTotalValue> GetAverageOrderValueByDay(DateTime startDate, DateTime endDate) =>
            statisticsRepository.GetAverageOrderValueByDay(startDate, endDate);


        public IEnumerable<AverageOrderTotalValue> GetAverageOrderValueByMonth(DateTime startDate, DateTime endDate) =>
            statisticsRepository.GetAverageOrderValueByMonth(
                new DateTime(startDate.Year, startDate.Month, 1),
                new DateTime(endDate.Year, endDate.Month, DateTime.DaysInMonth(endDate.Year, endDate.Month)));


        public IEnumerable<AverageOrderTotalValue> GetAverageOrderValueByYear(DateTime startDate, DateTime endDate) =>
            statisticsRepository.GetAverageOrderValueByYear(
                new DateTime(startDate.Year, 1, 1),
                new DateTime(endDate.Year, 12, DateTime.DaysInMonth(endDate.Year, 12)));

        public ProductOfMonth? GetProductOfMonth(int year, int month) =>
            statisticsRepository.GetProductOfMonth(year, month);
    }
}