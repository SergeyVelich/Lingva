using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SenderService.SettingsProvider.Core.Contracts;
using SenderService.SettingsProvider.Core.Providers;
using SenderService.SettingsProvider.EF.Context;
using SenderService.SettingsProvider.EF.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace SenderService.SettingsProvider.EF.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceExtensions
    {
        public static void ConfigureEmailSenderEF(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SenderDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
                //options.UseLazyLoadingProxies();
            });

            services.AddScoped<IRepository, Repository>();           
            services.AddScoped<IEmailSettingsProvider, EmailSettingsProvider>();
            services.AddScoped<IEmailTemplateProvider, EmailTemplateProvider>();
        }
    }
}