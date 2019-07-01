using Lingva.BC.Contracts;
using Lingva.BC.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SenderService.Email.Extensions;
using SenderService.SettingsProvider.EF.Extensions;

namespace Lingva.Background
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionStringValue = Configuration.GetConnectionString("LingvaEFConnection");

            services.ConfigureEF(Configuration); //for getiing info about events and users
            services.ConfigureEmailSender(); //for working with sender library
            services.ConfigureEmailSenderEF(connectionStringValue); //for working with sender db
            services.ConfigureQuartzSheduler();

            services.AddScoped<IGroupManager, GroupManager>();
            services.AddScoped<IUserManager, UserManager>();

            services.ConfigureAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseQuartz((quartz) => quartz.AddJob<EmailJob>("EmailJob", "Email", 60));

            DbInitializer.InitializeAsync(Configuration).Wait();
        }
    }
}
