using AutoMapper;
using Lingva.BC.DTO;
using Lingva.WebAPI.Models.Request;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.WebAPI.Mapper.Adapters
{
    [ExcludeFromCodeCoverage]
    public class SorterModelAdapter : Profile
    {
        public SorterModelAdapter()
        {
            CreateMap<SorterModel, QuerySorterDTO>();
        }
    }
}
