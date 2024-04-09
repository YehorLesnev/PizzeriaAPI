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

            // Create default users
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
            
            string managerEmail = "manager@manager.com";
            string managerPassword = "PizzeriaManager123!";

            if (await userManager.FindByEmailAsync(managerEmail) == null)
            {
                var user = new IdentityUser
                {
                    UserName = "manager",
                    Email = managerEmail
                };

                await userManager.CreateAsync(user, managerPassword);

                await userManager.AddToRoleAsync(user, UserRoleNames.Manager);
            }

            string cashierEmail = "cashier@cashier.com";
            string cashierPassword = "PizzeriaCashier123!";

            if (await userManager.FindByEmailAsync(cashierEmail) == null)
            {
                var user = new IdentityUser
                {
                    UserName = "cashier",
                    Email = cashierEmail
                };

                await userManager.CreateAsync(user, cashierPassword);

                await userManager.AddToRoleAsync(user, UserRoleNames.Cashier);
            }

            string customerEmail = "customer@customer.com";
            string customerPassword = "PizzeriaCustomer123!";

            if (await userManager.FindByEmailAsync(customerEmail) == null)
            {
                var user = new IdentityUser
                {
                    UserName = "customer",
                    Email = customerEmail
                };

                await userManager.CreateAsync(user, customerPassword);

                await userManager.AddToRoleAsync(user, UserRoleNames.Customer);
            }
        }
    }
}
