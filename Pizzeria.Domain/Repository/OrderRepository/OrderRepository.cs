using Microsoft.EntityFrameworkCore;
using Pizzeria.Domain.Models;
using System.Linq.Expressions;

namespace Pizzeria.Domain.Repository.OrderRepository
{
    public class OrderRepository(PizzeriaDbContext dbContext)
        : BaseRepository<Order>(dbContext), IOrderRepository
    {
        public override IEnumerable<Order> GetAll(
            Expression<Func<Order, bool>>? filter = null,
            int? pageNumber = null,
            int? pageSize = null,
            bool asNoTracking = false)
        {
            IQueryable<Order> query = DbSet;

            if (filter is not null)
                query = query.Where(filter);

            if (pageNumber is null || pageSize is null)
                return asNoTracking ? query.Include("OrderItems").Include("OrderItems.Item").AsNoTracking()
                    : query.Include("OrderItems").Include("OrderItems.Item");

            return asNoTracking ? query.Include("OrderItems").Include("OrderItems.Item")
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value)
                    .AsNoTracking()
                : query.Include("OrderItems").Include("OrderItems.Item")
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
        }

        public IEnumerable<Order> GetAllByUserEmail(
            string userEmail,
            Expression<Func<Order, bool>>? filter = null,
            int? pageNumber = null,
            int? pageSize = null,
            bool asNoTracking = false)
        {
            IQueryable<Order> query = DbSet;

            if (filter is not null)
                query = query.Where(filter);

            query = asNoTracking ? query.Include("OrderItems").Include("OrderItems.Item").Include("Customer").AsNoTracking()
                : query.Include("OrderItems").Include("OrderItems.Item").Include("Customer");

            if (pageNumber is not null && pageSize is not null)
            {
                query = query
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }

            return query.AsEnumerable().Where(x => x.Customer is not null && x.Customer.Email is not null && x.Customer.Email.Equals(userEmail, StringComparison.OrdinalIgnoreCase));
        }

        public override async Task<Order?> GetAsync(Expression<Func<Order, bool>>? filter = null, bool asNoTracking = false)
        {
            IQueryable<Order> query = DbSet;

            if (filter is not null)
                query = query.Where(filter);

            return asNoTracking ? await query.Include("OrderItems").Include("OrderItems.Item").AsNoTracking().FirstOrDefaultAsync()
                : await query.Include("OrderItems").Include("OrderItems.Item").FirstOrDefaultAsync();
        }
    }
}