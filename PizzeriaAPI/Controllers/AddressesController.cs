using Microsoft.AspNetCore.Mvc;
using Pizzeria.Domain.Models;
using Pizzeria.Domain.Repository.Interfaces;
using Pizzeria.Domain.Services.AddressService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        [HttpPost]
        public async void Post([FromBody] string value)
        {
            
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
