using Pizzeria.Domain.Models;
using Pizzeria.Domain.Repository.CustomerRepository;

namespace Pizzeria.Domain.Services.CustomerService
{
    public class CustomerService(ICustomerRepository customerRepository) 
        : BaseService<Customer>(customerRepository), ICustomerService
    {
    }
}
