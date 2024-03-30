using Microsoft.AspNetCore.Mvc;
using Pizzeria.Domain.Seeder;

namespace PizzeriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeederController(ISeeder seeder)
        : ControllerBase
    {
        [HttpPut]
        public async Task Seed()
        {
            await seeder.SeedItems();
        }
    }
}
