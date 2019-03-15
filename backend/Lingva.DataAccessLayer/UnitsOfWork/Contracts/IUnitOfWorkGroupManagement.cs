using Lingva.DataAccessLayer.Repositories.Contracts;

namespace Lingva.DataAccessLayer.UnitsOfWork.Contracts
{
    public interface IUnitOfWorkGroupManagement : IUnitOfWork
    {
        IRepositoryGroup Groups { get; }
    }
}
