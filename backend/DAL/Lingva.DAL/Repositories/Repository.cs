using Lingva.DAL.Context;
using Lingva.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lingva.DAL.Repositories
{
    public abstract class Repository<T> : IRepository<T> 
        where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _entities;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public virtual IEnumerable<T> GetList(Expression<Func<T, bool>> predicator = null)
        {
            IQueryable<T> result = _entities.AsNoTracking();

            if (predicator != null)
            {
                result.Where(predicator);
            }
            return result.ToList();
        }

        public virtual async Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicator = null)
        {
            IQueryable<T> result = _entities.AsNoTracking();

            if (predicator != null)
            {
                result.Where(predicator);
            }
            return await result.ToListAsync();
        }

        public virtual T GetById(int id)
        {
            return _entities.Find((int)id);
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _entities.FindAsync((int)id);
        }

        public virtual T Get(Expression<Func<T, bool>> predicator)
        {
            return _entities.Where(predicator).FirstOrDefault();
        }

        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> predicator)
        {
            return await _entities.Where(predicator).FirstOrDefaultAsync();
        }

        public virtual T Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Tried to insert null entity!");
            }

            _entities.Add(entity);

            return entity;
        }

        public virtual T Update(T entity)
        {
            _entities.Attach(entity);
            _entities.Update(entity);

            return entity;
        }

        public virtual void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _entities.Attach(entity);
            }
            _entities.Remove(entity);
        }
    }
}
