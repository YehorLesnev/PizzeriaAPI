using Pizzeria.Domain.Models;
using Pizzeria.Domain.Repository.Interfaces;

namespace Pizzeria.Domain.Services.CustomerService
{
    public class CustomerService(ICustomerRepository customerRepository) 
        : BaseService<Customer>(customerRepository), ICustomerService
    {
    }
}
