namespace QueryBuilder.QueryOptions
{
    public class QueryIncluder
    {
        public string PropertyName { get; set; }

        public QueryIncluder(string propertyName)
        {
            PropertyName = propertyName;
        }
    }
}
