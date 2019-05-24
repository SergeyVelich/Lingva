﻿using Lingva.DAL.EF.Context;
using Lingva.DAL.Mongo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Lingva.ASP
{
    public static class DbInitializer
    {
        public static void Initialize(IConfiguration config)
        {
            DbProviders dbProvider = (DbProviders)Enum.Parse(typeof(DbProviders), config.GetSection("Selectors:DbProvider").Value, true);

            switch (dbProvider)
            {
                case DbProviders.Dapper:
                    DictionaryContextFactory factory = new DictionaryContextFactory();
                    DictionaryContext dbContext = factory.CreateDbContext(new string[0]);
                    dbContext.Database.Migrate();
                    break;
                case DbProviders.Mongo:
                    new MongoContext(config).Initialize();
                    break;
                default:
                    factory = new DictionaryContextFactory();
                    dbContext = factory.CreateDbContext(new string[0]);
                    dbContext.Database.Migrate();
                    break;
            }
        }
    }
}