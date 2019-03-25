using AutoMapper;
using Lingva.BC.DTO;
using Lingva.WebAPI.ViewModel.Request;
using Lingva.WebAPI.ViewModel.Response;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.WebAPI.Mapper.Adapters
{
    [ExcludeFromCodeCoverage]
    public class AccountAdapter : Profile
    {
        [ExcludeFromCodeCoverage]
        public AccountAdapter()
        {
            CreateMap<AccountDTO, AccountViewModel>();

            CreateMap<LoginViewModel, AccountDTO>();

            CreateMap<RegisterViewModel, AccountDTO>();
        }
    }
}
