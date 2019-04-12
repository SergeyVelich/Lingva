using Lingva.Common.Extensions;
using Lingva.DAL.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Lingva.DAL.Dapper
{
    public class DapperContext : IDisposable
    {
        private readonly IDbConnection _dbConnection;

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

        public DapperSet<T> Set<T>() where T : BaseBE, new()
        {
            return new DapperSet<T>(this);
        }
    }
}
