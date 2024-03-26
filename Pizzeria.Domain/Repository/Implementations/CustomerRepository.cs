using Pizzeria.Domain.Models;
using Pizzeria.Domain.Repository.Interfaces;

namespace Pizzeria.Domain.Repository.Implementations
{
    public class CustomerRepository(PizzeriaDbContext dbContext) 
        : BaseRepository<Customer>(dbContext), ICustomerRepository;
}
