using AutoMapper;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.Common.Mapping
{
    [ExcludeFromCodeCoverage]
    public class DataAdapter : IDataAdapter
    {
        private readonly IMapper _mapper;

        [ExcludeFromCodeCoverage]
        public DataAdapter(IMapper mapper)
        {
            _mapper = mapper;
        }

        [ExcludeFromCodeCoverage]
        public DestinationType Map<DestinationType>(object source)
            where DestinationType : class
        {
            return _mapper.Map<DestinationType>(source);
        }

        [ExcludeFromCodeCoverage]
        public DestinationType Update<DestinationType>(DestinationType source, DestinationType destination)
            where DestinationType : class
        {
            return _mapper.Map(source, destination);
        }
    }
}
