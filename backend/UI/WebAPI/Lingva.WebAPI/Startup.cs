using Lingva.ASP;
using Lingva.ASP.Extensions;
using Lingva.ASP.Infrastructure.Binders;
using Lingva.WebAPI.Extensions;
using Lingva.WebAPI.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
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
            services.ConfigureOptions(Configuration);                      
            services.ConfigureAuthentication();
            services.ConfigureAutoMapper();
            services.ConfigureSwagger();

            services.ConfigureDbProvider(Configuration);
            services.ConfigureManagers();
            services.ConfigureDataAdapters();

            services.AddMvc(options =>
            {
                options.ModelBinderProviders.Insert(0, new OptionsModelBinderProvider());
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseMiddleware<ExceptionHandlerMiddleware>();
            }           

            var options = new RewriteOptions()
                .AddRedirect("(.*)/$", "$1")
                .AddRedirect("[h,H]ome[/]?$", "home/index"); 
            app.UseRewriter(options);

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseCors("CorsPolicy");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lingva API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();

            DbInitializer.Initialize(Configuration);
        }
    }
}
