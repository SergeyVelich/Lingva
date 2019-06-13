using Lingva.DAL.EF.Context;
using Lingva.DAL.Entities;
using Lingva.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using QueryBuilder.EF.Extensions;
using QueryBuilder.QueryOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingva.DAL.EF.Repositories
{
    public class Repository : IRepository, IDisposable, ITransactionProvider
    {
        protected readonly DictionaryContext _dbContext;
        protected IDbContextTransaction _dbTransaction;

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

        public virtual async Task<T> CreateAsync<T>(T entity) where T : BaseBE, new()
        {
            entity.CreateDate = DateTime.Now;
            entity.ModifyDate = DateTime.Now;

            await _dbContext.Set<T>().AddAsync(entity);
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

        public virtual async Task DeleteAsync<T>(int id) where T : BaseBE, new()
        {
            T entity = new T
            {
                Id = id
            };
            _dbContext.Set<T>().Attach(entity);

            _dbContext.Set<T>().Remove(entity);

            await _dbContext.SaveChangesAsync(true);
        }

        public void StartTransaction()
        {
            _dbTransaction = _dbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _dbTransaction.Commit();
        }

        public void AbortTransaction()
        {
            _dbTransaction.Rollback();
        }

        public void EndTransaction()
        {
            try
            {
                CommitTransaction();
            }
            catch
            {
                AbortTransaction();
                throw;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        public virtual async Task<IEnumerable<T>> GetListAsync<T>(IQueryOptions queryOptions) where T : BaseBE, new()
        {
            IQueryable<T> result = _dbContext.Set<T>().AsNoTracking();

            result = result.Where(queryOptions.Filters);

            result = result.Include(queryOptions.Includers);

            result = result.OrderBy(queryOptions.Sorters);

            int skip = queryOptions.Pagenator.Skip;
            if (skip != 0)
            {
                result = result.Skip(skip);
            }

            int take = queryOptions.Pagenator.Take;
            if (take != 0)
            {
                result = result.Take(take);
            }

            return await result.ToListAsync();
        }

        public virtual async Task<int> CountAsync<T>(IQueryOptions queryOptions) where T : BaseBE, new()
        {
            return await _dbContext.Set<T>().Where(queryOptions.Filters).CountAsync();
        }
    }
}
