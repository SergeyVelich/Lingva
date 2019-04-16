using AutoMapper;
using Lingva.BC.Dto;
using Lingva.MVC.Models.Request;
using Lingva.MVC.Models.Response;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.MVC.Mapper.Adapters
{
    [ExcludeFromCodeCoverage]
    public class GroupAdapter : Profile
    {
        public GroupAdapter()
        {
            CreateMap<GroupDto, GroupViewModel>();

            CreateMap<GroupCreateViewModel, GroupDto>();           
        }
    }
}
