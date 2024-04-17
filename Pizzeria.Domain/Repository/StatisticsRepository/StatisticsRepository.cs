using Pizzeria.Domain.Dto.StatisticsDto;

namespace Pizzeria.Domain.Repository.StatisticsRepository
{
    public class StatisticsRepository(PizzeriaDbContext dbContext) : IStatisticsRepository
    {
        public IEnumerable<StaffPayrollResult> CalculateStaffPayroll(DateTime startDate, DateTime endDate)
        {
            return dbContext.CalculateStaffPayroll(startDate, endDate);    
        }
    }
}
