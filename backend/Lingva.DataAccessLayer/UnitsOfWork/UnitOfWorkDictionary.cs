using Lingva.DAL.Context;
using Lingva.DAL.Repositories.Contracts;
using Lingva.DAL.UnitsOfWork.Contracts;
using System;

namespace Lingva.DAL.UnitsOfWork
{
    public class UnitOfWorkDictionary : IUnitOfWorkDictionary
    {
        private static DictionaryContext _context;

        private bool disposed = false;

        private readonly IRepositoryDictionaryRecord _dictionaryRecords;
        private readonly IRepositoryWord _words;

        public UnitOfWorkDictionary(DictionaryContext context, IRepositoryWord words, IRepositoryDictionaryRecord dictionaryRecords)
        {
            _context = context;
            _dictionaryRecords = dictionaryRecords;
            _words = words;
        }

        public IRepositoryDictionaryRecord DictionaryRecords { get => _dictionaryRecords; }
        public IRepositoryWord Words { get => _words; }

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
