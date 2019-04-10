using Lingva.Common.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Lingva.DAL.Dapper
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString;
        private IDbConnection _dbConnection;
        private IDbTransaction _dbTransaction;

        public ConnectionFactory(IConfiguration config)
        {
            string configStringValue = config.GetConnectionString("LingvaConnection");
            string configVariableName = configStringValue.GetVariableName();
            _connectionString = Environment.GetEnvironmentVariable(configVariableName);
        }

        public IDbConnection GetConnection
        {
            get
            {
                if (_dbConnection == null || _dbConnection.State != ConnectionState.Open)
                {
                    var conn = new SqlConnection(_connectionString);
                    conn.Open();
                    _dbConnection = conn;
                }

                return _dbConnection;
            }
        }

        public IDbTransaction GetTransaction
        {
            get
            {
                if(_dbTransaction == null || _dbTransaction.Connection == null)
                {
                    if (_dbConnection == null || _dbConnection.State != ConnectionState.Open)
                    {
                        var conn = new SqlConnection(_connectionString);
                        conn.Open();
                        _dbConnection = conn;
                    }

                    _dbTransaction = _dbConnection.BeginTransaction();
                }

                return _dbTransaction;
            }
        }
    }
}
