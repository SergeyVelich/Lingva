using AutoMapper;
using Lingva.BC.DTO;
using Lingva.MVC.Models.Request;
using Lingva.MVC.Models.Response;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.MVC.Mapper.Adapters
{
    [ExcludeFromCodeCoverage]
    public class UserAdapter : Profile
    {
        public UserAdapter()
        {
            CreateMap<UserDTO, UserViewModel>();

            CreateMap<UserCreateViewModel, UserDTO>();
        }
    }
}
