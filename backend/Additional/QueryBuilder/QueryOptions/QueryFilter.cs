using QueryBuilder.Enums;

namespace QueryBuilder.QueryOptions
{
    public class QueryFilter
    {
        public string PropertyName { get; set; }
        public object PropertyValue { get; set; }
        public FilterOperation Operation { get; set; }

        public QueryFilter(string propertyName, object propertyValue, FilterOperation operation)
        {
            PropertyName = propertyName;
            PropertyValue = propertyValue;
            Operation = operation;
        }
    }
}
