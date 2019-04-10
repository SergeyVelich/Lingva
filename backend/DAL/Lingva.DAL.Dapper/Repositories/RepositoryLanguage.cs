using Lingva.DAL.Entities;
using Lingva.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lingva.DAL.Dapper.Repositories
{
    public class RepositoryLanguage : Repository<Language>, IRepositoryLanguage
    {
        public RepositoryLanguage(IConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }

        public override IEnumerable<Language> GetList(Expression<Func<Language, bool>> predicator = null, IEnumerable<string> sorters = null, IEnumerable<Expression<Func<Language, bool>>> includers = null, int skip = 0, int take = 0)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<Language>> GetListAsync(Expression<Func<Language, bool>> predicator = null, IEnumerable<string> sorters = null, ICollection<Expression<Func<Language, bool>>> includers = null, int skip = 0, int take = 0)
        {
            throw new NotImplementedException();
        }

        public override Language GetById(int id)
        {
            throw new NotImplementedException();
        }

        public override async Task<Language> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public override Language Get(Expression<Func<Language, bool>> predicator)
        {
            throw new NotImplementedException();
        }

        public override async Task<Language> GetAsync(Expression<Func<Language, bool>> predicator)
        {
            throw new NotImplementedException();
        }

        public override Language Create(Language entity)
        {
            throw new NotImplementedException();
        }

        public override Language Update(Language entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Language entity)
        {
            throw new NotImplementedException();
        }
    }
}
