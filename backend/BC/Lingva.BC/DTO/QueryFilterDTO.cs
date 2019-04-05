using Lingva.BC.Common.Enums;

namespace Lingva.BC.DTO
{
    public class QueryFilterDTO
    {
        public string PropertyName { get; set; }
        public object PropertyValue { get; set; }
        public FilterOperation Operation { get; set; }
    }
}
