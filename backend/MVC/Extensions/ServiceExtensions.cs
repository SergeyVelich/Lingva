using AutoMapper;
using Lingva.BusinessLayer;
using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Repositories;
using Lingva.DataAccessLayer.Repositories.Contracts;
using Lingva.DataAccessLayer.UnitsOfWork;
using Lingva.DataAccessLayer.UnitsOfWork.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Lingva.MVC.Extensions
{
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

        public static void ConfigureJwt(this IServiceCollection services, IOptions<StorageOptions> storageOptions)
        {
            //var key = Encoding.ASCII.GetBytes(storageOptions.Value.Secret);
            //services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(x =>
            //{
            //    x.Events = new JwtBearerEvents
            //    {
            //        OnTokenValidated = context =>
            //        {
            //            var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
            //            var userId = int.Parse(context.Principal.Identity.Name);
            //            var user = userService.GetById(userId);
            //            if (user == null)
            //            {
            //                context.Fail("Unauthorized");
            //            }
            //            return Task.CompletedTask;
            //        }
            //    };
            //    x.RequireHttpsMetadata = true;
            //    x.SaveToken = true;
            //    x.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(key),
            //        ValidateIssuer = false,
            //        ValidateAudience = false,
            //        ValidateLifetime = true
            //    };
            //});
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper();
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            //services.AddSingleton<ILoggerFactory, LoggerManager>();
        }

        public static void ConfigureUnitsOfWork(this IServiceCollection services)
        {
            //services.AddScoped<IUnitOfWorkDictionary, UnitOfWorkDictionary>();
            services.AddScoped<IUnitOfWorkGroupManagement, UnitOfWorkGroupManagement>();
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            //services.AddScoped<IRepositoryWord, RepositoryWord>();
            //services.AddScoped<IRepositoryDictionaryRecord, RepositoryDictionaryRecord>();
            services.AddScoped<IRepositoryGroup, RepositoryGroup>();
        }
    }
}