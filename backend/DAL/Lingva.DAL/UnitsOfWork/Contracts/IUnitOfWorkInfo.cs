using Lingva.DAL.Repositories.Contracts;

namespace Lingva.DAL.UnitsOfWork.Contracts
{
    public interface IUnitOfWorkInfo : IUnitOfWork
    {
        IRepositoryLanguage Languages { get; }
    }
}
