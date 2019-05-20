using Lingva.BC.Contracts;
using Lingva.BC.Services;
using Lingva.MVC.Extensions;
using Lingva.MVC.Filters;
using Lingva.MVC.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            services.ConfigureEF(Configuration);
            //services.ConfigureDapper(Configuration);
            //services.ConfigureMongo(Configuration);
            services.ConfigureOptions(Configuration);
            services.ConfigureAuthentication();
            services.ConfigureAutoMapper();

            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IInfoService, InfoService>();

            services.AddScoped<QueryOptionsAdapter>();

            services.AddMvc(options =>
            {
                //options.ModelBinderProviders.Insert(0, new OptionsModelBinderProvider());
                //options.Filters.Add(typeof(GlobalExceptionFilter));
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

            services.AddScoped<GlobalExceptionFilter>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            app.UseCors("CorsPolicy");
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(config =>
            {
                config.MapRoute(name: "Default",
                    template: "{controller}/{action}",
                    defaults: new {Controller = "Home", Action = "Index"});
            });
        }
    }
}
