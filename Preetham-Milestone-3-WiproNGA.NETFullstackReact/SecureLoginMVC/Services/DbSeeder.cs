using Microsoft.AspNetCore.Identity;

namespace SecureLoginMVC.Services
{
    public class DbSeeder
    {
        private readonly RoleManager<IdentityRole> _roleMgr;
        private readonly UserManager<IdentityUser> _userMgr;

        public DbSeeder(RoleManager<IdentityRole> roleMgr, UserManager<IdentityUser> userMgr)
        {
            _roleMgr = roleMgr;
            _userMgr = userMgr;
        }

        public async Task SeedAsync()
        {
            // roles
            foreach (var role in new[] { "Admin", "Manager" })
            {
                if (!await _roleMgr.RoleExistsAsync(role))
                    await _roleMgr.CreateAsync(new IdentityRole(role));
            }

            // sample users (from the prompt)
            await EnsureUserAsync("admin", "Admin@123", "Admin");
            await EnsureUserAsync("manager1", "Manager@123", "Manager");
        }

        private async Task EnsureUserAsync(string username, string password, string role)
        {
            var user = await _userMgr.FindByNameAsync(username);
            if (user == null)
            {
                user = new IdentityUser { UserName = username };
                await _userMgr.CreateAsync(user, password);
            }
            if (!await _userMgr.IsInRoleAsync(user, role))
                await _userMgr.AddToRoleAsync(user, role);
        }
    }
}
