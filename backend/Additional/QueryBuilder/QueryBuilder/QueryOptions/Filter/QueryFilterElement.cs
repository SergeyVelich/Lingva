using QueryBuilder.Enums;
using System.Diagnostics.CodeAnalysis;

namespace QueryBuilder.QueryOptions.Filter
{
    [ExcludeFromCodeCoverage]
    public class QueryFilterElement : QueryFilter
    {
        public string PropertyName { get; set; }
        public object PropertyValue { get; set; }
        public FilterElementOperation Operation { get; set; }
        public QueryFilterAdditionalOptions Options { get; set; }

        public QueryFilterElement(string propertyName, object propertyValue, FilterElementOperation operation, QueryFilterAdditionalOptions options = null)
        {
            PropertyName = propertyName;
            PropertyValue = propertyValue;
            Operation = operation;
            Options = options;
        }
    }
}
