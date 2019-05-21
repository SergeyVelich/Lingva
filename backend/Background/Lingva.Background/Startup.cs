using Lingva.BC.Contracts;
using Lingva.BC.Services;
using Lingva.DAL.EF.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SenderService.Email.EF.Extensions;

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
            services.ConfigureEF(Configuration);
            services.ConfigureEmailSenderEF<DictionaryContext>();
            services.ConfigureQuartzSheduler();

            services.AddScoped<IGroupManager, GroupManager>();
            services.AddScoped<IUserManager, UserManager>();

            services.ConfigureAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseQuartz((quartz) => quartz.AddJob<EmailJob>("EmailJob", "Email", 60));
        }
    }
}
