using QueryBuilder.QueryOptions.Filter;
using QueryBuilder.QueryOptions.Includer;
using QueryBuilder.QueryOptions.Pagenator;
using QueryBuilder.QueryOptions.Selector;
using QueryBuilder.QueryOptions.Sorter;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace QueryBuilder.QueryOptions
{
    [ExcludeFromCodeCoverage]
    public class QueryOptions : IQueryOptions
    {
        public IList<QueryFilter> Filters { get; set; }
        public IList<QuerySorter> Sorters { get; set; }
        public IList<QuerySelector> Selectors { get; set; }
        public IList<QueryIncluder> Includers { get; set; }
        public QueryPagenator Pagenator { get; set; }

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
