using Pizzeria.Domain.Models;

namespace Pizzeria.Domain.Services.CustomerService
{
    public interface ICustomerService : IBaseService<Customer>
    {
        Task CreateAsync(Customer entity, string password);

        Task CreateAllAsync(IEnumerable<Customer> entities, string usersPassword);
    }
}
