﻿using Lingva.DAL.Context;
using Lingva.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using QueryBuilder.Extensions;
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
        protected readonly DictionaryContext _context;
        protected readonly DbSet<T> _entities;

        public Repository(DictionaryContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public virtual IEnumerable<T> GetList(Expression<Func<T, bool>> predicator = null, IEnumerable<string> sorters = null, int skip = 0, int take = 0)
        {
            IQueryable<T> result = _entities.AsNoTracking();

            if (predicator != null)
            {
                result = result.Where(predicator);
            }

            if (sorters != null)
            {
                result = result.OrderBy(sorters);
            }

            if (skip != 0)
            {
                result = result.Skip(skip);
            }

            if (take != 0)
            {
                result = result.Take(take);
            }

            return result.ToList();
        }

        public virtual async Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicator = null, IEnumerable<string> sorters = null, int skip = 0, int take = 0)
        {
            IQueryable<T> result = _entities.AsNoTracking();

            if (predicator != null)
            {
                result = result.Where(predicator);
            }

            if (sorters != null)
            {
                result = result.OrderBy(sorters);
            }

            if (skip != 0)
            {
                result = result.Skip(skip);
            }

            if (take != 0)
            {
                result = result.Take(take);
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

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicator = null, IEnumerable<string> sorters = null, int skip = 0, int take = 0)
        {
            IQueryable<T> result = _entities.AsNoTracking();

            if (predicator != null)
            {
                result = result.Where(predicator);
            }

            if (sorters != null)
            {
                result = result.OrderBy(sorters);
            }

            if (skip != 0)
            {
                result = result.Skip(skip);
            }

            if (take != 0)
            {
                result = result.Take(take);
            }

            return await result.CountAsync();
        }

        public virtual int Count(Expression<Func<T, bool>> predicator = null, IEnumerable<string> sorters = null, int skip = 0, int take = 0)
        {
            IQueryable<T> result = _entities.AsNoTracking();

            if (predicator != null)
            {
                result = result.Where(predicator);
            }

            if (sorters != null)
            {
                result = result.OrderBy(sorters);
            }

            if (skip != 0)
            {
                result = result.Skip(skip);
            }

            if (take != 0)
            {
                result = result.Take(take);
            }

            return result.Count();
        }
    }
}
