using Pizzeria.Domain.Models;

namespace Pizzeria.Domain.Repository.CustomerRepository
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task CreateAsync(Customer entity, string password);
        Task CreateAllAsync(IEnumerable<Customer> entities, string usersPassword);
        Task UpdateAsync(Customer entity);
    }
}
