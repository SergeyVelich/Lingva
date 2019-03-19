using AutoMapper;
using Lingva.BC.DTO;
using Lingva.WebAPI.ViewModel;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.WebAPI.Mapper.Adapters
{
    [ExcludeFromCodeCoverage]
    public class GroupAdapter : Profile
    {
        [ExcludeFromCodeCoverage]
        public GroupAdapter()
        {
            CreateMap<GroupDTO, GroupViewModel>();
            CreateMap<GroupViewModel, GroupDTO>();
        }
    }
}
