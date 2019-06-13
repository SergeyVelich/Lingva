using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace Lingva.DAL.EF.Context
{
    public abstract class DesignTimeDbContextFactoryBase<TContext> : IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        public abstract TContext CreateDbContext(string[] args);

        public DbContextOptions<TContext> GetDbContextOptions()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var basePath = Directory.GetCurrentDirectory(); //var basePath = AppContext.BaseDirectory;
            return GetDbContextOptions(basePath, environmentName);
        }

        private DbContextOptions<TContext> GetDbContextOptions(string basePath, string environmentName)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .AddEnvironmentVariables();

            var config = builder.Build();

            string connectionStringValue = config.GetConnectionString("LingvaEFConnection");

            if (string.IsNullOrWhiteSpace(connectionStringValue))
            {
                throw new InvalidOperationException(
                    "Could not find a connection string named 'Default'.");
            }
            return GetDbContextOptions(connectionStringValue);
        }

        private DbContextOptions<TContext> GetDbContextOptions(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException(
             $"{nameof(connectionString)} is null or empty.",
             nameof(connectionString));

            var optionsBuilder = new DbContextOptionsBuilder<TContext>();

            Console.WriteLine("DesignTimeDbContextFactory.Create(string): Connection string: {0}", connectionString);

            optionsBuilder.UseSqlServer(connectionString, 
                sql => sql.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name));

            var options = optionsBuilder.Options;
            return options;
        }
    }
}
