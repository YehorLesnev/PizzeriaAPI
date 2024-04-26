using Pizzeria.Domain.Models;

namespace Pizzeria.Domain.Repository.LogRepository
{
    public class LogRepository(PizzeriaDbContext dbContext) :
        BaseRepository<Log>(dbContext), ILogRepository
    {
    }
}