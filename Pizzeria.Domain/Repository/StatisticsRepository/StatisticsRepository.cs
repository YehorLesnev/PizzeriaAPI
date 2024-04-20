using Pizzeria.Domain.Dto.StatisticsDto;

namespace Pizzeria.Domain.Repository.StatisticsRepository
{
    public class StatisticsRepository(PizzeriaDbContext dbContext) : IStatisticsRepository
    {
        public IEnumerable<StaffPayrollResult> CalculateStaffPayroll(DateTime startDate, DateTime endDate)
        {
            return dbContext.CalculateStaffPayroll(startDate, endDate);
        }

        public IEnumerable<TotalSales> GetTotalSalesRevenueByDay(DateTime startDate, DateTime endDate)
        {
            return dbContext.GetTotalSalesRevenueByDay(startDate, endDate);
        }

        public IEnumerable<TotalSales> GetSalesDistributionByCategoryAndDay(string itemCategory, DateTime startDate, DateTime endDate)
        {
            return dbContext.GetSalesDistributionByCategoryAndDay(itemCategory, startDate, endDate);
        }

        public IEnumerable<AverageOrderTotalValue> GetAverageOrderValueByDay(DateTime startDate, DateTime endDate)
        {
            return dbContext.GetAverageOrderValueByDays(startDate, endDate);
        }

        public IEnumerable<AverageOrderTotalValue> GetAverageOrderValueByMonth(DateTime startDate, DateTime endDate)
        {
            return dbContext.GetAverageOrderValueByMonth(startDate, endDate);
        }

        public IEnumerable<AverageOrderTotalValue> GetAverageOrderValueByYear(DateTime startDate, DateTime endDate)
        {
            return dbContext.GetAverageOrderValueByYear(startDate, endDate);
        }
    }
}
