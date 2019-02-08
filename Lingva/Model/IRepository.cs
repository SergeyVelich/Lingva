using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingva.Model
{
    interface IRepository<T>
    {
        IQueryable<T> GetList();
        T Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int item);
    }
}
