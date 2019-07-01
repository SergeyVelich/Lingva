using System.Diagnostics.CodeAnalysis;

namespace QueryBuilder.QueryOptions.Pagenator
{
    [ExcludeFromCodeCoverage]
    public class QueryPagenator
    {
        public int Skip { get; set; }
        public int Take { get; set; }

        public QueryPagenator()
        {

        }

        public QueryPagenator(int take, int skip)
        {
            Take = take;
            Skip = skip;
        }
    }
}
