using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SenderService.Email.Contracts;
using SenderService.Email.EF.Contracts;
using SenderService.Email.EF.DAL.Contracts;
using SenderService.Email.EF.DAL.Repositories;
using SenderService.Email.EF.Providers;
using System.Diagnostics.CodeAnalysis;

namespace SenderService.Email.EF.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceExtensions
    {
        public static void ConfigureEmailSenderEF<T>(this IServiceCollection services) where T : DbContext
        {
            services.AddScoped<IEFEmailSender, EFEmailSender>();
            services.AddScoped<IEmailSenderRepository, EmailEFRepository>();
            services.AddScoped<DbContext, T>();
            services.AddScoped<IEFSendingOptionsProvider, EFSendingOptionsProvider>();
            services.AddScoped<IEFTemplateProvider, EFTemplateProvider>();
        }
    }
}