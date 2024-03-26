using Microsoft.AspNetCore.Mvc;
using Pizzeria.Domain.Models;
using Pizzeria.Domain.Repository.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PizzeriaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AddressesController(
        IAddressRepository addressRepository
        ) : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Address> Get()
        {
            return addressRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Address?> Get(Guid id)
        {
            return await addressRepository.GetAsync(a => a.AddressId.Equals(id));
        }

        [HttpPost]
        public void Post([FromBody] string value)
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
