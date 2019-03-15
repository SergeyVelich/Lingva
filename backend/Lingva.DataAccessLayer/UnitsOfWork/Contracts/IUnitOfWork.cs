using System;

namespace Lingva.DataAccessLayer.UnitsOfWork.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}
