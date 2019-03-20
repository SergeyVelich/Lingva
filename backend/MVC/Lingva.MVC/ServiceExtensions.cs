using AutoMapper;
using Lingva.BC;
using Lingva.BC.Auth;
using Lingva.Common.Extensions;
using Lingva.Common.Mapping;
using Lingva.DAL.Context;
using Lingva.DAL.Repositories;
using Lingva.DAL.Repositories.Contracts;
using Lingva.DAL.UnitsOfWork;
using Lingva.DAL.UnitsOfWork.Contracts;
using Lingva.MVC.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.MVC.Extensions
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

        public static void ConfigureAuthEncodingKey(this IServiceCollection services, IConfiguration config)
        {
            string signingSecurityKey = config.GetSection("EncodingKey").Value;
            var signingKey = new SigningSymmetricKey(signingSecurityKey);
            services.AddSingleton<IJwtSigningEncodingKey>(signingKey);
        }

        public static void ConfigureAuthDecodingKey(this IServiceCollection services, IConfiguration config)
        {
            string signingSecurityKey = config.GetSection("DecodingKey").Value;
            var signingKey = new SigningSymmetricKey(signingSecurityKey);
            services.AddSingleton<IJwtSigningDecodingKey>(signingKey);
        }

        public static void ConfigureAuthOptions(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<AuthOptions>(config.GetSection("AuthOptions"));
        }

        public static void ConfigureAuthJwt(this IServiceCollection services, IConfiguration config)
        {
            string signingSecurityKey = config.GetSection("DecodingKey").Value;
            var signingKey = new SigningSymmetricKey(signingSecurityKey);

            const string jwtSchemeName = JwtBearerDefaults.AuthenticationScheme;
            var signingDecodingKey = (IJwtSigningDecodingKey)signingKey;
            services
                .AddAuthentication(options => {
                    options.DefaultAuthenticateScheme = jwtSchemeName;
                    options.DefaultChallengeScheme = jwtSchemeName;
                })
                .AddJwtBearer(jwtSchemeName, jwtBearerOptions => {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = signingDecodingKey.GetKey(),

                        ValidateIssuer = true,
                        ValidIssuer = "DemoApp",

                        ValidateAudience = true,
                        ValidAudience = "DemoAppClient",

                        ValidateLifetime = true,

                        ClockSkew = TimeSpan.FromSeconds(5)
                    };
                });
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
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryGroup, RepositoryGroup>();
            services.AddScoped<IRepositoryUser, RepositoryUser>();
        }
    }
}