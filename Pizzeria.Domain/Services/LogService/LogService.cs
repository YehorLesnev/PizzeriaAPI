using Pizzeria.Domain.Models;
using Pizzeria.Domain.Repository.LogRepository;

namespace Pizzeria.Domain.Services.LogService
{
    public class LogService(ILogRepository logRepository) : BaseService<Log>(logRepository), ILogService
    {
    }
}
