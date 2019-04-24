﻿using Lingva.Background.Mapper;
using Lingva.Common.Extensions;
using Lingva.Common.Mapping;
using Lingva.DAL.EF.Context;
using Lingva.DAL.EF.Repositories;
using Lingva.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz.Spi;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.Background
{
    [ExcludeFromCodeCoverage]
    public static class ServiceExtensions
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddScoped<IDataAdapter, DataAdapter>();
            services.AddSingleton(AppMapperConfig.GetMapper());
        }

        public static void ConfigureQuartzSheduler(this IServiceCollection services)
        {
            services.AddTransient<IJobFactory, QuartzJobFactory>((provider) => new QuartzJobFactory(services.BuildServiceProvider()));
            services.AddTransient<EmailJob>();
        }

        public static void ConfigureEF(this IServiceCollection services, IConfiguration config)
        {
            services.ConfigureSqlContext(config);
            services.ConfigureEFRepositories();
        }

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
        {
            string configStringValue = config.GetConnectionString("LingvaConnection");
            string configVariableName = configStringValue.GetVariableName();
            string connectionStringValue = Environment.GetEnvironmentVariable(configVariableName);

            services.AddDbContext<DictionaryContext>(options =>
                options.UseSqlServer(connectionStringValue), 
                ServiceLifetime.Transient);
        }

        public static void ConfigureEFRepositories(this IServiceCollection services)
        {
            services.AddTransient<IRepository, Repository>();
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}