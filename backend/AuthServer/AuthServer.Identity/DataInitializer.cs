using AuthServer.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace AuthServer.Identity
{
    [ExcludeFromCodeCoverage]
    public class DataInitializer
    {
        public static async Task AddDefaultRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (await roleManager.FindByNameAsync("User") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }
        }

        public static async Task AddDefaultUsersAsync(UserManager<AppUser> userManager)
        {
            string Name = "Sergey";
            string Email = "veloceraptor89@gmail.com";
            string Password = "Qwerty1!";
            if (await userManager.FindByNameAsync(Email) == null)
            {
                AppUser admin = new AppUser { Email = Email, UserName = Name };
                IdentityResult result = await userManager.CreateAsync(admin, Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }

            Name = "Begma";
            Email = "veloceraptor89@gmail.com";
            Password = "Qwerty1!";
            if (await userManager.FindByNameAsync(Email) == null)
            {
                AppUser user = new AppUser { Email = Email, UserName = Name };
                IdentityResult result = await userManager.CreateAsync(user, Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "user");
                }
            }
        }
    }
}
