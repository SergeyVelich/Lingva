using AutoMapper;
using Lingva.BC.DTO;
using Lingva.DAL.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.BC.Mapper.Adapters
{
    [ExcludeFromCodeCoverage]
    public class LanguageAdapter : Profile
    {
        public LanguageAdapter()
        {
            CreateMap<Language, LanguageDTO>();
            CreateMap<LanguageDTO, Language>();
        }
    }
}
