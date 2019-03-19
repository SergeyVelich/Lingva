using AutoMapper;
using Lingva.Common.Mapping;
using Lingva.DAL.Context;
using Lingva.DAL.Repositories;
using Lingva.DAL.Repositories.Contracts;
using Lingva.DAL.UnitsOfWork;
using Lingva.DAL.UnitsOfWork.Contracts;
using Lingva.WebAPI.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.WebAPI.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceExtensions
    {
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

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
        {
            string connection = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<DictionaryContext>(options => options.UseSqlServer(connection));
        }

        public static void ConfigureOptions(this IServiceCollection services, IConfiguration config)
        {
            //services.Configure<StorageOptions>(config.GetSection("StorageConfig"));
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddScoped<IDataAdapter, DataAdapter>();
            services.AddSingleton<IMapper>(AppMapperConfig.GetMapper());
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            //services.AddSingleton<ILoggerFactory, LoggerManager>();
        }

        public static void ConfigureUnitsOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWorkGroupManagement, UnitOfWorkGroupManagement>();
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryGroup, RepositoryGroup>();
        }
    }
}