using Lingva.BC.Common.Enums;

namespace Lingva.WebAPI.Models.Request
{
    public class FilterModel
    {
        public string PropertyName { get; set; }
        public object PropertyValue { get; set; }
        public FilterOperation Operation { get; set; }
    }
}
