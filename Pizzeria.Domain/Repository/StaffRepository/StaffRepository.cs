﻿using Microsoft.EntityFrameworkCore;
using Pizzeria.Domain.Models;
using System.Linq.Expressions;

namespace Pizzeria.Domain.Repository.StaffRepository
{
    public class StaffRepository(PizzeriaDbContext dbContext)
        : BaseRepository<Staff>(dbContext), IStaffRepository
    {
        public override IEnumerable<Staff> GetAll(Expression<Func<Staff, bool>>? filter = null,
            int? pageNumber = null,
            int? pageSize = null,
            bool asNoTracking = false)
        {
            IQueryable<Staff> query = DbSet;

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

        public IEnumerable<Staff> GetAllWithOrders(Expression<Func<Staff, bool>>? filter = null, int? pageNumber = null, int? pageSize = null,
            bool asNoTracking = false)
        {
            IQueryable<Staff> query = DbSet;

            if (filter is not null)
                query = query.Where(filter);

            query = query.Include("Orders");

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

        public async Task<Staff?> GetWithOrdersAsync(Expression<Func<Staff, bool>>? filter = null, bool asNoTracking = false)
        {
            IQueryable<Staff> query = DbSet;

            query = query.Include("Orders");

            if (filter is not null)
                query = query.Where(filter);

            return asNoTracking ? await query.AsNoTracking().FirstOrDefaultAsync() : await query.FirstOrDefaultAsync();
        }
    }
}
