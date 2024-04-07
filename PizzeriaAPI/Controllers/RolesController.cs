using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzeriaAPI.Identity.Roles;

namespace PizzeriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoleNames.Admin)]
    public class RolesController(RoleManager<IdentityRole> roleManager) : ControllerBase
    {
        [HttpGet]
        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return roleManager.Roles;
        }
    }
}
