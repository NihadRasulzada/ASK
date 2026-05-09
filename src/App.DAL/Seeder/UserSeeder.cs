using App.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace App.DAL.Seeder;

public static class UserSeeder
{
    public static async Task SeedAdminAsync(
        UserManager<AppUser> userManager,
        IConfiguration configuration,
        ILogger logger)
    {
        var username = configuration["SeedAdmin:UserName"];
        var email = configuration["SeedAdmin:Email"];
        var password = configuration["SeedAdmin:Password"];

        if (string.IsNullOrEmpty(username) ||
            string.IsNullOrEmpty(email) ||
            string.IsNullOrEmpty(password))
        {
            logger.LogWarning("SeedAdmin is enabled but configuration is incomplete. Skipping admin seed.");
            return;
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

            logger.LogInformation("Seed admin user created: {Username}", username);
        }
    }
}
