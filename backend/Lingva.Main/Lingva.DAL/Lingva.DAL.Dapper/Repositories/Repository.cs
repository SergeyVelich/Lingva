using Lingva.DAL.Entities;
using Lingva.DAL.Repositories;
using QueryBuilder.QueryOptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Lingva.DAL.Dapper.Repositories
{
    public class Repository : IRepository, IDisposable, ITransactionProvider 
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

        public virtual async Task<T> GetByIdAsync<T>(int id) where T : BaseBE, new()
        {
            return await _dbContext.Set<T>().FindAsync(id);
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

        public virtual async Task DeleteAsync<T>(int id) where T : BaseBE, new()
        {
            await _dbContext.Set<T>().RemoveAsync(id, _dbTransaction);
        }

        public void StartTransaction()
        {
            _dbTransaction = _dbContext.Connection.BeginTransaction();
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
                    _dbTransaction.Dispose();
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
            //Expression<Func<T, bool>> filters = queryOptions.GetFiltersExpression<T>();
            //IEnumerable<string> sorters = queryOptions.GetSortersCollection<T>();
            //ICollection<Expression<Func<T, bool>>> includers = null;
            ////ICollection<Expression<Func<Group, bool>>> includers = optionsDTO.GetIncludersCollection<T>();//??
            //QueryPagenator pagenator = queryOptions.Pagenator;
            //int skip = pagenator.Skip;
            //int take = pagenator.Take; IQueryable<T> result = _dbContext.Set<T>().AsNoTracking();

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

        public Task<int> CountAsync<T>(IQueryOptions queryOptions) where T : BaseBE, new()
        {
            throw new NotImplementedException();
        }
    }
}
