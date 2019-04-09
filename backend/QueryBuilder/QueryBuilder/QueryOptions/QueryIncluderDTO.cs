namespace QueryBuilder.QueryOptions
{
    public class QueryIncluderDTO
    {
        public string PropertyName { get; set; }

        public QueryIncluderDTO(string propertyName)
        {
            PropertyName = propertyName;
        }
    }
}
