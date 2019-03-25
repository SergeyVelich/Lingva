using AutoMapper;
using Lingva.BC;
using Lingva.Common.Extensions;
using Lingva.Common.Mapping;
using Lingva.DAL.Context;
using Lingva.DAL.Entities;
using Lingva.DAL.Repositories;
using Lingva.DAL.Repositories.Contracts;
using Lingva.DAL.UnitsOfWork;
using Lingva.DAL.UnitsOfWork.Contracts;
using Lingva.WebAPI.Mapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
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
            string configStringValue = config.GetConnectionString("LingvaConnection");
            string configVariableName = configStringValue.GetVariableName();
            string connectionStringValue = Environment.GetEnvironmentVariable(configVariableName);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionStringValue));
    }

        public static void ConfigureOptions(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<StorageOptions>(config.GetSection("StorageConfig"));
        }

        public static void ConfigureIdentity(this IServiceCollection services, IConfiguration config)
        {
            string configStringValue = config.GetConnectionString("LingvaConnection");
            string configVariableName = configStringValue.GetVariableName();
            string connectionStringValue = Environment.GetEnvironmentVariable(configVariableName);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionStringValue));

            services.AddIdentity<Account, IdentityRole>()
                 .AddEntityFrameworkStores<ApplicationDbContext>();

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
            services.AddScoped<IUnitOfWorkGroup, UnitOfWorkGroup>();
            services.AddScoped<IUnitOfWorkAuth, UnitOfWorkAuth>();
            services.AddScoped<IUnitOfWorkAccount, UnitOfWorkAccount>();
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryGroup, RepositoryGroup>();
            services.AddScoped<IRepositoryAccount, RepositoryAccount>();
        }
    }
}