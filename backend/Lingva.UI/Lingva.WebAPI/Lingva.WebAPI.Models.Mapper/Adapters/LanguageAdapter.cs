using AutoMapper;
using Lingva.BC.Dto;
using Lingva.WebAPI.Models.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.WebAPI.Mapper.Adapters
{
    [ExcludeFromCodeCoverage]
    public class LanguageAdapter : Profile
    {
        public LanguageAdapter()
        {
            CreateMap<LanguageDto, LanguageViewModel>();

            CreateMap<LanguageViewModel, LanguageDto>();
        }
    }
}
