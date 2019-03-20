using AutoMapper;
using Lingva.BC.Auth;
using Lingva.WebAPI.ViewModel.Request;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.WebAPI.Mapper.Adapters
{
    [ExcludeFromCodeCoverage]
    public class AuthRequestAdapter : Profile
    {
        [ExcludeFromCodeCoverage]
        public AuthRequestAdapter()
        {
            CreateMap<AuthRequestViewModel, AuthRequest>();
        }
    }
}
