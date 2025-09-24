using Microsoft.AspNetCore.Identity;
using ResolveIQ.Web.Data;
using ResolveIQ.Web.Data.Auth;
using System.Threading.Tasks;

namespace ResolveIQ.Web.Data.Seeder
{
    public static class SeederExtensions
    {
        public static async Task SeedRolesAndUsersAsync(this ApplicationDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Seed Roles
            var roles = new[] { "Officer", "Manager" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Seed Users
            var users = new[]
            {
                new { UserName = "manager1@domain.com", FullName = "Manager One", Email = "manager1@domain.com", Role = "Manager" },
                new { UserName = "officer1@domain.com", FullName = "Officer One", Email = "officer1@domain.com", Role = "Officer" },
                new { UserName = "officer2@domain.com", FullName = "Officer Two", Email = "officer2@domain.com", Role = "Officer" },
                new { UserName = "officer3@domain.com", FullName = "Officer Three", Email = "officer3@domain.com", Role = "Officer" },
                new { UserName = "officer4@domain.com", FullName = "Officer Four", Email = "officer4@domain.com", Role = "Officer" }
            };

            foreach (var userInfo in users)
            {
                var user = await userManager.FindByEmailAsync(userInfo.Email);
                if (user == null)
                {
                    user = new AppUser { UserName = userInfo.UserName, Email = userInfo.Email, FullName = userInfo.FullName, EmailConfirmed = true, LockoutEnabled = false };
                    await userManager.CreateAsync(user, "Password123!"); // Use a strong password in production
                    await userManager.AddToRoleAsync(user, userInfo.Role);
                }
            }
        }
    }
}