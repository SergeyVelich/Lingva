using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;

namespace AuthServer.Identity.Contexts.Factories
{
    public class PersistedGrantDbContextFactory : DesignTimeDbContextFactoryBase<PersistedGrantDbContext>
    {
        public override PersistedGrantDbContext CreateDbContext(string[] args)
        {
            DbContextOptions<PersistedGrantDbContext> options = GetDbContextOptions();
            return new PersistedGrantDbContext(options, new OperationalStoreOptions());
        }
    }
}
