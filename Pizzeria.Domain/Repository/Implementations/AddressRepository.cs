using Microsoft.EntityFrameworkCore;
using Pizzeria.Domain.Models;
using Pizzeria.Domain.Repository.Interfaces;

namespace Pizzeria.Domain.Repository.Implementations
{
    public class AddressRepository(PizzeriaDbContext dbContext) 
        : BaseRepository<Address>(dbContext), IAddressRepository;
}
