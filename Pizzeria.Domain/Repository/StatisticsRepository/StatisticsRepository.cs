using Pizzeria.Domain.Dto.StatisticsDto;
using Pizzeria.Domain.Repository.StaffRepository;

namespace Pizzeria.Domain.Repository.StatisticsRepository
{
    public class StatisticsRepository(
        PizzeriaDbContext dbContext) : IStatisticsRepository
    {
        public IEnumerable<StaffPayrollResult> CalculateStaffPayroll(DateTime startDate, DateTime endDate) =>
            dbContext.CalculateStaffPayroll(startDate, endDate);

        public IEnumerable<TotalSales> GetTotalSalesRevenueByDay(DateTime startDate, DateTime endDate) =>
            dbContext.GetTotalSalesRevenueByDay(startDate, endDate);

        public IEnumerable<TotalSales> GetSalesDistributionByCategoryAndDay(
            string itemCategory,
            DateTime startDate,
            DateTime endDate) =>
            dbContext.GetSalesDistributionByCategoryAndDay(itemCategory, startDate, endDate);

        public IEnumerable<AverageOrderTotalValue> GetAverageOrderValueByDay(DateTime startDate, DateTime endDate) =>
            dbContext.GetAverageOrderValueByDays(startDate, endDate);

        public IEnumerable<AverageOrderTotalValue> GetAverageOrderValueByMonth(DateTime startDate, DateTime endDate) =>
            dbContext.GetAverageOrderValueByMonth(startDate, endDate);

        public IEnumerable<AverageOrderTotalValue> GetAverageOrderValueByYear(DateTime startDate, DateTime endDate) =>
            dbContext.GetAverageOrderValueByYear(startDate, endDate);

        public ProductOfMonth? GetProductOfMonth(int year, int month) =>
            dbContext.GetProductOfMonth(year, month);
    }
}
