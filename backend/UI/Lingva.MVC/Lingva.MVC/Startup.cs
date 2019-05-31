using Lingva.ASP;
using Lingva.ASP.Extensions;
using Lingva.ASP.Infrastructure.Binders;
using Lingva.MVC.Extensions;
using Lingva.MVC.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

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
            services.ConfigureAuthentication(Configuration);
            services.ConfigureAutoMapper();
            services.ConfigureFilters();

            services.ConfigureDbProvider(Configuration);
            services.ConfigureManagers();
            services.ConfigureDataAdapters();

            services.AddMvc(options =>
            {
                options.ModelBinderProviders.Insert(0, new ModelBinderProvider());
                options.Filters.Add(typeof(GlobalExceptionFilter));
                options.CacheProfiles.Add("NoCashing",
                    new CacheProfile()
                    {
                        NoStore = true
                    });
                options.CacheProfiles.Add("Default30",
                    new CacheProfile()
                    {
                        NoStore = false,
                        Location = ResponseCacheLocation.Client,
                        Duration = 30
                    });
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");
                //app.UseExceptionHandler("/Home/Error");//??
                app.UseHsts();
            }

            var options = new RewriteOptions()
                .AddRedirect("(.*)/$", "$1")
                .AddRedirect("[h,H]ome[/]?$", "home/index");
            app.UseRewriter(options);

            app.UseCors("AllowAll");
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(config =>
            {
                config.MapRoute(name: "Default",
                    template: "{controller}/{action}",
                    defaults: new { Controller = "Home", Action = "Index" });
            });

            await DbInitializer.Initialize(Configuration);
        }
    }
}
