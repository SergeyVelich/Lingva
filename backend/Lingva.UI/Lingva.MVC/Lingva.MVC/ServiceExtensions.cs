using Lingva.Additional.Mapping.DataAdapter;
using Lingva.MVC.Filters;
using Lingva.MVC.Mapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.MVC.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceExtensions
    {
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration config)
        {
            bool useIs = bool.Parse(config.GetSection("UseIS").Value);

            if (!useIs)
            {
                return;
            }
                
            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();//не совсем понятно, для чего это

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddOpenIdConnect(options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme; // cookie middle setup above
                options.Authority = "http://localhost:6050"; // Auth Server  
                options.RequireHttpsMetadata = false; // only for development   
                options.ClientId = "mvc_client"; // client setup in Auth Server  
                options.ClientSecret = "secret";
                options.ResponseType = "code id_token"; // means Hybrid flow (id + access token)  
                options.Scope.Add("resourceapi");
                options.Scope.Add("offline_access");
                options.GetClaimsFromUserInfoEndpoint = true;
                options.SaveTokens = true;
            });
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddScoped<IDataAdapter, DataAdapter>();
            services.AddSingleton(AppMapperConfig.GetMapper());
        }

        public static void ConfigureCaching(this IServiceCollection services)
        {
            services.AddResponseCaching(options =>
            {
                options.UseCaseSensitivePaths = false;
                options.MaximumBodySize = 1024;
            });
        }

        public static void ConfigureFilters(this IServiceCollection services)
        {
            services.AddScoped<GlobalExceptionFilter>();
        }
    }
}