using Lingva.DataAccessLayer.Repositories.Contracts;

namespace Lingva.DataAccessLayer.UnitsOfWork.Contracts
{
    public interface IUnitOfWorkUser: IUnitOfWork
    {
        IRepositoryUser Users { get; }
    }
}
