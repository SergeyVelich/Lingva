using AuthServer.Identity;
using AuthServer.Identity.Contexts;
using AuthServer.Identity.Contexts.Factories;
using AuthServer.Identity.Entities;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthServer
{
    public static class DbInitializer
    {
        public static void Initialize(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            AppIdentityDbContextFactory factory = new AppIdentityDbContextFactory();
            AppIdentityDbContext dbContext = factory.CreateDbContext(new string[0]);
            dbContext.Database.Migrate();

            PersistedGrantDbContextFactory grantsFactory = new PersistedGrantDbContextFactory();
            PersistedGrantDbContext grantsContext = grantsFactory.CreateDbContext(new string[0]);
            grantsContext.Database.Migrate();

            DataInitializer.AddDefaultRolesAsync(roleManager).Wait();
            DataInitializer.AddDefaultUsersAsync(userManager).Wait();
        }
    }
}
