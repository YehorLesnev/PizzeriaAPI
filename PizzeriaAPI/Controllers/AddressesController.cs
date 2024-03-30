using Microsoft.AspNetCore.Mvc;
using Pizzeria.Domain.Models;
using Pizzeria.Domain.Services.AddressService;

namespace PizzeriaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AddressesController(
        IAddressService addressService
        ) : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Address> Get()
        {
            return addressService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Address?> Get(Guid id)
        {
            return await addressService.GetAsync(a => a.AddressId.Equals(id));
        }
    }
}