using IdentityServer4.AccessTokenValidation;
using Lingva.BC.Contracts;
using Lingva.BC.Crypto;
using Lingva.BC.Services;
using Lingva.WebAPI.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.WebAPI
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors();
            services.ConfigureSqlContext(Configuration);
            services.ConfigureOptions(Configuration);
            services.ConfigureIdentity(Configuration);
            services.ConfigureAutoMapper();
            services.ConfigureLoggerService();
            services.ConfigureUnitsOfWork();
            services.ConfigureRepositories();
           
            services.AddTransient<IGroupService, GroupService>();            
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IDefaultCryptoProvider, DefaultCryptoProvider>();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                    .AddIdentityServerAuthentication(options =>
                    {
                        options.Authority = "http://localhost:6050"; // Auth Server
                        options.RequireHttpsMetadata = false;
                        options.ApiName = "fiver_auth_api"; // API Resource Id
                    });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}
