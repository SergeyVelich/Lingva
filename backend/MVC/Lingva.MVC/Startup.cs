using Lingva.MVC.Extensions;
using Lingva.MVC.Filters;
using Lingva.MVC.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.MVC
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
            services.ConfigureOptions(Configuration);
            services.ConfigureAuthentication();
            services.ConfigureAutoMapper();

            services.AddHttpClient();
            services.AddMvc(options =>
            {
                //options.ModelBinderProviders.Insert(0, new OptionsModelBinderProvider());
                options.Filters.Add(typeof(GlobalExceptionFilter));
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseCors("CorsPolicy");
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}
