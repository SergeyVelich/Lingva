using AutoMapper;
using Lingva.BC.Auth;
using Lingva.MVC.ViewModel.Request;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.MVC.Mapper.Adapters
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
