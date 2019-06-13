using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Lingva.DAL.EF.Context
{
    public class DictionaryContextFactory : DesignTimeDbContextFactoryBase<DictionaryContext>
    {
        public DictionaryContext CreateDbContext(IConfiguration config)
        {
            DbContextOptions<DictionaryContext> options = GetDbContextOptions(config);
            return new DictionaryContext(options);
        }

        public override DictionaryContext CreateDbContext(string[] args)
        {
            DbContextOptions<DictionaryContext> options = GetDbContextOptions();
            return new DictionaryContext(options);
        }
    }
}
