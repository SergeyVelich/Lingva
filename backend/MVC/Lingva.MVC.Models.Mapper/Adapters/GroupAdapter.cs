using AutoMapper;
using Lingva.BC.DTO;
using Lingva.MVC.ViewModel.Request;
using Lingva.MVC.ViewModel.Response;
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

            CreateMap<GroupDTO, GroupCreateViewModel>();
            CreateMap<GroupCreateViewModel, GroupDTO>();           
        }
    }
}
