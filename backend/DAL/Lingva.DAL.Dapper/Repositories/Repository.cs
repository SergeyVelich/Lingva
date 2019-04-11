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
            return await _dbContext.SelectAllAsync<T>();
        }

        public virtual async Task<IEnumerable<T>> GetListAsync<T>(Expression<Func<T, bool>> predicator = null, IEnumerable<string> sorters = null, ICollection<Expression<Func<T, bool>>> includers = null, int skip = 0, int take = 0) where T : BaseBE, new()
        {
            return await _dbContext.SelectAllAsync<T>();//??
        }

        public virtual async Task<T> GetByIdAsync<T>(int id) where T : BaseBE, new()
        {
            return await _dbContext.FindAsync<T>(id);
        }

        public virtual async Task<T> GetAsync<T>(Expression<Func<T, bool>> predicator) where T : BaseBE, new()//??
        {
            throw new NotImplementedException();
        }

        public virtual async Task<T> CreateAsync<T>(T entity) where T : BaseBE, new()
        {
            entity.CreateDate = DateTime.Now;
            entity.ModifyDate = DateTime.Now;
            await _dbContext.AddAsync(entity, _dbTransaction);
            EndTransaction();

            return entity;
        }

        public virtual async Task<T> UpdateAsync<T>(T entity) where T : BaseBE, new()
        {
            entity.ModifyDate = DateTime.Now;
            await _dbContext.UpdateAsync(entity, _dbTransaction);
            EndTransaction();

            return entity;
        }

        public virtual async Task DeleteAsync<T>(T entity) where T : BaseBE, new()
        {
            await _dbContext.RemoveAsync<T>(entity, _dbTransaction);
            EndTransaction();
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
