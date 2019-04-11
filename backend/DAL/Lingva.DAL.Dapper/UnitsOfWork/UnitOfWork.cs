using Lingva.DAL.Repositories;
using Lingva.DAL.UnitsOfWork;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Lingva.DAL.Dapper.UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly IConnectionFactory _connectionFactory;
        private readonly IDbTransaction _dbTransaction;
        protected readonly IDbConnection _dbConnection;
        protected static IRepository _repository;

        protected bool disposed = false;

        public IRepository Repository { get => _repository; }

        public IDbTransaction GetTransaction
        {
            get
            {
                return _dbTransaction;
            }
        }

        public UnitOfWork(IConnectionFactory connectionFactory, IRepository repository)
        {
            _connectionFactory = connectionFactory;
            _dbConnection = connectionFactory.GetConnection;
            _dbTransaction = connectionFactory.GetTransaction;
            _repository = repository;
        }

        public virtual async Task SaveAsync()
        {
            try
            {
                _dbTransaction.Commit();
            }
            catch
            {
                _dbTransaction.Rollback();
                throw;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbTransaction.Dispose();
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
    }
}
