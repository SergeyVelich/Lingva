using System;
using System.Threading.Tasks;

namespace Lingva.DAL.UnitsOfWork.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        Task SaveAsync();
    }
}
