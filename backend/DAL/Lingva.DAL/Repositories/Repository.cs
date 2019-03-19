using Lingva.DAL.Context;
using Lingva.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Lingva.DAL.Repositories
{
    public abstract class Repository<T> : IRepository<T> 
        where T : class
    {
        protected readonly DictionaryContext _context;
        protected readonly DbSet<T> _entities;

        public Repository(DictionaryContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public virtual IQueryable<T> GetList()
        {
            return _entities.AsNoTracking();
        }

        public virtual IQueryable<T> GetList(Expression<Func<T, bool>> predicator)
        {
            return _entities.Where(predicator).AsNoTracking();
        }

        public virtual T Get(object id)
        {
            return _entities.Find((int)id);
        }

        public virtual T Get(Expression<Func<T, bool>> predicator)
        {
            return _entities.Where(predicator).FirstOrDefault();
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
            _context.Entry(entity).State = EntityState.Modified;

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
