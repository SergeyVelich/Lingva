using System.Diagnostics.CodeAnalysis;

namespace QueryBuilder.QueryOptions.Selector
{
    [ExcludeFromCodeCoverage]
    public class QuerySelector
    {
        public string PropertyName { get; set; }
        public string PropertySourceName { get; set; }

        public QuerySelector(string propertyName, string propertySourceName)
        {
            PropertyName = propertyName;
            PropertySourceName = propertySourceName;
        }
    }
}
