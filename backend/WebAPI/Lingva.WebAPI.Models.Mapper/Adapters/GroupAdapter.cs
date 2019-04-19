using AutoMapper;
using Lingva.BC.Dto;
using Lingva.WebAPI.Models.Request;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.WebAPI.Mapper.Adapters
{
    [ExcludeFromCodeCoverage]
    public class GroupAdapter : Profile
    {
        public GroupAdapter()
        {
            CreateMap<GroupDto, GroupViewModel>();

            CreateMap<GroupViewModel, GroupDto>();
        }
    }
}
