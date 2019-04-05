using AutoMapper;
using Lingva.BC.DTO;
using Lingva.WebAPI.Models.Request;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.WebAPI.Mapper.Adapters
{
    [ExcludeFromCodeCoverage]
    public class PagenatorModelAdapter : Profile
    {
        public PagenatorModelAdapter()
        {
            CreateMap<PagenatorModel, QueryPagenatorDTO>()
                .ForMember("Take", opt => opt.MapFrom(c => c.PageSize))
                .ForMember("Skip", opt => opt.MapFrom(c => (c.CurrentPage - 1) * c.PageSize));
        }
    }
}
