using Dapper;
using Lingva.DAL.Entities;
using Lingva.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lingva.DAL.Dapper.Repositories
{
    public class RepositoryGroup : Repository<Group>, IRepositoryGroup
    {
        public RepositoryGroup(IConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }

        public override IEnumerable<Group> GetList(Expression<Func<Group, bool>> predicator = null, IEnumerable<string> sorters = null, IEnumerable<Expression<Func<Group, bool>>> includers = null, int skip = 0, int take = 0)
        {
            StringBuilder queryStringBuilder = new StringBuilder();
            queryStringBuilder.AppendLine("SELECT g.Id, g.Date, g.Description, g.LanguageId, g.Name, g.Picture");
            queryStringBuilder.AppendLine("FROM Groups AS g");
            IEnumerable<Group> result = _dbConnection.Query<Group>(queryStringBuilder.ToString(), transaction: _dbTransaction);

            return result;
        }

        public override async Task<IEnumerable<Group>> GetListAsync(Expression<Func<Group, bool>> predicator = null, IEnumerable<string> sorters = null, ICollection<Expression<Func<Group, bool>>> includers = null, int skip = 0, int take = 0)
        {
            StringBuilder queryStringBuilder = new StringBuilder();
            queryStringBuilder.AppendLine("SELECT g.Id, g.Date, g.Description, g.LanguageId, g.Name, g.Picture");
            queryStringBuilder.AppendLine("FROM Groups AS g");
            IEnumerable<Group> result = await _dbConnection.QueryAsync<Group>(queryStringBuilder.ToString(), transaction: _dbTransaction);

            return result;
        }

        public override Group GetById(int id)
        {
            StringBuilder queryStringBuilder = new StringBuilder();
            queryStringBuilder.AppendLine("SELECT g.Id, g.Date, g.Description, g.LanguageId, g.Name, g.Picture");
            queryStringBuilder.AppendLine("FROM Groups AS g");
            queryStringBuilder.AppendLine("WHERE g.Id = @Id");
            Group result = _dbConnection.QueryFirstOrDefault<Group>(queryStringBuilder.ToString(), new { Id = id }, transaction: _dbTransaction);

            return result;
        }

        public override async Task<Group> GetByIdAsync(int id)
        {
            StringBuilder queryStringBuilder = new StringBuilder();
            queryStringBuilder.AppendLine("SELECT g.Id, g.Date, g.Description, g.LanguageId, g.Name, g.Picture");
            queryStringBuilder.AppendLine("FROM Groups AS g");
            queryStringBuilder.AppendLine("WHERE g.Id = @Id");
            Group result = await _dbConnection.QueryFirstOrDefaultAsync<Group>(queryStringBuilder.ToString(), new { Id = id }, transaction: _dbTransaction);

            return result;
        }

        public override Group Get(Expression<Func<Group, bool>> predicator)
        {
            throw new NotImplementedException();
        }

        public override async Task<Group> GetAsync(Expression<Func<Group, bool>> predicator)
        {
            throw new NotImplementedException();
        }

        public override Group Create(Group entity)
        {
            StringBuilder queryStringBuilder = new StringBuilder();
            queryStringBuilder.AppendLine("");
            Group result = _dbConnection.QueryFirstOrDefault<Group>(queryStringBuilder.ToString(), entity, transaction: _dbTransaction);

            return result;
        }

        public override Group Update(Group entity)
        {
            StringBuilder queryStringBuilder = new StringBuilder();
            queryStringBuilder.AppendLine("");
            Group result = _dbConnection.QueryFirstOrDefault<Group>(queryStringBuilder.ToString(), new { entity }, transaction: _dbTransaction);

            return result;
        }

        public override void Delete(Group entity)
        {
            StringBuilder queryStringBuilder = new StringBuilder();
            queryStringBuilder.AppendLine("");
            _dbConnection.Execute(queryStringBuilder.ToString(), new { entity.Id }, transaction: _dbTransaction);
        }
    }
}
