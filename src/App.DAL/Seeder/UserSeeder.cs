using App.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace App.DAL.Seeder;

public static class UserSeeder
{
    public static async Task SeedAdminAsync(
        UserManager<AppUser> userManager,
        IConfiguration configuration)
    {
        var username = configuration["SeedAdmin:UserName"];
        var email = configuration["SeedAdmin:Email"];
        var password = configuration["SeedAdmin:Password"];

        if (string.IsNullOrEmpty(username) ||
            string.IsNullOrEmpty(email) ||
            string.IsNullOrEmpty(password))
        {
            throw new Exception("SeedAdmin configuration is missing");
        }

        var user = await userManager.FindByNameAsync(username);

        if (user == null)
        {
            user = new AppUser
            {
                UserName = username,
                Email = email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Admin creation failed: {errors}");
            }
        }
    }
}
