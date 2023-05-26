using TaskManagerAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace TaskManagerAPI.Data
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
