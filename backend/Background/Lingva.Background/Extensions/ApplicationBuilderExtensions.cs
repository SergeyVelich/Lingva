using Microsoft.AspNetCore.Builder;
using System;

namespace Lingva.Background
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseQuartz(this IApplicationBuilder app, Action<Quartz> configuration)
        {
            var jobFactory = new QuartzJobFactory(app.ApplicationServices);
            Quartz.Instance.UseJobFactory(jobFactory);

            configuration.Invoke(Quartz.Instance);
            Quartz.Start();
        }
    }
}
