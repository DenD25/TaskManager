using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            var roles = new List<Role>
            {
                new Role{Name = "Member"},
                new Role{Name = "Admin"},
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }
        }
    }
}
