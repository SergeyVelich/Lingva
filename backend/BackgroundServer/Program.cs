using Lingva.Background.Jobs;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Lingva.Background
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            EmailScheduler.Start();
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
