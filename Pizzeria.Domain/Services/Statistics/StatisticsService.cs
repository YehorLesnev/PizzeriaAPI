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

        public IEnumerable<TotalSalesDay> GetTotalSalesRevenueByDay(DateTime startDate, DateTime endDate)
        {
            return statisticsRepository.GetTotalSalesRevenueByDay(startDate, endDate);
        }

        public IEnumerable<AverageOrderTotalValueDay> GetAverageOrderValueByDays(DateTime startDate, DateTime endDate)
        {
            return statisticsRepository.GetAverageOrderValueByDays(startDate, endDate);
        }
    }
}
