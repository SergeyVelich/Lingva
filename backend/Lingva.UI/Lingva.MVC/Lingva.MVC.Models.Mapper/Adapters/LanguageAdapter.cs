using AutoMapper;
using Lingva.BC.Dto;
using Lingva.MVC.Models.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.MVC.Mapper.Adapters
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
