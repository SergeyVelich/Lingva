using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace QueryBuilder.QueryOptions
{
    [ExcludeFromCodeCoverage]
    public class QueryOptions : IQueryOptions
    {
        public IList<QueryFilter> Filters { get; }
        public IList<QuerySorter> Sorters { get; }
        public IList<QuerySelector> Selectors { get; }
        public IList<QueryIncluder> Includers { get; }
        public QueryPagenator Pagenator { get; }

        public QueryOptions(IList<QueryFilter> filters = null,
            IList<QuerySorter> sorters = null,
            IList<QuerySelector> selectors = null,
            IList<QueryIncluder> includers = null, 
            QueryPagenator pagenator = null)
        {
            Filters = filters;
            Sorters = sorters;
            Selectors = selectors;
            Includers = includers;
            Pagenator = pagenator;
        }      
    }
}
