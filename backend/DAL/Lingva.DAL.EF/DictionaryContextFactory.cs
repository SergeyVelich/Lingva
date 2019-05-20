using Microsoft.EntityFrameworkCore;

namespace Lingva.DAL.EF.Context
{
    public class DictionaryContextFactory : DesignTimeDbContextFactoryBase<DictionaryContext>
    {
        public override DictionaryContext CreateDbContext(string[] args)
        {
            DbContextOptions<DictionaryContext> options = GetDbContextOptions();
            return new DictionaryContext(options);
        }
    }
}
