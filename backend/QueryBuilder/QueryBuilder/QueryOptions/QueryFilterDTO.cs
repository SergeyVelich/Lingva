using QueryBuilder.QueryOptions.Enums;

namespace QueryBuilder.QueryOptions
{
    public class QueryFilterDTO
    {
        public string PropertyName { get; set; }
        public object PropertyValue { get; set; }
        public FilterOperation Operation { get; set; }

        public QueryFilterDTO(string propertyName, object propertyValue, FilterOperation operation)
        {
            PropertyName = propertyName;
            PropertyValue = propertyValue;
            Operation = operation;
        }
    }
}
