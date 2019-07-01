using Lingva.Additional.Mapping.DataAdapter;
using Lingva.Background.Mapper;
using Lingva.DAL.EF.Context;
using Lingva.DAL.EF.Repositories;
using Lingva.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz.Spi;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.Background
{
    [ExcludeFromCodeCoverage]
    public static class ServiceExtensions
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddScoped<IDataAdapter, DataAdapter>();
            services.AddSingleton(AppMapperConfig.GetMapper());
        }

        public static void ConfigureQuartzSheduler(this IServiceCollection services)
        {
            services.AddTransient<IJobFactory, QuartzJobFactory>((provider) => new QuartzJobFactory(services.BuildServiceProvider()));
            services.AddTransient<EmailJob>();
        }

        public static void ConfigureEF(this IServiceCollection services, IConfiguration config)
        {
            services.ConfigureEFContext(config);
            services.ConfigureEFRepositories();
        }

        public static void ConfigureEFContext(this IServiceCollection services, IConfiguration config)
        {
            string connectionStringValue = config.GetConnectionString("LingvaEFConnection");

            services.AddDbContext<DictionaryContext>(options =>
            {
                options.UseSqlServer(connectionStringValue);
                //options.UseLazyLoadingProxies();
            });
        }

        public static void ConfigureEFRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}