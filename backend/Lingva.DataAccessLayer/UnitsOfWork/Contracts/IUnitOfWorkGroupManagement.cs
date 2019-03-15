using Lingva.DAL.Repositories.Contracts;

namespace Lingva.DAL.UnitsOfWork.Contracts
{
    public interface IUnitOfWorkGroupManagement : IUnitOfWork
    {
        IRepositoryGroup Groups { get; }
    }
}
