namespace QueryBuilder.QueryOptions
{
    public class QueryPagenator
    {
        public int Skip { get; set; }
        public int Take { get; set; }

        public QueryPagenator(int take, int skip)
        {
            Take = take;
            Skip = skip;
        }
    }
}
