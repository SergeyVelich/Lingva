using AutoMapper;
using Lingva.BC.DTO;
using Lingva.DAL.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.BC.Mapper.Adapters
{
    [ExcludeFromCodeCoverage]
    public class GroupAdapter : Profile
    {
        public GroupAdapter()
        {
            CreateMap<Group, GroupDTO>();
            CreateMap<GroupDTO, Group>();

            CreateMap<Group, Group>();
        }
    }
}
