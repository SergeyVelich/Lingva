using Microsoft.Extensions.Configuration;
using SenderService.SettingsProvider.EF.Context;
using System.Threading.Tasks;

namespace Lingva.Background
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IConfiguration config)
        {
            SenderDbContextFactory factory = new SenderDbContextFactory();
            SenderDbContext dbContext = factory.CreateDbContext(config);
            await dbContext.InitializeAsync(); ;
        }
    }
}
