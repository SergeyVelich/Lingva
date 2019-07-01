using Lingva.DAL.Entities;
using Lingva.DAL.Repositories;
using QueryBuilder.QueryOptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.DAL.CosmosSqlApi.Repositories
{
    public class Repository : IRepository, IDisposable, ITransactionProvider
    {
        protected readonly CosmosSqlApiContext _dbContext;

        protected bool disposed = false;

        public Repository(CosmosSqlApiContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<IEnumerable<T>> GetListAsync<T>() where T : BaseBE, new()
        {
            return await _dbContext.Set<T>().SelectAllAsync();
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

            return entity;
        }

        public virtual async Task<T> UpdateAsync<T>(T entity) where T : BaseBE, new()
        {
            await _dbContext.Set<T>().UpdateAsync(entity);

            return entity;
        }

        public virtual async Task DeleteAsync<T>(int id) where T : BaseBE, new()
        {
            await _dbContext.Set<T>().RemoveAsync(id);
        }

        public void StartTransaction()
        {

        }

        public void CommitTransaction()
        {

        }

        public void AbortTransaction()
        {
        
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
                    AbortTransaction();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<T>> GetListAsync<T>(IQueryOptions queryOptions) where T : BaseBE, new()
        {
            //var result = _dbContext.Set<T>()
            //    .Find(queryOptions.Filters)
            //    .SortBy(queryOptions.Sorters)
            //    .Skip(queryOptions.Pagenator.Skip)
            //    .Limit(queryOptions.Pagenator.Take);

            //return await result.ToListAsync();

            return null;
        }

        public virtual async Task<int> CountAsync<T>(IQueryOptions queryOptions) where T : BaseBE, new()
        {
            //var result = _dbContext.Set<T>()
            //    .Find(queryOptions.Filters);

            //return (int)await result.CountDocumentsAsync();

            return 0;
        }
    }
}
