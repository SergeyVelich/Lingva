using System;

namespace Lingva.DAL.UnitsOfWork.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}
