using System.Collections.Generic;

namespace QueryBuilder.QueryOptions
{
    public interface IQueryOptions
    {
        IList<QueryFilter> Filters { get; }
        IList<QuerySorter> Sorters { get; }
        IList<QuerySelector> Selectors { get; }
        IList<QueryIncluder> Includers { get; }
        QueryPagenator Pagenator { get; }         
    }
}
