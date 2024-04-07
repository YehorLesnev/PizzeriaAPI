using Pizzeria.Domain.Models;

namespace Pizzeria.Domain.Repository.CustomerRepository
{
    public class CustomerRepository(PizzeriaDbContext dbContext) 
        : BaseRepository<Customer>(dbContext), ICustomerRepository;
}
