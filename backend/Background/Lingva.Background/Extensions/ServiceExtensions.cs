using Lingva.Background.Mapper;
using Lingva.Common.Mapping;
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
            services.ConfigureSqlContext(config);
            services.ConfigureEFRepositories();
        }

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
        {
            string connectionStringValue = config.GetConnectionString("LingvaConnection");

            services.AddDbContext<DictionaryContext>(options =>
                options.UseSqlServer(connectionStringValue));
        }

        public static void ConfigureEFRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}