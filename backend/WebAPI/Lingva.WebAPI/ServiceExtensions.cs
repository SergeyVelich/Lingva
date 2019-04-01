using AutoMapper;
using IdentityServer4.AccessTokenValidation;
using Lingva.BC;
using Lingva.Common.Extensions;
using Lingva.Common.Mapping;
using Lingva.DAL.Context;
using Lingva.DAL.Repositories;
using Lingva.DAL.Repositories.Contracts;
using Lingva.DAL.UnitsOfWork;
using Lingva.DAL.UnitsOfWork.Contracts;
using Lingva.WebAPI.Mapper;
using Microsoft.AspNetCore.Builder;
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

            services.AddDbContext<DictionaryContext>(options =>
                options.UseSqlServer(connectionStringValue));
        }

        public static void ConfigureOptions(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<StorageOptions>(config.GetSection("StorageConfig"));
        }

        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                    .AddIdentityServerAuthentication(options =>
                    {
                        options.Authority = "http://localhost:6050"; // Auth Server
                        options.RequireHttpsMetadata = false;
                        options.ApiName = "fiver_auth_api"; // API Resource Id
                    });
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddScoped<IDataAdapter, DataAdapter>();
            services.AddSingleton<IMapper>(AppMapperConfig.GetMapper());
        }

        public static void ConfigureUnitsOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWorkGroup, UnitOfWorkGroup>();
            services.AddScoped<IUnitOfWorkInfo, UnitOfWorkInfo>();
            services.AddScoped<IUnitOfWorkUser, UnitOfWorkUser>();
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryGroup, RepositoryGroup>();
            services.AddScoped<IRepositoryLanguage, RepositoryLanguage>();
            services.AddScoped<IRepositoryUser, RepositoryUser>();
        }
    }
}