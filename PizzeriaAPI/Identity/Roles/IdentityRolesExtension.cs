using Microsoft.AspNetCore.Identity;

namespace PizzeriaAPI.Identity.Roles
{
    public static class IdentityRolesExtension
    {
        public static async Task AddIdentityRoles(this WebApplication app)
        {
            // Create roles
            using var scope = app.Services.CreateScope();

            var roleManager =
                scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = new[] {
                UserRoleNames.Admin,
                UserRoleNames.Manager,
                UserRoleNames.Cashier,
                UserRoleNames.Customer
                };

            foreach (var role in roles)
            {
                if (false == await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Create default admin user
            var userManager =
                scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string adminEmail = "admin@admin.com";
            string adminPassword = "PizzeriaAdmin123!";

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var user = new IdentityUser
                {
                    UserName = "admin",
                    Email = adminEmail
                };

                await userManager.CreateAsync(user, adminPassword);

                await userManager.AddToRoleAsync(user, UserRoleNames.Admin);
            }
        }
    }
}
