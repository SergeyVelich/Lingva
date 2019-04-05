using AutoMapper;
using Lingva.BC.DTO;
using Lingva.WebAPI.Models.Request.Entities;
using Lingva.WebAPI.Models.Response.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.WebAPI.Mapper.Adapters
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