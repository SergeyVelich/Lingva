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
                .ForMember(dto => dto.LanguageId, opt => opt.MapFrom(g => g.Language == null ? 0 : g.Language.Id))
                .ForMember(dto => dto.LanguageName, opt => opt.MapFrom(g => g.Language == null ? null : g.Language.Name));
            CreateMap<GroupDto, Group>();
                //.ForMember(dto => dto.Language, opt => opt.MapFrom(g => new Language { Id = g.LanguageId, Name = g.LanguageName }));

            CreateMap<Group, Group>();
        }
    }
}
