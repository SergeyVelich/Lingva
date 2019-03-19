using AutoMapper;
using Lingva.BC.DTO;
using Lingva.MVC.ViewModel;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.MVC.Mapper.Adapters
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
