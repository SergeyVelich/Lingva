using Lingva.DAL.Entities;
using Lingva.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lingva.DAL.Dapper.Repositories
{
    public class Repository : IRepository, ITransactionProvider 
    {
        protected readonly DapperContext _dbContext;
        protected IDbTransaction _dbTransaction;

        protected bool disposed = false;

        public Repository(DapperContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<IEnumerable<T>> GetListAsync<T>() where T : BaseBE, new()
        {
            return await _dbContext.Set<T>().SelectAllAsync();
        }

        public virtual async Task<IEnumerable<T>> GetListAsync<T>(Expression<Func<T, bool>> predicator = null, IEnumerable<string> sorters = null, ICollection<Expression<Func<T, bool>>> includers = null, int skip = 0, int take = 0) where T : BaseBE, new()
        {
            //IQueryable<T> result = _dbContext.Set<T>().AsNoTracking();

            //if (predicator != null)
            //{
            //    result = result.Where(predicator);
            //}

            //if (includers != null)
            //{
            //    foreach (var includer in includers)
            //    {
            //        result = result.Include(includer);
            //    }
            //}

            //if (sorters != null)
            //{
            //    result = result.OrderBy(sorters);
            //}

            //if (skip != 0)
            //{
            //    result = result.Skip(skip);
            //}

            //if (take != 0)
            //{
            //    result = result.Take(take);
            //}

            //return await result.ToListAsync();
            return await _dbContext.Set<T>().SelectAllAsync();//??
        }

        public virtual async Task<T> GetByIdAsync<T>(int id) where T : BaseBE, new()
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> GetAsync<T>(Expression<Func<T, bool>> predicator) where T : BaseBE, new()//??
        {
            throw new NotImplementedException();
        }

        public virtual async Task<T> CreateAsync<T>(T entity) where T : BaseBE, new()
        {
            entity.CreateDate = DateTime.Now;
            entity.ModifyDate = DateTime.Now;
            await _dbContext.Set<T>().AddAsync(entity, _dbTransaction);

            return entity;
        }

        public virtual async Task<T> UpdateAsync<T>(T entity) where T : BaseBE, new()
        {
            entity.ModifyDate = DateTime.Now;
            await _dbContext.Set<T>().UpdateAsync(entity, _dbTransaction);

            return entity;
        }

        public virtual async Task DeleteAsync<T>(T entity) where T : BaseBE, new()
        {
            await _dbContext.Set<T>().RemoveAsync(entity, _dbTransaction);
        }

        public IDbTransaction BeginTransaction()
        {
            _dbTransaction = _dbContext.Connection.BeginTransaction();
            return _dbTransaction;
        }

        public void EndTransaction()
        {
            try
            {
                if(_dbTransaction != null)
                {
                    _dbTransaction.Commit();
                }               
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
    }
}
