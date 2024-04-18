using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pizzeria.Domain.Identity.Roles;
using Pizzeria.Domain.Seeder;

namespace PizzeriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoleNames.Admin)]
    public class SeederController(ISeeder seeder)
        : ControllerBase
    {
        [HttpPut]
        public async Task Seed()
        {
            await seeder.Seed();
        }
    }
}
