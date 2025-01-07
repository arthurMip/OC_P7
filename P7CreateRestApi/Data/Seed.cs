using Microsoft.AspNetCore.Identity;

namespace P7CreateRestApi.Data;

public static class Seed
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using IServiceScope serviceScope = app.Services.CreateScope();
        var services = serviceScope.ServiceProvider;
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await CreateRolesAsync(roleManager);
        await CreateAdminAsync(userManager, roleManager);
    }

    public static async Task CreateRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            IdentityRole role = new()
            {
                Name = "Admin"
            };

            await roleManager.CreateAsync(role);
        }

        if (!await roleManager.RoleExistsAsync("User"))
        {
            IdentityRole role = new()
            {
                Name = "User",
            };

            await roleManager.CreateAsync(role);
        }
    }

    public static async Task CreateAdminAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        if (await userManager.FindByNameAsync("admin") is null)
        {
            IdentityUser user = new()
            {
                UserName = "admin"
            };

            await userManager.CreateAsync(user, "Admin12345!");
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}
