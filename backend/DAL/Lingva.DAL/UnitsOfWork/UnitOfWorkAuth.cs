using Lingva.DAL.Context;
using Lingva.DAL.Repositories.Contracts;
using Lingva.DAL.UnitsOfWork.Contracts;

namespace Lingva.DAL.UnitsOfWork
{
    public class UnitOfWorkAuth : UnitOfWork, IUnitOfWorkAuth
    {
        private readonly IRepositoryAccount _accounts;

        public UnitOfWorkAuth(ApplicationDbContext context, IRepositoryAccount accounts) : base(context)
        {
            _accounts = accounts;
        }

        public IRepositoryAccount Accounts { get => _accounts; }
    }
}
