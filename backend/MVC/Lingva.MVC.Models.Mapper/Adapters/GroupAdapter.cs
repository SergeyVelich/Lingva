using AutoMapper;
using Lingva.BC.DTO;
using Lingva.MVC.Models.Request.Entities;
using Lingva.MVC.Models.Response.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.MVC.Mapper.Adapters
{
    [ExcludeFromCodeCoverage]
    public class GroupAdapter : Profile
    {
        public GroupAdapter()
        {
            CreateMap<GroupDTO, GroupViewModel>();

            CreateMap<GroupCreateViewModel, GroupDTO>();           
        }
    }
}
