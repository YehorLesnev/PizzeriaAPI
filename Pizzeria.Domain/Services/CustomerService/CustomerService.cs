using Microsoft.AspNetCore.Identity;
using Pizzeria.Domain.Models;
using Pizzeria.Domain.Repository.CustomerRepository;

namespace Pizzeria.Domain.Services.CustomerService
{
    public class CustomerService(ICustomerRepository customerRepository, UserManager<Customer> userManager) 
        : BaseService<Customer>(customerRepository), ICustomerService
    {
        public override async Task CreateAsync(Customer entity)
        {
            await customerRepository.CreateAsync(entity);
        }

        public async Task CreateAsync(Customer entity, string password)
        {            
            await customerRepository.CreateAsync(entity, password);
        }

        public async Task CreateAllAsync(IEnumerable<Customer> entities, string usersPassword)
        {
            await customerRepository.CreateAllAsync(entities, usersPassword);
        }

        public override async Task CreateAllAsync(IEnumerable<Customer> entities)
        {
            foreach(var entity in entities)
            {
                await customerRepository.CreateAsync(entity);
            }
        }

        public override async Task UpdateAsync(Customer entity)
        {
            await customerRepository.UpdateAsync(entity);
        }
    }
}
