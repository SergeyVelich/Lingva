using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingva.Model
{
    public class DictionaryRecordRepository : IRepository<DictionaryRecord>
    {
        private readonly DBContext _context;

        public DictionaryRecordRepository(DBContext context)
        {
            _context = context;
        }

        // Получить таблицу
        public IQueryable<DictionaryRecord> GetList()
        {
            return _context.Dictionary;
        }

        // Получить элемент таблицы
        public DictionaryRecord Get(int id)
        {
            return _context.Dictionary.Find(id);
        }

        public Task GetAsync(int id)
        {
            return _context.Dictionary.FindAsync(id);
        }

        // Добавление одного поля в БД
        public void Create(DictionaryRecord entity)
        {
            _context.Dictionary.Add(entity);
            _context.SaveChanges();
        }

        public Task CreateAsync(DictionaryRecord entity)
        {
            _context.Dictionary.Add(entity);
            return _context.SaveChangesAsync();
        }

        // Изменение одного поля в БД
        public void Update(DictionaryRecord entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public Task UpdateAsync(DictionaryRecord entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }

        // Удаление одного поля в БД
        public void Delete(int id)
        {
            var entity = _context.Dictionary.Find(id);
            if (entity == null)
            {
                return;
            }

            _context.Dictionary.Remove(entity);
            _context.SaveChanges();
        }

        public Task DeleteAsync(int id)
        {
            var entity = _context.Dictionary.FindAsync(id);
            if (entity == null)
            {
                return null;
            }

            _context.Dictionary.Remove(entity.Result);
            return _context.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
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
