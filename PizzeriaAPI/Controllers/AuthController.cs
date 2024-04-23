using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pizzeria.Domain.Dto.Auth;
using Pizzeria.Domain.Dto.CustomerDto;
using Pizzeria.Domain.Mapper;
using Pizzeria.Domain.Models;
using Pizzeria.Domain.Services.CustomerService;
using PizzeriaAPI.Identity.JwtConfig;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PizzeriaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController(
        ICustomerService customerService,
        UserManager<Customer> userManager,
        IJwtTokenConfig jwtConfig) : ControllerBase
    {
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseCustomerDto>> RegisterAsync(
            [FromBody] RequestCustomerDto registerDto)
        {
            if (!ModelState.IsValid) return BadRequest("Wrong input");

            if (await userManager.FindByEmailAsync(registerDto.Email) is not null)
                return BadRequest("Customer with specified email is already exists");

            var customer = Mappers.MapRequestDtoToCustomer(registerDto);

            try
            {
                await customerService.CreateAsync(customer, registerDto.Password);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest("The password must contain capital letters, numbers and special symbol");
            }

            var createdCustomer = await customerService.GetAsync(r => r.Email == customer.Email);

            if (createdCustomer is null)
                return BadRequest("Couldn't create customer");

            return Created(nameof(RegisterAsync), Mappers.MapCustomerToResponseDto(createdCustomer));
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseLoginDto>> LoginAsync(
            [FromBody] RequestLoginDto loginDto)
        {
            if (!ModelState.IsValid) return BadRequest("Wrong input");

            var user = await userManager.FindByEmailAsync(loginDto.Email);

            if (user is null)
                return NotFound("No users with specified email found");

            if (await userManager.CheckPasswordAsync(user, loginDto.Password) == false)
                return BadRequest("Wrong password or email");

            var token = await GetJwtSecurityTokenAsync(user);
            var tokenExpirationDate = DateTime.Now.AddDays(jwtConfig.TokenLifetimeInDays);

            CookieOptions cookieOptions = new()
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Secure = true,
                Expires = tokenExpirationDate
            };

            Response.Cookies.Append(
                "jwt",
                new JwtSecurityTokenHandler().WriteToken(token),
                cookieOptions
                );

            return Ok(new ResponseLoginDto
            {
                Email = user.Email,
                Roles = await userManager.GetRolesAsync(user),
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expires = tokenExpirationDate
            });
        }

        private async Task<JwtSecurityToken?> GetJwtSecurityTokenAsync(Customer user)
        {
            var userRoles = await userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in userRoles)
                authClaims.Add(new Claim(ClaimTypes.Role, role));

            var tokenExpirationDate = DateTime.UtcNow.AddDays(jwtConfig.TokenLifetimeInDays);

            return new JwtSecurityToken(
                issuer: jwtConfig.JwtIssuer,
                audience: jwtConfig.JwtAudience,
                claims: authClaims,
                notBefore: DateTime.Now,
                expires: tokenExpirationDate,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtConfig.JwtKey)),
                    SecurityAlgorithms.HmacSha256)
                );
        }
    }
}
