using AutoMapper;
using Lingva.BC.Dto;
using Lingva.MVC.Models.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.MVC.Mapper.Adapters
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
