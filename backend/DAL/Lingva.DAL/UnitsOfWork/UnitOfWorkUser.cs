using Lingva.DAL.Context;
using Lingva.DAL.Repositories.Contracts;
using Lingva.DAL.UnitsOfWork.Contracts;
using System;
using System.Threading.Tasks;

namespace Lingva.DAL.UnitsOfWork
{
    public class UnitOfWorkAccount : UnitOfWork, IUnitOfWorkAccount
    {
        private readonly IRepositoryAccount _accounts;

        public UnitOfWorkAccount(ApplicationDbContext context, IRepositoryAccount accounts) : base(context)
        {
            _accounts = accounts;
        }

        public IRepositoryAccount Accounts { get => _accounts; }
    }
}
