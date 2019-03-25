using Lingva.DAL.Repositories.Contracts;

namespace Lingva.DAL.UnitsOfWork.Contracts
{
    public interface IUnitOfWorkAccount : IUnitOfWork
    {
        IRepositoryAccount Accounts { get; }
    }
}
