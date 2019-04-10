using Lingva.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lingva.DAL.Dapper.Repositories
{
    public abstract class Repository<T> : IRepository<T> 
        where T : class
    {
        protected readonly IDbConnection _dbConnection;
        protected readonly IDbTransaction _dbTransaction;

        public Repository(IConnectionFactory connectionFactory)
        {
            _dbConnection = connectionFactory.GetConnection;
            _dbTransaction = connectionFactory.GetTransaction;
        }

        public abstract IEnumerable<T> GetList(Expression<Func<T, bool>> predicator = null, IEnumerable<string> sorters = null, IEnumerable<Expression<Func<T, bool>>> includers = null, int skip = 0, int take = 0);

        public abstract Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicator = null, IEnumerable<string> sorters = null, ICollection<Expression<Func<T, bool>>> includers = null, int skip = 0, int take = 0);

        public abstract T GetById(int id);

        public abstract Task<T> GetByIdAsync(int id);

        public abstract T Get(Expression<Func<T, bool>> predicator);

        public abstract Task<T> GetAsync(Expression<Func<T, bool>> predicator);

        public abstract T Create(T entity);

        public abstract T Update(T entity);

        public abstract void Delete(T entity);
    }
}
