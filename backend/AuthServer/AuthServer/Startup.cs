using AuthServer.Identity;
using AuthServer.Identity.Entities;
using Lingva.Common.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

namespace AuthServer
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
            string configStringValue = Configuration.GetConnectionString("AccountConnection");
            string configVariableName = configStringValue.GetVariableName();
            string connectionStringValue = Environment.GetEnvironmentVariable(configVariableName);

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(connectionStringValue));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                        .AddDeveloperSigningCredential(filename: "tempkey.rsa")
                        .AddInMemoryApiResources(Config.GetApiResources())
                        .AddInMemoryIdentityResources(Config.GetIdentityResources())
                        .AddInMemoryClients(Config.GetClients())
                        .AddAspNetIdentity<ApplicationUser>();

            services.AddMvc();



            //services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));

            //services.AddIdentity<AppUser, IdentityRole>()
            //  .AddEntityFrameworkStores<AppIdentityDbContext>()
            //  .AddDefaultTokenProviders();

            //services.AddIdentityServer().AddDeveloperSigningCredential()
               // this adds the operational data from DB (codes, tokens, consents)
      //         .AddOperationalStore(options =>
      //         {
      //             options.ConfigureDbContext = builder => builder.UseSqlServer(Configuration.GetConnectionString("Default"));
      //             // this enables automatic token cleanup. this is optional.
      //             options.EnableTokenCleanup = true;
      //             options.TokenCleanupInterval = 30; // interval in seconds
      //})
               //.AddInMemoryIdentityResources(Config.GetIdentityResources())
               //.AddInMemoryApiResources(Config.GetApiResources())
               //.AddInMemoryClients(Config.GetClients())
               //.AddAspNetIdentity<AppUser>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseIdentityServer();            
            app.UseMvcWithDefaultRoute();
        }
    }
}
