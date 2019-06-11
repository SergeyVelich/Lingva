using Lingva.DAL.Entities;
using Lingva.DAL.Repositories;
using MongoDB.Driver;
using QueryBuilder.Mongo.Extensions;
using QueryBuilder.QueryOptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.DAL.Mongo.Repositories
{
    public class Repository : IRepository, IDisposable, ITransactionProvider
    {
        protected readonly MongoContext _dbContext;

        protected bool disposed = false;

        public Repository(MongoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<IEnumerable<T>> GetListAsync<T>() where T : BaseBE, new()
        {
            var documents = await _dbContext.Set<T>().Find(_ => true).ToListAsync();
            return documents;
        }

        public virtual async Task<T> GetByIdAsync<T>(int id) where T : BaseBE, new()
        {
            return await _dbContext.Set<T>().Find(note => note.Id == id).FirstOrDefaultAsync();
        }

        public virtual async Task<T> CreateAsync<T>(T entity) where T : BaseBE, new()
        {
            entity.CreateDate = DateTime.Now;
            entity.ModifyDate = DateTime.Now;

            await _dbContext.Set<T>().InsertOneAsync(entity);

            return entity;
        }

        public virtual async Task<T> UpdateAsync<T>(T entity) where T : BaseBE, new()
        {
            var filter = Builders<T>.Filter.Eq("_id", entity.Id);
            await _dbContext.Set<T>().ReplaceOneAsync(filter, entity);

            return entity;
        }

        public virtual async Task DeleteAsync<T>(int id) where T : BaseBE, new()
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            await _dbContext.Set<T>().DeleteOneAsync(filter);
        }

        public void StartTransaction()
        {
            _dbContext.Session.StartTransaction();
        }

        public void CommitTransaction()
        {
            if (_dbContext.Session.IsInTransaction)
            {
                _dbContext.Session.CommitTransaction();
            }
        }

        public void AbortTransaction()
        {
            if (_dbContext.Session.IsInTransaction)
            {
                _dbContext.Session.AbortTransaction();
            }           
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

        public virtual async Task<IEnumerable<T>> GetListAsync<T>(IQueryOptions queryOptions) where T : BaseBE, new()
        {
            var result = _dbContext.Set<T>()
                .Find(queryOptions.Filters)
                .SortBy(queryOptions.Sorters)
                .Skip(queryOptions.Pagenator.Skip)
                .Limit(queryOptions.Pagenator.Take);

            return await result.ToListAsync();
        }

        public virtual async Task<int> CountAsync<T>(IQueryOptions queryOptions) where T : BaseBE, new()
        {
            var result = _dbContext.Set<T>()
                .Find(queryOptions.Filters);

            return (int)await result.CountDocumentsAsync();
        }
    }
}
