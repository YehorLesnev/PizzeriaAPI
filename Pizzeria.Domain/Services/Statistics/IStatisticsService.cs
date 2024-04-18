using Pizzeria.Domain.Dto.StatisticsDto;

namespace Pizzeria.Domain.Services.Statistics
{
    public interface IStatisticsService
    {
        IEnumerable<StaffPayrollResult> CalculateStaffPayroll(DateTime startDate, DateTime endDate);
    }
}
