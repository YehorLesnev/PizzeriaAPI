﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pizzeria.Domain.Identity.DTO.User;
using Pizzeria.Domain.Identity.Roles;
using Pizzeria.Domain.Models;

namespace PizzeriaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoleNames.Admin)]
    public class RolesController(
        RoleManager<IdentityRole<Guid>> roleManager,
        UserManager<Customer> userManager) : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<IdentityRole<Guid>>> GetAllRoles()
        {
            return Ok(roleManager.Roles);
        }

        [HttpGet($"user/{{{nameof(userEmail)}}}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<string>>> GetUserRole([FromRoute] string userEmail)
        {
            var user = await userManager.FindByEmailAsync(userEmail);

            if(user is null) return NotFound();

            return Ok(await userManager.GetRolesAsync(user));
        }

        [HttpPost]
        public async Task<ActionResult> CreateRole([FromBody] string roleName)
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));

            return Ok();
        }

        [HttpPost("create-user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateUser([FromBody] RequestUserDto userDto)
        {
            var user = new Customer
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