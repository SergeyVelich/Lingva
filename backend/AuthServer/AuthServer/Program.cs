using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace AuthServer
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("serilog.config.json", optional: false, reloadOnChange: true)
            .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            Log.Information("Logger created");

            try
            {
                Log.Information("Starting web host");
                var host = CreateWebHostBuilder(args).Build();
                host.Run();
            }
            catch (Exception exception)
            {
                Log.Fatal(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://localhost:6050")
                .UseStartup<Startup>()
                .UseSerilog();
    }
}
