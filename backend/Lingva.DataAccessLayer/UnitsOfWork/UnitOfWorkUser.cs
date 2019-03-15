using System;
using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Repositories.Contracts;
using Lingva.DataAccessLayer.UnitsOfWork.Contracts;

namespace Lingva.DataAccessLayer.UnitsOfWork
{
    public class UnitOfWorkUser:IUnitOfWorkUser
    {
        private static DictionaryContext _context;

        private bool disposed = false;

        private readonly IRepositoryUser _users;

        public IRepositoryUser Users { get => _users;}

        public UnitOfWorkUser(DictionaryContext context, IRepositoryUser userRepository)
        {
            _context = context;
            _users = userRepository;
        }

        public void Save()
        {
            _context.SaveChanges();
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
