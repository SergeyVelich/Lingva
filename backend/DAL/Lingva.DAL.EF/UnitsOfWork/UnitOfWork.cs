using Lingva.DAL.EF.Context;
using Lingva.DAL.Repositories;
using Lingva.DAL.UnitsOfWork;
using System;
using System.Threading.Tasks;

namespace Lingva.DAL.EF.UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        protected static DictionaryContext _context;
        protected static IRepository _repository;

        protected bool disposed = false;

        public IRepository Repository { get => _repository; }

        public UnitOfWork(DictionaryContext context, IRepository repository)
        {
            _context = context;
            _repository = repository;
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
