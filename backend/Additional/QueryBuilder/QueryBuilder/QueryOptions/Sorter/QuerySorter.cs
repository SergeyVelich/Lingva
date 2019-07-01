using QueryBuilder.Enums;
using System.Diagnostics.CodeAnalysis;

namespace QueryBuilder.QueryOptions.Sorter
{
    [ExcludeFromCodeCoverage]
    public class QuerySorter
    {
        public string PropertyName { get; set; }
        public SortOrder SortOrder { get; set; }

        public QuerySorter(string propertyName, SortOrder sortOrder)
        {
            PropertyName = propertyName;
            SortOrder = sortOrder;
        }
    }
}
