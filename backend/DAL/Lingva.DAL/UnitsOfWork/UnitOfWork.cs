using Lingva.DAL.Context;
using Lingva.DAL.UnitsOfWork.Contracts;
using System;
using System.Threading.Tasks;

namespace Lingva.DAL.UnitsOfWork
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        protected static DictionaryContext _context;

        protected bool disposed = false;

        public UnitOfWork(DictionaryContext context)
        {
            _context = context;
        }

        public virtual void Save()
        {
            _context.SaveChanges();
        }

        public virtual async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
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
