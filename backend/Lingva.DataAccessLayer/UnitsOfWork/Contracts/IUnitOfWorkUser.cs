using Lingva.DAL.Repositories.Contracts;

namespace Lingva.DAL.UnitsOfWork.Contracts
{
    public interface IUnitOfWorkUser: IUnitOfWork
    {
        IRepositoryUser Users { get; }
    }
}
