using Microsoft.AspNetCore.Identity;
using Pizzeria.Domain.Identity.Roles;
using Pizzeria.Domain.Models;
using Pizzeria.Domain.Repository.CustomerRepository;

namespace Pizzeria.Domain.Services.CustomerService
{
    public class CustomerService(ICustomerRepository customerRepository, UserManager<Customer> userManager) 
        : BaseService<Customer>(customerRepository), ICustomerService
    {
        public override Task CreateAsync(Customer entity)
        {
            customerRepository.CreateAsync(entity);
            
            userManager.AddToRoleAsync(entity, UserRoleNames.Customer);

            return customerRepository.SaveAsync();
        }

        public override Task CreateAllAsync(IEnumerable<Customer> entities)
        {
            customerRepository.CreateAllAsync(entities);

            foreach(var entity in entities)
            {
                userManager.AddToRoleAsync(entity, UserRoleNames.Customer);
            }

            return customerRepository.SaveAsync();
        }
    }
}
