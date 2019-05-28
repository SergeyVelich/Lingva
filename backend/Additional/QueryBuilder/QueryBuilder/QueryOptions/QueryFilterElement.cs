using QueryBuilder.Enums;
using System.Diagnostics.CodeAnalysis;

namespace QueryBuilder.QueryOptions
{
    [ExcludeFromCodeCoverage]
    public class QueryFilterElement : QueryFilter
    {
        public string PropertyName { get; set; }
        public object PropertyValue { get; set; }
        public FilterElementOperation Operation { get; set; }

        public QueryFilterElement(string propertyName, object propertyValue, FilterElementOperation operation)
        {
            PropertyName = propertyName;
            PropertyValue = propertyValue;
            Operation = operation;
        }
    }
}
