using Lingva.DAL.Repositories.Contracts;

namespace Lingva.DAL.UnitsOfWork.Contracts
{
    public interface IUnitOfWorkGroup : IUnitOfWork
    {
        IRepositoryGroup Groups { get; }
    }
}
