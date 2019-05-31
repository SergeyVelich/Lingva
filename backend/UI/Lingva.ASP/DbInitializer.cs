using Lingva.DAL.AzureCosmosDB;
using Lingva.DAL.EF.Context;
using Lingva.DAL.Mongo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Lingva.ASP
{
    public static class DbInitializer
    {
        public static async Task Initialize(IConfiguration config)
        {
            DbProviders dbProvider = (DbProviders)Enum.Parse(typeof(DbProviders), config.GetSection("Selectors:DbProvider").Value, true);

            switch (dbProvider)
            {
                case DbProviders.Dapper:
                    DictionaryContextFactory factory = new DictionaryContextFactory();
                    DictionaryContext dbContext = factory.CreateDbContext(new string[0]);
                    await dbContext.Database.MigrateAsync();
                    break;
                case DbProviders.Mongo:
                    await new MongoContext(config).InitializeAsync();
                    break;
                case DbProviders.AzureCosmosDB:
                    await new AzureCosmosDBContext(config).InitializeAsync();
                    break;
                default:
                    factory = new DictionaryContextFactory();
                    dbContext = factory.CreateDbContext(new string[0]);
                    dbContext.Database.Migrate();
                    break;
            }
        }
    }
}
