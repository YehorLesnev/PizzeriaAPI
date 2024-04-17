using Microsoft.EntityFrameworkCore;
using Pizzeria.Domain.Models;
using System.Linq.Expressions;

namespace Pizzeria.Domain.Repository.OrderRepository
{
    public class OrderRepository(PizzeriaDbContext dbContext)
        : BaseRepository<Order>(dbContext), IOrderRepository
    {
        public override IEnumerable<Order> GetAll(Expression<Func<Order, bool>>? filter = null, bool asNoTracking = false)
        {
            IQueryable<Order> query = DbSet;

            if (filter is not null)
                query = query.Where(filter);

            return asNoTracking ? query.Include("OrderItems").Include("OrderItems.Item").AsNoTracking() 
                : query.Include("OrderItems").Include("OrderItems.Item");
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