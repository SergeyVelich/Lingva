using Dapper;
using Lingva.DAL.Entities;
using Lingva.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lingva.DAL.Dapper.Repositories
{
    public class Repository : IRepository//?? 
    {
        protected readonly IDbConnection _dbConnection;
        protected readonly IDbTransaction _dbTransaction;

        public Repository(IConnectionFactory connectionFactory)
        {
            _dbConnection = connectionFactory.GetConnection;
            _dbTransaction = connectionFactory.GetTransaction;
        }

        public virtual async Task<IEnumerable<T>> GetListAsync<T>(Expression<Func<T, bool>> predicator = null, IEnumerable<string> sorters = null, ICollection<Expression<Func<T, bool>>> includers = null, int skip = 0, int take = 0) where T : BaseBE, new()
        {
            StringBuilder queryStringBuilder = new StringBuilder();
            queryStringBuilder.AppendLine("SELECT g.Id, g.Date, g.Description, g.LanguageId, g.Name, g.Picture");
            queryStringBuilder.AppendLine("FROM Groups AS g");
            IEnumerable<T> result = await _dbConnection.QueryAsync<T>(queryStringBuilder.ToString(), transaction: _dbTransaction);

            return result;
        }

        public virtual async Task<T> GetByIdAsync<T>(int id) where T : BaseBE, new()
        {
            StringBuilder queryStringBuilder = new StringBuilder();
            queryStringBuilder.AppendLine("SELECT g.Id, g.Date, g.Description, g.LanguageId, g.Name, g.Picture");
            queryStringBuilder.AppendLine("FROM Groups AS g");
            queryStringBuilder.AppendLine("WHERE g.Id = @Id");
            T result = await _dbConnection.QueryFirstOrDefaultAsync<T>(queryStringBuilder.ToString(), new { Id = id }, transaction: _dbTransaction);

            return result;
        }

        public virtual async Task<T> GetAsync<T>(Expression<Func<T, bool>> predicator) where T : BaseBE, new()//??
        {
            throw new NotImplementedException();
        }

        public virtual T Create<T>(T entity) where T : BaseBE, new()
        {
            StringBuilder queryStringBuilder = new StringBuilder();
            queryStringBuilder.AppendLine("");
            T result = _dbConnection.QueryFirstOrDefault<T>(queryStringBuilder.ToString(), entity, transaction: _dbTransaction);

            return result;
        }

        public virtual T Update<T>(T entity) where T : BaseBE, new()
        {
            StringBuilder queryStringBuilder = new StringBuilder();
            queryStringBuilder.AppendLine("");
            T result = _dbConnection.QueryFirstOrDefault<T>(queryStringBuilder.ToString(), new { entity }, transaction: _dbTransaction);

            return result;
        }

        public virtual void Delete<T>(T entity) where T : BaseBE, new()
        {
            StringBuilder queryStringBuilder = new StringBuilder();
            queryStringBuilder.AppendLine("");
            _dbConnection.Execute(queryStringBuilder.ToString(), new { entity.Id }, transaction: _dbTransaction);
        }
    }
}
