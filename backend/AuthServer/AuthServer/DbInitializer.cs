using AuthServer.Identity;
using AuthServer.Identity.Contexts;
using AuthServer.Identity.Contexts.Factories;
using AuthServer.Identity.Entities;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace AuthServer
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IConfiguration config, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            AppIdentityDbContextFactory factory = new AppIdentityDbContextFactory();
            AppIdentityDbContext dbContext = factory.CreateDbContext(config);
            await dbContext.InitializeAsync();

            PersistedGrantDbContextFactory grantsFactory = new PersistedGrantDbContextFactory();
            PersistedGrantDbContext grantsContext = grantsFactory.CreateDbContext(config);
            await grantsContext.Database.MigrateAsync();

            await DataInitializer.AddDefaultRolesAsync(roleManager);
            await DataInitializer.AddDefaultUsersAsync(userManager);
        }
    }
}
