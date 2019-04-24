using Lingva.BC.Contracts;
using Lingva.BC.Services;
using Lingva.DAL.EF.Context;
using Lingva.WebAPI.Extensions;
using Lingva.WebAPI.Infrastructure;
using Lingva.WebAPI.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
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
            services.ConfigureEF(Configuration);
            //services.ConfigureDapper(Configuration);
            services.ConfigureOptions(Configuration);
            services.ConfigureAuthentication();
            services.ConfigureAutoMapper();
            services.ConfigureSwagger();
           
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IInfoService, InfoService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<QueryOptionsAdapter>();

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
            else
            {
                app.UseMiddleware<ExceptionHandlerMiddleware>();
            }           

            var options = new RewriteOptions()
                .AddRedirect("(.*)/$", "$1")
                .AddRedirect("[h,H]ome[/]?$", "home/index"); 
            app.UseRewriter(options);

            app.UseCors("CorsPolicy");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lingva API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}
