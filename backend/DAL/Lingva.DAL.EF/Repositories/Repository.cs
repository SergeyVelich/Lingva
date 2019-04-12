using Lingva.DAL.EF.Context;
using Lingva.DAL.Entities;
using Lingva.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using QueryBuilder.Extensions;
using QueryBuilder.QueryOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lingva.DAL.EF.Repositories
{
    public class Repository : IRepository, IDisposable
    {
        protected readonly DictionaryContext _dbContext;

        protected bool disposed = false;

        public Repository(DictionaryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<IEnumerable<T>> GetListAsync<T>() where T : BaseBE, new()
        {
            IQueryable<T> result = _dbContext.Set<T>().AsNoTracking();

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

        public virtual async Task<T> CreateAsync<T>(T entity) where T : BaseBE, new()
        {
            entity.CreateDate = DateTime.Now;
            entity.ModifyDate = DateTime.Now;
            await _dbContext.Set<T>().AddAsync(entity);
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync(true);

            return entity;
        }

        public virtual async Task<T> UpdateAsync<T>(T entity) where T : BaseBE, new()
        {
            entity.ModifyDate = DateTime.Now;
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync(true);

            return entity;
        }

        public virtual async Task DeleteAsync<T>(T entity) where T : BaseBE, new()
        {
            entity.ModifyDate = DateTime.Now;
            _dbContext.Set<T>().Remove(entity);

            await _dbContext.SaveChangesAsync(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }



        public virtual async Task<IEnumerable<T>> GetListAsync<T>(QueryOptions queryOptions) where T : BaseBE, new()
        {
            Expression<Func<T, bool>> filters = queryOptions.GetFiltersExpression<T>();
            IEnumerable<string> sorters = queryOptions.GetSortersCollection<T>();
            ICollection<Expression<Func<T, bool>>> includers = null;
            //ICollection<Expression<Func<Group, bool>>> includers = optionsDTO.GetIncludersCollection<T>();//??
            QueryPagenator pagenator = queryOptions.Pagenator;
            int skip = pagenator.Skip;
            int take = pagenator.Take; IQueryable<T> result = _dbContext.Set<T>().AsNoTracking();

            if (filters != null)
            {
                result = result.Where(filters);
            }

            if (includers != null)
            {
                foreach (var includer in includers)
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
    }
}
