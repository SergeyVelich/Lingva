using QueryBuilder.Enums;
using System.Diagnostics.CodeAnalysis;

namespace QueryBuilder.QueryOptions.Filter
{
    [ExcludeFromCodeCoverage]
    public class QueryFilterGroup : QueryFilter
    {
        public FilterGroupOperation Operation { get; set; }
        public QueryFilterElement[] FilterElements { get; set; }

        public QueryFilterGroup(FilterGroupOperation operation)
        {
            Operation = operation;
        }

        public QueryFilterGroup(FilterGroupOperation operation, QueryFilterElement[] filterElements)
        {
            Operation = operation;
            FilterElements = filterElements;
        }
    }
}
