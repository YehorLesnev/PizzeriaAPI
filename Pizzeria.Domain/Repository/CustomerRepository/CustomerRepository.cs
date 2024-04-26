using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Domain.Identity.Roles;
using Pizzeria.Domain.Models;
using System.Linq.Expressions;

namespace Pizzeria.Domain.Repository.CustomerRepository
{
    public class CustomerRepository(PizzeriaDbContext dbContext, UserManager<Customer> userManager) 
        : BaseRepository<Customer>(dbContext), ICustomerRepository
    {
        public override IEnumerable<Customer> GetAll(Expression<Func<Customer, bool>>? filter = null,
            int? pageNumber = null,
            int? pageSize = null,
            bool asNoTracking = false)
        {
            IQueryable<Customer> query = DbSet;

            if (filter is not null)
                query = query.Where(filter);

            if (pageNumber is null || pageSize is null)
                return asNoTracking ? query.AsNoTracking() : query;

            query = query.OrderBy(x => x.FirstName).ThenBy(x => x.LastName);

            return asNoTracking ? query
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value)
                    .AsNoTracking()
                : query
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
        }

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

            if ((await userManager.CreateAsync(entity, Constants.Constants.DefaultUserPassword)).Succeeded)
            {
                await userManager.AddToRoleAsync(entity, UserRoleNames.Customer);
            }
        }

        public override async Task CreateAllAsync(IEnumerable<Customer> entities)
        {
            foreach(var entity in entities)
            {
                var normalizedEmail = entity.Email.ToUpper();

                entity.NormalizedEmail = normalizedEmail;
                entity.UserName = entity.Email;
                entity.NormalizedUserName = normalizedEmail;

                if ((await userManager.CreateAsync(entity, Constants.Constants.DefaultUserPassword)).Succeeded)
                {
                    await userManager.AddToRoleAsync(entity, UserRoleNames.Customer);
                }
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
                
                if ((await userManager.CreateAsync(entity, usersPassword)).Succeeded)
                {
                    await userManager.AddToRoleAsync(entity, UserRoleNames.Customer);
                }
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