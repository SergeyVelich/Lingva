using Microsoft.EntityFrameworkCore;
using SenderService.Email.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenderService.Email.EF.Repositories
{
    public class EmailEFRepository : IEmailSenderRepository, IDisposable
    {
        protected readonly DbContext _dbContext;

        protected bool disposed = false;

        public EmailEFRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<IEnumerable<T>> GetListAsync<T>() where T : class, new()
        {
            IQueryable<T> result = _dbContext.Set<T>().AsNoTracking();

            return await result.ToListAsync();
        }      

        public virtual async Task<T> GetByIdAsync<T>(int id) where T : class, new()
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> CreateAsync<T>(T entity) where T : class, new()
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync(true);

            return entity;
        }

        public virtual async Task<T> UpdateAsync<T>(T entity) where T : class, new()
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync(true);

            return entity;
        }

        public virtual async Task DeleteAsync<T>(T entity) where T : class, new()
        {
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
    }
}
