using Lingva.DAL.EF.Context;
using Lingva.DAL.Entities;
using Lingva.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using QueryBuilder.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lingva.DAL.EF.Repositories
{
    public class Repository : IRepository 
    {
        protected readonly DictionaryContext _dbContext;

        public Repository(DictionaryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<IEnumerable<T>> GetListAsync<T>(Expression<Func<T, bool>> predicator = null, IEnumerable<string> sorters = null, ICollection<Expression<Func<T, bool>>> includers = null, int skip = 0, int take = 0) where T : BaseBE, new()
        {
            IQueryable<T> result = _dbContext.Set<T>().AsNoTracking();

            if (predicator != null)
            {
                result = result.Where(predicator);
            }

            if (includers != null)
            {
                foreach(var includer in includers)
                {
                    result = result.Include(includer);
                }               
            }

            if (sorters != null)
            {
                result = result.OrderBy(sorters);
            }

            if (skip != 0)
            {
                result = result.Skip(skip);
            }

            if (take != 0)
            {
                result = result.Take(take);
            }

            return await result.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync<T>(int id) where T : BaseBE, new()
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> GetAsync<T>(Expression<Func<T, bool>> predicator) where T : BaseBE, new()
        {
            return await _dbContext.Set<T>().Where(predicator).FirstOrDefaultAsync();
        }

        public virtual T Create<T>(T entity) where T : BaseBE, new()
        {
            _dbContext.Set<T>().Add(entity);

            return entity;
        }

        public virtual T Update<T>(T entity) where T : BaseBE, new()
        {
            //_dbContext.Set<T>().Attach(entity);
            _dbContext.Set<T>().Update(entity);

            return entity;
        }

        public virtual void Delete<T>(T entity) where T : BaseBE, new()
        {
            _dbContext.Set<T>().Remove(entity);
        }
    }
}
