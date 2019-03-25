using AutoMapper;
using Lingva.BC.DTO;
using Lingva.DAL.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.BC.Mapper.Adapters
{
    [ExcludeFromCodeCoverage]
    public class AccountAdapter : Profile
    {
        [ExcludeFromCodeCoverage]
        public AccountAdapter()
        {
            CreateMap<Account, AccountDTO>();
            CreateMap<AccountDTO, Account>();
        }
    }
}
