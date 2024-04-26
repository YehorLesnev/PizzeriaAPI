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

            query = query.OrderByDescending(x => x.Date);

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

            query = query.Where(x => x.Customer != null 
                && x.Customer.Email != null
                && x.Customer.Email.Equals(userEmail));

            if (filter is not null)
                query = query.Where(filter);

            query = asNoTracking ? query.Include("OrderItems").Include("OrderItems.Item").Include("Customer").AsNoTracking()
                : query.Include("OrderItems").Include("OrderItems.Item").Include("Customer");

            if (pageNumber is not null && pageSize is not null)
            {
                return query
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value)
                    .OrderByDescending(x => x.Date);
            }

            return query.OrderByDescending(x => x.Date);
        }

        public override async Task<Order?> GetAsync(Expression<Func<Order, bool>>? filter = null, bool asNoTracking = false)
        {
            IQueryable<Order> query = DbSet;

            if (filter is not null)
                query = query.Where(filter);

            return asNoTracking ? await query.Include("OrderItems").Include("OrderItems.Item").AsNoTracking().FirstOrDefaultAsync()
                : await query.Include("OrderItems").Include("OrderItems.Item").FirstOrDefaultAsync();
        }

        public IEnumerable<Order> GetAllWithFullInfo(
            Expression<Func<Order, bool>>? filter = null,
            int? pageNumber = null,
            int? pageSize = null,
            bool asNoTracking = false)
        {
            IQueryable<Order> query = DbSet;

            if (filter is not null)
                query = query.Where(filter);

            query = query.OrderByDescending(x => x.Date);

            if (pageNumber is null || pageSize is null)
                return asNoTracking ? query
                    .Include("OrderItems")
                    .Include("OrderItems.Item")
                    .Include("Customer")
                    .Include("Staff")
                    .Include("DeliveryAddress")
                    .AsNoTracking()
                    : query
                        .Include("OrderItems")
                        .Include("OrderItems.Item")
                        .Include("Customer")
                        .Include("Staff")
                        .Include("DeliveryAddress");

            return asNoTracking ? query
                    .Include("OrderItems")
                    .Include("OrderItems.Item")
                    .Include("Customer")
                    .Include("Staff")
                    .Include("DeliveryAddress")
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value)
                    .AsNoTracking()
                : query
                    .Include("OrderItems")
                    .Include("OrderItems.Item")
                    .Include("Customer")
                    .Include("Staff")
                    .Include("DeliveryAddress")
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
        }
    }
}