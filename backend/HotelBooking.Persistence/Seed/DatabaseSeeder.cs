using HotelBooking.Domain.Entities;
using HotelBooking.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HotelBooking.Persistence.Seed
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
            var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("DatabaseSeeder");

            const string adminEmail = "admin@admin.com";
            const string adminPassword = "Ad_m1n";
            const string adminRole = nameof(UserRole.Admin);

            try
            {
                if (!await roleManager.RoleExistsAsync(adminRole))
                {
                    var role = new Role
                    {
                        Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                        Name = adminRole,
                        NormalizedName = adminRole.ToUpper()
                    };
                    await roleManager.CreateAsync(role);
                    logger.LogInformation("✅ Role '{Role}' created.", adminRole);
                }

                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if (adminUser == null)
                {
                    var userResult = User.Create(adminEmail);
                    if (userResult.IsFailure)
                    {
                        logger.LogError("Failed to create admin user entity: {Error}", userResult.Error);
                        return;
                    }

                    adminUser = userResult.Value;
                    adminUser.EmailConfirmed = true;

                    var createResult = await userManager.CreateAsync(adminUser, adminPassword);
                    if (!createResult.Succeeded)
                    {
                        foreach (var error in createResult.Errors)
                            logger.LogError("❌ Failed to create admin user: {Error}", error.Description);
                        return;
                    }

                    logger.LogInformation("✅ Admin user created with email: {Email}", adminEmail);
                }

                if (!await userManager.IsInRoleAsync(adminUser, adminRole))
                {
                    await userManager.AddToRoleAsync(adminUser, adminRole);
                    logger.LogInformation("✅ Role '{Role}' assigned to admin user.", adminRole);
                }

                logger.LogInformation("🎉 Database seeding completed successfully!");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "❌ Error occurred while seeding the database.");
            }
        }
    }
}
