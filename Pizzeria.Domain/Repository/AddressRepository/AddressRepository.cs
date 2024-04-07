using Pizzeria.Domain.Models;

namespace Pizzeria.Domain.Repository.AddressRepository
{
    public class AddressRepository(PizzeriaDbContext dbContext) 
        : BaseRepository<Address>(dbContext), IAddressRepository;
}
