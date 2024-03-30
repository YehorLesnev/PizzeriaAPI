using Pizzeria.Domain.Models;
using Pizzeria.Domain.Repository.Interfaces;

namespace Pizzeria.Domain.Services.AddressService
{
    public class AddressService(IAddressRepository addressRepository)
    : BaseService<Address>(addressRepository), IAddressService
    {
    }
}
