using Pizzeria.Domain.Dto.StatisticsDto;

namespace Pizzeria.Domain.Repository.StatisticsRepository
{
    public class StatisticsRepository(PizzeriaDbContext dbContext) : IStatisticsRepository
    {
        public IEnumerable<StaffPayrollResult> CalculateStaffPayroll(DateTime startDate, DateTime endDate)
        {
            return dbContext.CalculateStaffPayroll(startDate, endDate);    
        }

        public IEnumerable<TotalSalesDay> GetTotalSalesRevenueByDay(DateTime startDate, DateTime endDate)
        {
            return dbContext.GetTotalSalesRevenueByDay(startDate, endDate);    
        }
        
        public IEnumerable<AverageOrderTotalValueDay> GetAverageOrderValueByDays(DateTime startDate, DateTime endDate)
        {
            return dbContext.GetAverageOrderValueByDays(startDate, endDate);    
        }
    }
}
