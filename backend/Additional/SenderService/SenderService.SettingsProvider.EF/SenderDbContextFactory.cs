using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SenderService.SettingsProvider.EF.Context
{
    public class SenderDbContextFactory : DesignTimeDbContextFactoryBase<SenderDbContext>
    {
        public SenderDbContext CreateDbContext(IConfiguration config)
        {
            DbContextOptions<SenderDbContext> options = GetDbContextOptions(config);
            return new SenderDbContext(options);
        }

        public override SenderDbContext CreateDbContext(string[] args)
        {
            DbContextOptions<SenderDbContext> options = GetDbContextOptions();
            return new SenderDbContext(options);
        }
    }
}
