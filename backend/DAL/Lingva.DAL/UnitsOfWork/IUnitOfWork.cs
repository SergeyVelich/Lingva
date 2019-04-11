using Lingva.DAL.Repositories;
using System;
using System.Threading.Tasks;

namespace Lingva.DAL.UnitsOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository Repository { get; }
        Task SaveAsync();
    }
}
