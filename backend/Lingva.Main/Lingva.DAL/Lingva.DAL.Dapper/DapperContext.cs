﻿using Lingva.DAL.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Lingva.DAL.Dapper
{
    public class DapperContext : IDisposable
    {
        private Dictionary<Type, object> sets;
        protected bool disposed = false;

        public IDbConnection Connection { get; }

        public DapperContext(IConfiguration config)
        {
            string connectionStringValue = config.GetConnectionString("LingvaDapperConnection");

            var conn = new SqlConnection(connectionStringValue);
            conn.Open();
            Connection = conn;

            sets = new Dictionary<Type, object>();
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Connection.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public DapperSet<T> Set<T>() where T : BaseBE, new()
        {
            DapperSet<T> set = null;

            Type type = typeof(T);
            if(sets.TryGetValue(type, out object setObject) && setObject != null)
            {
                if(setObject is DapperSet<T>)
                {
                    set = (DapperSet<T>)setObject;
                }              
            }
            else
            {
                string collectionName = GetTableName<T>();
                set = new DapperSet<T>(this, collectionName);
                sets.Add(type, set);
            }

            return set;
        }

        protected string GetTableName<T>()
        {
            Type type = typeof(T);
            return type.Name + "s";
        }
    }
}
