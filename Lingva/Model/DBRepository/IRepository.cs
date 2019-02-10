using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingva.Model
{
    interface IRepository<T> : IDisposable
    {
        IQueryable<T> GetList();
        T Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int item);

        Task GetAsync(int id);
        Task CreateAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(int item);
    }
}
