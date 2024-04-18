using Pizzeria.Domain.Dto.StatisticsDto;
using Pizzeria.Domain.Repository.StatisticsRepository;

namespace Pizzeria.Domain.Services.Statistics
{
    public class StatisticsService(IStatisticsRepository statisticsRepository) : IStatisticsService
    {
        public IEnumerable<StaffPayrollResult> CalculateStaffPayroll(DateTime startDate, DateTime endDate)
        {
            return statisticsRepository.CalculateStaffPayroll(startDate, endDate);
        }
    }
}
