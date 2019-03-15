using Lingva.BC.Contracts;
using Lingva.BC.Services;
using Lingva.MVC.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lingva.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors();
            services.ConfigureSqlContext(Configuration);
            services.ConfigureOptions(Configuration);
            //services.ConfigureJwt(services.);
            services.ConfigureAutoMapper();
            services.ConfigureLoggerService();
            services.ConfigureUnitsOfWork();
            services.ConfigureRepositories();

            services.AddTransient<IDictionaryService, DictionaryService>();
            services.AddTransient<IGroupManagementService, GroupManagementService>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvcWithDefaultRoute();
            //app.UseMvc(routes =>
            //{
            //routes.MapRoute(
            //    name: "default",
            //    template: "groups",
            //    new { controller = "GroupManagement", action = "Index" });
            //routes.MapRoute("default", "groups");
            //routes.MapRoute("default", "groups/getGroup/5");
            //    routes.MapRoute(
            //        name: "Default",
            //        template: "groups/{action}}",
            //        defaults: new { action = "Index" }
            //);
            //});
        }
    }
}
