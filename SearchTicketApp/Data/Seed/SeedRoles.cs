using Microsoft.AspNetCore.Identity;
using SearchTicketApp.Models.Constants;

namespace SearchTicketApp.Data.Seed
{
    public static class SeedRoles
    {
        public static async Task SeedRolesAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            using var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole<int>>>()!;

            var roles = UserRoles.GetRoles();

            foreach (var role in roles)
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole<int>(role));

        }
    }
}
