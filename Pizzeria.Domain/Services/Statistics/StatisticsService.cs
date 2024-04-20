using Pizzeria.Domain.Dto.StatisticsDto;
using Pizzeria.Domain.Repository.StatisticsRepository;
using System.Linq;

namespace Pizzeria.Domain.Services.Statistics
{
    public class StatisticsService(IStatisticsRepository statisticsRepository) : IStatisticsService
    {
        public IEnumerable<StaffPayrollResult> CalculateStaffPayroll(DateTime startDate, DateTime endDate)
        {
            return statisticsRepository.CalculateStaffPayroll(startDate, endDate);
        }

        public IEnumerable<TotalSales> GetTotalSalesRevenueByDay(DateTime startDate, DateTime endDate)
        {
            return statisticsRepository.GetTotalSalesRevenueByDay(startDate, endDate);
        }

        public IEnumerable<TotalSales> GetTotalSalesRevenueByMonth(DateTime startDate, DateTime endDate)
        {
            var salesDays =
                statisticsRepository.GetTotalSalesRevenueByDay(
                    new DateTime(startDate.Year, startDate.Month, 1),
                    new DateTime(endDate.Year, endDate.Month, DateTime.DaysInMonth(endDate.Year, endDate.Month)));

            return salesDays
                .GroupBy(s => new { s.SalesDate.Year, s.SalesDate.Month })
                .Select(g => new TotalSales
                {
                    SalesDate = new DateTime(g.Key.Year, g.Key.Month, DateTime.DaysInMonth(g.Key.Year, g.Key.Month)),
                    TotalRevenue = g.Sum(s => s.TotalRevenue)
                });
        }

        public IEnumerable<TotalSales> GetTotalSalesRevenueByYear(DateTime startDate, DateTime endDate)
        {
            var salesDays =
                statisticsRepository.GetTotalSalesRevenueByDay(
                    new DateTime(startDate.Year, 1, 1),
                    new DateTime(endDate.Year, 12, DateTime.DaysInMonth(endDate.Year, 12)));

            return salesDays
                .GroupBy(s => s.SalesDate.Year)
                .Select(g => new TotalSales
                {
                    SalesDate = new DateTime(g.Key, 12, DateTime.DaysInMonth(g.Key, 12)),
                    TotalRevenue = g.Sum(s => s.TotalRevenue)
                });
        }

        public IEnumerable<AverageOrderTotalValue> GetAverageOrderValueByDay(DateTime startDate, DateTime endDate)
        {
            return statisticsRepository.GetAverageOrderValueByDay(startDate, endDate);
        }

        public IEnumerable<AverageOrderTotalValue> GetAverageOrderValueByMonth(DateTime startDate, DateTime endDate)
        {
            return statisticsRepository.GetAverageOrderValueByMonth(
                new DateTime(startDate.Year, startDate.Month, 1),
                new DateTime(endDate.Year, endDate.Month, DateTime.DaysInMonth(endDate.Year, endDate.Month)));
        }

        public IEnumerable<AverageOrderTotalValue> GetAverageOrderValueByYear(DateTime startDate, DateTime endDate)
        {
            return statisticsRepository.GetAverageOrderValueByYear(
                new DateTime(startDate.Year, 1, 1),
                new DateTime(endDate.Year, 12, DateTime.DaysInMonth(endDate.Year, 12)));
        }
    }
}
