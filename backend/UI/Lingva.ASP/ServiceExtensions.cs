using Lingva.ASP.Infrastructure;
using Lingva.BC;
using Lingva.BC.Contracts;
using Lingva.BC.Services;
using Lingva.DAL.Dapper;
using Lingva.DAL.EF.Context;
using Lingva.DAL.EF.Repositories;
using Lingva.DAL.Mongo;
using Lingva.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.ASP.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceExtensions
    {
        public static void ConfigureOptions(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<StorageOptions>(config.GetSection("StorageConfig"));
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }

        public static void ConfigureDbProvider(this IServiceCollection services, IConfiguration config)
        {
            DbProviders dbProvider = (DbProviders)Enum.Parse(typeof(DbProviders), config.GetSection("Selectors:DbProvider").Value, true);

            services.ConfigureCors();
            switch (dbProvider)
            {
                case DbProviders.Dapper:
                    services.ConfigureDapper();
                    break;
                case DbProviders.Mongo:
                    services.ConfigureMongo();
                    break;
                default:
                    services.ConfigureEF(config);
                    break;
            }
        }

        public static void ConfigureEF(this IServiceCollection services, IConfiguration config)
        {
            services.ConfigureEFContext(config);
            services.ConfigureEFRepositories();
        }

        public static void ConfigureDapper(this IServiceCollection services)
        {
            services.AddScoped<DapperContext>();
            services.ConfigureDapperRepositories();
        }

        public static void ConfigureMongo(this IServiceCollection services)
        {
            services.AddTransient<MongoContext>();
            services.ConfigureMongoRepositories();
        }

        public static void ConfigureEFContext(this IServiceCollection services, IConfiguration config)
        {
            string connectionStringValue = config.GetConnectionString("LingvaEFConnection");

            services.AddDbContext<DictionaryContext>(options =>
            {
                options.UseSqlServer(connectionStringValue);
                options.UseLazyLoadingProxies();
            });
        }
        
        public static void ConfigureEFRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
        }

        public static void ConfigureDapperRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository, DAL.Dapper.Repositories.Repository>();
        }

        public static void ConfigureMongoRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository, DAL.Mongo.Repositories.Repository>();
        }

        public static void ConfigureManagers(this IServiceCollection services)
        {
            services.AddScoped<IGroupManager, GroupManager>();
            services.AddScoped<IInfoManager, InfoManager>();
        }

        public static void ConfigureDataAdapters(this IServiceCollection services)
        {
            services.AddScoped<QueryOptionsAdapter>();
        }
    }
}