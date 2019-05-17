using AutoMapper;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.Additional.Mapping.DataAdapter
{
    [ExcludeFromCodeCoverage]
    public class DataAdapter : IDataAdapter
    {
        private readonly IMapper _mapper;

        public DataAdapter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public DestinationType Map<DestinationType>(object source)
            where DestinationType : class
        {
            return _mapper.Map<DestinationType>(source);
        }

        public DestinationType Update<DestinationType>(DestinationType source, DestinationType destination)
            where DestinationType : class
        {
            return _mapper.Map(source, destination);
        }
    }
}
