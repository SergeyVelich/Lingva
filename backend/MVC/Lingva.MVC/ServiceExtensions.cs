using AutoMapper;
using Lingva.BC;
using Lingva.BC.Auth;
using Lingva.BC.Services;
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

        public static void ConfigureAuthJwt(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IAuthService, AuthService>();            

            string signingEncodingSecurityKey = config.GetSection("EncodingKey").Value;
            SigningSymmetricKey signingEncodingKey = new SigningSymmetricKey(signingEncodingSecurityKey);
            services.AddSingleton<IJwtSigningEncodingKey>(signingEncodingKey);

            string signingDecodingSecurityKey = config.GetSection("DecodingKey").Value;
            SigningSymmetricKey signingDecodingKey = new SigningSymmetricKey(signingDecodingSecurityKey);
            services.AddSingleton<IJwtSigningDecodingKey>(signingDecodingKey);

            AuthOptions authOptions = new AuthOptions()
            {

                Issuer = config.GetSection("AuthOptions:Issuer").Value,
                Audience = config.GetSection("AuthOptions:Audience").Value,
                Lifetime = Int32.Parse(config.GetSection("AuthOptions:Lifetime").Value)
            };

            services.Configure<AuthOptions>(config.GetSection("AuthOptions"));

            const string jwtSchemeName = JwtBearerDefaults.AuthenticationScheme;
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = jwtSchemeName;
                    options.DefaultChallengeScheme = jwtSchemeName;
                })
                .AddJwtBearer(jwtSchemeName, jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = signingDecodingKey.GetKey(),

                        ValidateIssuer = true,
                        ValidIssuer = authOptions.Issuer,

                        ValidateAudience = true,
                        ValidAudience = authOptions.Audience,

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
            services.AddScoped<IUnitOfWorkUser, UnitOfWorkUser>();
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryGroup, RepositoryGroup>();
            services.AddScoped<IRepositoryUser, RepositoryUser>();
        }
    }
}