using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AuthServer.Identity.Contexts.Factories
{
    public class PersistedGrantDbContextFactory : DesignTimeDbContextFactoryBase<PersistedGrantDbContext>
    {
        public PersistedGrantDbContext CreateDbContext(IConfiguration config)
        {
            DbContextOptions<PersistedGrantDbContext> options = GetDbContextOptions(config);
            return new PersistedGrantDbContext(options, new OperationalStoreOptions());
        }

        public override PersistedGrantDbContext CreateDbContext(string[] args)
        {
            DbContextOptions<PersistedGrantDbContext> options = GetDbContextOptions();
            return new PersistedGrantDbContext(options, new OperationalStoreOptions());
        }
    }
}
