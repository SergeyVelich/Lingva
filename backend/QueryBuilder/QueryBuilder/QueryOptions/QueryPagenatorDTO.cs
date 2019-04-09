namespace QueryBuilder.QueryOptions
{
    public class QueryPagenatorDTO
    {
        public int Skip { get; set; }
        public int Take { get; set; }

        public QueryPagenatorDTO(int take, int skip)
        {
            Take = take;
            Skip = skip;
        }
    }
}
