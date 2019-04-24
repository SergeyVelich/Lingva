using Microsoft.Extensions.DependencyInjection;
using SenderService.Email.Contracts;
using SenderService.Email.EF.Contracts;
using SenderService.Email.EF.Providers;
using System.Diagnostics.CodeAnalysis;

namespace SenderService.Email.EF.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceExtensions
    {
        public static void ConfigureEmailSenderIO(this IServiceCollection services)
        {
            services.AddScoped<IIOEmailSender, IOEmailSender>();
            services.AddScoped<IIOSendingOptionsProvider, IOSendingOptionsProvider>();
            services.AddScoped<IIOTemplateProvider, IOTemplateProvider>();
        }
    }
}