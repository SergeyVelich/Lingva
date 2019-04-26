using IdentityServer4.AccessTokenValidation;
using Lingva.BC;
using Lingva.Common.Extensions;
using Lingva.Common.Mapping;
using Lingva.DAL.Dapper;
using Lingva.DAL.EF.Context;
using Lingva.DAL.EF.Repositories;
using Lingva.DAL.Repositories;
using Lingva.WebAPI.Mapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;

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

        public static void ConfigureEF(this IServiceCollection services, IConfiguration config)
        {
            services.ConfigureSqlContext(config);
            services.ConfigureEFRepositories();
        }

        public static void ConfigureDapper(this IServiceCollection services)
        {
            services.AddScoped<DapperContext>();           
            services.ConfigureDapperRepositories();
        }

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
        {
            string configStringValue = config.GetConnectionString("LingvaConnection");
            string configVariableName = configStringValue.GetVariableName();
            string connectionStringValue = Environment.GetEnvironmentVariable(configVariableName);

            services.AddDbContext<DictionaryContext>(options =>
            {
                options.UseSqlServer(connectionStringValue);
                options.UseLazyLoadingProxies();
            });
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
            services.AddSingleton(AppMapperConfig.GetMapper());
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Lingva API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Serhii Behma",
                        Email = string.Empty,
                        //Url = "https://twitter.com/spboyer"
                    },
                    License = new License
                    {
                        Name = "Use under LICX",
                        //Url = "https://example.com/license"
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public static void ConfigureEFRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        public static void ConfigureDapperRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository, DAL.Dapper.Repositories.Repository>();
        }
    }
}