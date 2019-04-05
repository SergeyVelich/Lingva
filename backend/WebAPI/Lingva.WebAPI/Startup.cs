using Lingva.BC.Contracts;
using Lingva.BC.Services;
using Lingva.WebAPI.Extensions;
using Lingva.WebAPI.Infrastructure;
using Lingva.WebAPI.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            services.ConfigureAuthentication();
            services.ConfigureAutoMapper();
            services.ConfigureUnitsOfWork();
            services.ConfigureRepositories();
           
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<IInfoService, InfoService>();
            services.AddTransient<IUserService, UserService>();

            services.AddMvc(options =>
            {
                options.ModelBinderProviders.Insert(0, new OptionsModelBinderProvider());
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}
