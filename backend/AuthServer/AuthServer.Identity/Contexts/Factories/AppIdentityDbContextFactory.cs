using Microsoft.EntityFrameworkCore;

namespace AuthServer.Identity.Contexts.Factories
{
    public class AppIdentityDbContextFactory : DesignTimeDbContextFactoryBase<AppIdentityDbContext>
    {
        public override AppIdentityDbContext CreateDbContext(string[] args)
        {
            DbContextOptions<AppIdentityDbContext> options = GetDbContextOptions();
            return new AppIdentityDbContext(options);
        }
    }
}
