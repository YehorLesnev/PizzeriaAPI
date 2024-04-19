using Microsoft.AspNetCore.Identity;
using Pizzeria.Domain.Identity.Roles;
using Pizzeria.Domain.Models;
using System.Runtime.CompilerServices;

namespace Pizzeria.Domain.Repository.CustomerRepository
{
    public class CustomerRepository(PizzeriaDbContext dbContext, UserManager<Customer> userManager) 
        : BaseRepository<Customer>(dbContext), ICustomerRepository
    {
        public async Task CreateAsync(Customer entity, string password)
        {
            var normalizedEmail = entity.Email.ToUpper();

            entity.NormalizedEmail = normalizedEmail;
            entity.UserName = entity.Email;
            entity.NormalizedUserName = normalizedEmail;

            await userManager.CreateAsync(entity, password);
            await userManager.AddToRoleAsync(entity, UserRoleNames.Customer);
        }

        public override async Task CreateAsync(Customer entity)
        {
            var normalizedEmail = entity.Email.ToUpper();

            entity.NormalizedEmail = normalizedEmail;
            entity.UserName = entity.Email;
            entity.NormalizedUserName = normalizedEmail;

            await userManager.CreateAsync(entity, "Password123!");
            await userManager.AddToRoleAsync(entity, UserRoleNames.Customer);
        }

        public override async Task CreateAllAsync(IEnumerable<Customer> entities)
        {
            foreach(var entity in entities)
            {
                var normalizedEmail = entity.Email.ToUpper();

                entity.NormalizedEmail = normalizedEmail;
                entity.UserName = entity.Email;
                entity.NormalizedUserName = normalizedEmail;

                await userManager.CreateAsync(entity, "Password123!");
                await userManager.AddToRoleAsync(entity, UserRoleNames.Customer);
            } 
        }

        public async Task CreateAllAsync(IEnumerable<Customer> entities, string usersPassword)
        {
            foreach(var entity in entities)
            {
                var normalizedEmail = entity.Email.ToUpper();

                entity.NormalizedEmail = normalizedEmail;
                entity.UserName = entity.Email;
                entity.NormalizedUserName = normalizedEmail;

                await userManager.CreateAsync(entity, usersPassword);
                await userManager.AddToRoleAsync(entity, UserRoleNames.Customer);
            } 
        }

        public async Task UpdateAsync(Customer entity)
        {
            await userManager.UpdateAsync(entity);
            await userManager.UpdateSecurityStampAsync(entity);
            await userManager.UpdateNormalizedEmailAsync(entity);
            await userManager.UpdateNormalizedUserNameAsync(entity);
        }
    }
}