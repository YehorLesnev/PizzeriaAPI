using Microsoft.Data.SqlClient;
using Pizzeria.Domain.Dto.StatisticsDto;

namespace Pizzeria.Domain.Repository.StatisticsRepository
{
    public interface IStatisticsRepository
    {
        IEnumerable<StaffPayrollResult> CalculateStaffPayroll(DateTime startDate, DateTime endDate);

        IEnumerable<TotalSalesDay> GetTotalSalesRevenueByDay(DateTime startDate, DateTime endDate);

    }
}
