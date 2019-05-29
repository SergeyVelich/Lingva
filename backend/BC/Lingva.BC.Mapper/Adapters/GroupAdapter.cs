using AutoMapper;
using Lingva.BC.Dto;
using Lingva.DAL.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.BC.Mapper.Adapters
{
    [ExcludeFromCodeCoverage]
    public class GroupAdapter : Profile
    {
        public GroupAdapter()
        {
            CreateMap<Group, GroupDto>()
                .ForMember(dto => dto.LanguageId, opt => opt.MapFrom(g => g.Language.Id))
                .ForMember(dto => dto.LanguageName, opt => opt.MapFrom(g => g.Language.Name));
            CreateMap<GroupDto, Group>();

            CreateMap<Group, Group>();
        }
    }
}
