using Lingva.DAL.Context;
using Lingva.DAL.Entities;
using Lingva.DAL.Repositories.Contracts;

namespace Lingva.DAL.Repositories
{
    public class RepositoryAccount : Repository<Account>, IRepositoryAccount
    {
        public RepositoryAccount(ApplicationDbContext context) : base(context)

        {

        }
    }
}