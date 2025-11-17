using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SearchTicketApp.Data.Models;
using SearchTicketApp.Models.Constants;
using SearchTicketApp.Options;

namespace SearchTicketApp.Data.Seed
{
    public static class SeedUsers
    {
        public static async Task SeedUsersAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var credentials = scope.ServiceProvider.GetService<IOptions<AdminCredentials>>();
            using var userManager = scope.ServiceProvider.GetService<UserManager<User>>()!;
            using var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole<int>>>();

            var admin = new User()
            {
                UserName = credentials.Value.Email,
                Email = credentials.Value.Email,
            };

            var foundUser = await userManager.FindByEmailAsync(credentials.Value.Email);

            if (foundUser == null)
            {
                var createUserResult = await userManager.CreateAsync(admin, credentials.Value.Password);

                if (!createUserResult.Succeeded)
                    throw new InvalidOperationException("Failed to create admin!");

                admin = foundUser;
            }

            if (await userManager.IsInRoleAsync(admin, UserRoles.Admin))
                return;

            var addToRoleResult =  await userManager.AddToRolesAsync(admin, [UserRoles.Admin]);

            if (!addToRoleResult.Succeeded)
                throw new InvalidOperationException("Failed to add admin to role 'Admin'.");

        }
    }
}
