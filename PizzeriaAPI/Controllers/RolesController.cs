using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzeriaAPI.Identity.DTO.User;
using PizzeriaAPI.Identity.Roles;

namespace PizzeriaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoleNames.Admin)]
    public class RolesController(
        RoleManager<IdentityRole> roleManager,
        UserManager<IdentityUser> userManager) : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<IdentityRole>> GetAllRoles()
        {
            return Ok(roleManager.Roles);
        }

        [HttpPost]
        public async Task<ActionResult> CreateRole([FromBody] string roleName)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));

            return Ok();
        }

        [HttpPost("create-user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateUser([FromBody] RequestUserDto userDto)
        {
            var user = new IdentityUser
            {
                UserName = userDto.UserName,
                Email = userDto.Email
            };

            try
            {
                await userManager.CreateAsync(user, userDto.Password);

                // Default roles
                await userManager.AddToRoleAsync(user, UserRoleNames.Customer);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPost("assign-role/{userEmail}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AssignRoleToUser([FromRoute] string userEmail, [FromBody] string roleName)
        {
            var user = await userManager.FindByEmailAsync(userEmail);

            if (user is null) return NotFound("No users with such email found");
            
            try
            {
                await userManager.AddToRoleAsync(user, roleName);
            } 
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
