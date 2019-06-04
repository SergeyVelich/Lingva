using AuthServer.Identity;
using AuthServer.Identity.Contexts;
using AuthServer.Identity.Contexts.Factories;
using AuthServer.Identity.Entities;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AuthServer
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            AppIdentityDbContextFactory factory = new AppIdentityDbContextFactory();
            AppIdentityDbContext dbContext = factory.CreateDbContext(new string[0]);
            await dbContext.Database.MigrateAsync();

            PersistedGrantDbContextFactory grantsFactory = new PersistedGrantDbContextFactory();
            PersistedGrantDbContext grantsContext = grantsFactory.CreateDbContext(new string[0]);
            await grantsContext.Database.MigrateAsync();

            await DataInitializer.AddDefaultRolesAsync(roleManager);
            await DataInitializer.AddDefaultUsersAsync(userManager);
        }
    }
}
