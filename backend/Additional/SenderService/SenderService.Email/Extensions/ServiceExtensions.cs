using Microsoft.Extensions.DependencyInjection;
using SenderService.Email.Contracts;
using System.Diagnostics.CodeAnalysis;

namespace SenderService.Email.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceExtensions
    {
        public static void ConfigureEmailSender(this IServiceCollection services)
        {
            services.AddScoped<IEmailSender, EmailSender>();
        }
    }
}