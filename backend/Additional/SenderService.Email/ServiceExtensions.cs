using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.WebAPI.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceExtensions
    {
        //public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
        //{
        //    string configStringValue = config.GetConnectionString("LingvaConnection");
        //    string configVariableName = configStringValue.GetVariableName();
        //    string connectionStringValue = Environment.GetEnvironmentVariable(configVariableName);

        //    services.AddDbContext<DictionaryContext>(options =>
        //        options.UseSqlServer(connectionStringValue));
        //}       
    }
}