using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AuthServer.Identity.Contexts.Factories
{
    public class AppIdentityDbContextFactory : DesignTimeDbContextFactoryBase<AppIdentityDbContext>
    {
        public AppIdentityDbContext CreateDbContext(IConfiguration config)
        {
            DbContextOptions<AppIdentityDbContext> options = GetDbContextOptions(config);
            return new AppIdentityDbContext(options);
        }

        public override AppIdentityDbContext CreateDbContext(string[] args)
        {
            DbContextOptions<AppIdentityDbContext> options = GetDbContextOptions();
            return new AppIdentityDbContext(options);
        }
    }
}
