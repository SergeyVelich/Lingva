using Lingva.DAL.Repositories.Contracts;

namespace Lingva.DAL.UnitsOfWork.Contracts
{
    public interface IUnitOfWorkAuth : IUnitOfWork
    {
        IRepositoryAccount Accounts { get; }
    }
}
