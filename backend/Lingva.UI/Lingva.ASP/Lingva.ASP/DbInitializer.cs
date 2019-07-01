using Lingva.DAL.CosmosSqlApi;
using Lingva.DAL.EF.Context;
using Lingva.DAL.Mongo;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Lingva.ASP
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IConfiguration config)
        {
            DbProviders dbProvider = (DbProviders)Enum.Parse(typeof(DbProviders), config.GetSection("Selectors:DbProvider").Value, true);

            switch (dbProvider)
            {
                case DbProviders.Dapper:
                    DictionaryContextFactory factory = new DictionaryContextFactory();
                    DictionaryContext dbContext = factory.CreateDbContext(config);
                    await dbContext.InitializeAsync();
                    break;
                case DbProviders.Mongo:
                    await new MongoContext(config).InitializeAsync();
                    break;
                case DbProviders.CosmosSqlApi:
                    await new CosmosSqlApiContext(config).InitializeAsync();
                    break;
                default:
                    factory = new DictionaryContextFactory();
                    dbContext = factory.CreateDbContext(config);
                    await dbContext.InitializeAsync();
                    break;
            }
        }
    }
}
