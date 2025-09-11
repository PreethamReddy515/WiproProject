using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace RoleBasedAuthApp.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string[] roles = { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            var admin = await userManager.FindByNameAsync("admin");
            if (admin == null)
            {
                admin = new IdentityUser { UserName = "admin", Email = "admin@example.com", EmailConfirmed = true };
                await userManager.CreateAsync(admin, "Admin@123");
                await userManager.AddToRoleAsync(admin, "Admin");
            }

            var user = await userManager.FindByNameAsync("user1");
            if (user == null)
            {
                user = new IdentityUser { UserName = "user1", Email = "user1@example.com", EmailConfirmed = true };
                await userManager.CreateAsync(user, "User@123");
                await userManager.AddToRoleAsync(user, "User");
            }
        }
    }
}
