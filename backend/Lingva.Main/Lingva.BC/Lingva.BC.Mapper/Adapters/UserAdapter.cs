using AutoMapper;
using Lingva.BC.Dto;
using Lingva.DAL.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.BC.Mapper.Adapters
{
    [ExcludeFromCodeCoverage]
    public class UserAdapter : Profile
    {
        public UserAdapter()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<User, User>();
        }
    }
}
