using Lingva.Common.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Lingva.DAL.Dapper
{
    public class DapperContext : IDisposable
    {
        private readonly IDbConnection _dbConnection;
        private Dictionary<Type, object> sets;
        protected bool disposed = false;

        public IDbConnection Connection { get => _dbConnection; }

        public DapperContext(IConfiguration config)
        {
            string configStringValue = config.GetConnectionString("LingvaConnection");
            string configVariableName = configStringValue.GetVariableName();
            string connectionString = Environment.GetEnvironmentVariable(configVariableName);

            var conn = new SqlConnection(connectionString);
            conn.Open();
            _dbConnection = conn;

            sets = new Dictionary<Type, object>();
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbConnection.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public DapperSet<T> Set<T>() where T : class, new()
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
                set = new DapperSet<T>(this);
                sets.Add(type, set);
            }

            return set;
        }
    }
}
