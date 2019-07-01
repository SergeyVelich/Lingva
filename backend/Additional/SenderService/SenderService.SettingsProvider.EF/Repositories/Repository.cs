using Microsoft.EntityFrameworkCore;
using SenderService.SettingsProvider.Core.Contracts;
using SenderService.SettingsProvider.EF.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenderService.SettingsProvider.EF.Repositories
{
    public class Repository : IRepository, IDisposable
    {
        protected readonly SenderDbContext _senderDbContext;

        protected bool disposed = false;

        public Repository(SenderDbContext senderDbContext)
        {
            _senderDbContext = senderDbContext;
        }

        public virtual async Task<IEnumerable<T>> GetListAsync<T>() where T : class, new()
        {
            IQueryable<T> result = _senderDbContext.Set<T>().AsNoTracking();

            return await result.ToListAsync();
        }      

        public virtual async Task<T> GetByIdAsync<T>(int id) where T : class, new()
        {
            return await _senderDbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> CreateAsync<T>(T entity) where T : class, new()
        {
            await _senderDbContext.Set<T>().AddAsync(entity);
            await _senderDbContext.SaveChangesAsync(true);

            return entity;
        }

        public virtual async Task<T> UpdateAsync<T>(T entity) where T : class, new()
        {
            _senderDbContext.Set<T>().Update(entity);
            await _senderDbContext.SaveChangesAsync(true);

            return entity;
        }

        public virtual async Task DeleteAsync<T>(T entity) where T : class, new()
        {
            _senderDbContext.Set<T>().Remove(entity);
            await _senderDbContext.SaveChangesAsync(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _senderDbContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
