using Lingva.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lingva.DAL.Repositories
{
    public interface IRepository
    {
        Task<IEnumerable<T>> GetListAsync<T>() where T : BaseBE, new();
        Task<IEnumerable<T>> GetListAsync<T>(Expression<Func<T, bool>> predicator = null, IEnumerable<string> sorters = null, ICollection<Expression<Func<T, bool>>> includers = null, int skip = 0, int take = 0) where T : BaseBE, new();
        Task<T> GetByIdAsync<T>(int id) where T : BaseBE, new();
        Task<T> GetAsync<T>(Expression<Func<T, bool>> predicator) where T : BaseBE, new();

        Task<T> CreateAsync<T>(T entity) where T : BaseBE, new();
        Task<T> UpdateAsync<T>(T entity) where T : BaseBE, new();
        Task DeleteAsync<T>(T entity) where T : BaseBE, new();
    }
}
